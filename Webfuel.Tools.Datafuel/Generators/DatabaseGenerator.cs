using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class DatabaseGenerator
    {
        public static void GenerateDatabase(Schema schema)
        {
            CreateDatabase();
            CreateSchema();

            // Tables
            foreach (var entity in schema.Entities.Where(p => p.Repository && p.View == null))
                ExecuteNonQuery(TableDefinition(entity));

            // Views
            foreach (var entity in schema.Entities.Where(p => p.Repository && p.View != null))
                ExecuteNonQuery(ViewDefinition(entity));

            // Constraints
            foreach (var entity in schema.Entities.Where(p => p.Repository))
            {
                // Foreign Keys
                foreach (var reference in entity.References)
                    ExecuteNonQuery(ForeignKeyDefinition(reference));

                // Indexes
                foreach (var index in entity.Indexes.Where(p => p.Repository))
                    ExecuteNonQuery(IndexDefinition(index));

                // Other
                foreach (var member in entity.Members)
                    ExecuteNonQuery(ConstraintDefinition(member));
            }

            // Data
            foreach(var entity in schema.Entities.Where(p => p.Repository))
            {
                if (entity.Data != null)
                    WriteToDatabase.WriteDataSetToDatabase(entity.Data, Settings.DatabaseName, Settings.DatabaseServer);
            }

            // Check constraints following bulk data inserts (which ignore constraints & triggers)
            WriteToDatabase.CheckConstraints(Settings.DatabaseName, Settings.DatabaseServer);
        }

        // Table Definition

        static string TableName(SchemaEntity entity)
        {
            return entity.Name;
        }

        static string TableDefinition(SchemaEntity entity)
        {
            var sb = new StringBuilder();
            sb.Append("CREATE TABLE [dbo].[" + TableName(entity) + "] (\n");

            foreach (var member in entity.Members)
                sb.Append(member.GenerateSqlColumnDefinition() + ",\n");

            if (entity.Key != null)
                sb.Append(PrimaryKeyDefinition(entity.Key) + "\n");

            sb.Append(") ON [PRIMARY];\n");
            sb.Append("GO\n");

            return sb.ToString();
        }

        // Constraint Definition

        static string PrimaryKeyDefinition(SchemaEntityProperty key)
        {
            return "CONSTRAINT [PK_" + TableName(key.Entity) + "] PRIMARY KEY CLUSTERED (" + key.Name + " ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
        }

        static string ForeignKeyDefinition(SchemaEntityReference reference)
        {
            if (reference.ReferenceEntity.Key == null)
                throw new InvalidOperationException("Cannot reference an entity without a primary key");

            StringBuilder sb = new StringBuilder();
            sb.Append("ALTER TABLE [dbo].[" + TableName(reference.Entity) + "] WITH CHECK ADD CONSTRAINT FK_" + TableName(reference.Entity) + "_" + reference.Name + "\n");
            sb.Append("FOREIGN KEY (" + reference.Name + ") REFERENCES [dbo].[" + TableName(reference.ReferenceEntity) + "] (" + reference.ReferenceEntity.Key.Name + ")\n");

            if (reference.CascadeDelete)
                sb.Append("ON DELETE CASCADE\n");

            sb.Append("GO\n");
            return sb.ToString();
        }
        
        static string ConstraintDefinition(SchemaEntityMember member)
        {
            return String.Empty;
        }

        // Index Definition

        static string IndexName(SchemaEntityIndex index)
        {
            var sb = new StringBuilder("IX_" + TableName(index.Entity));
            foreach (var member in index.Members)
            {
                sb.Append("_" + member.Name);
            }
            return sb.ToString();
        }

        static string IndexDefinition(SchemaEntityIndex index)
        {
            var sb = new StringBuilder();
            sb.Append("CREATE " + (index.Unique ? "UNIQUE " : "") + "NONCLUSTERED INDEX " + IndexName(index) + "\n");
            sb.Append("ON [dbo].[" + TableName(index.Entity) + "] (");
            foreach (var member in index.Members)
            {
                sb.Append("[" + member.Name + "] ASC, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(")\nWITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);");
            return sb.ToString();
        }

        // View Definition

        static string ViewDefinition(SchemaEntity entity)
        {
            var sb = new StringBuilder();
            sb.Append($"CREATE VIEW [dbo].[{entity.Name}]\n");
            sb.Append($"AS {entity.View!.Sql}");
            sb.Append(";");
            return sb.ToString();
        }

        // Database Helpers

        static void CreateDatabase()
        {
            string sql =
@"
IF EXISTS (SELECT dbid FROM master.dbo.sysdatabases WHERE name = '{0}') BEGIN
	ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [{0}]
END
CREATE DATABASE [{0}]
";
            sql = String.Format(sql, Settings.DatabaseName);
            using (SqlConnection connection = new SqlConnection(Settings.DatabaseServer))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        static void CreateSchema()
        {
            if (Settings.DatabaseSchema == "dbo")
                return; // nothing to do

            string sql =
@"
CREATE SCHEMA {0}
";
            sql = String.Format(sql, Settings.DatabaseSchema);
            using (SqlConnection connection = new SqlConnection(Settings.DatabaseServer))
            {
                connection.Open();
                connection.ChangeDatabase(Settings.DatabaseName);
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        static void ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            StringReader sr = new StringReader(sql);
            StringBuilder sb = new StringBuilder();
            String? line;
            String? test;

            sql = sql.Replace("[dbo]", $"[{Settings.DatabaseSchema}]");

            line = sr.ReadLine();
            while (line != null)
            {
                // Is Line a GO signal
                test = line.Trim();
                if (test.ToUpper() == "GO")
                {
                    // Yes - so execute this block
                    if (sb.Length > 0)
                    {
                        ExecuteSqlBlock(sb.ToString(), parameters);
                        sb.Length = 0;
                    }
                }
                else
                {
                    // No so continue to build the block
                    sb.Append("\n");
                    sb.Append(line);
                }
                line = sr.ReadLine();
            }
            // Do we have a block to execute
            if (sb.Length > 0)
                ExecuteSqlBlock(sb.ToString(), parameters);
        }

        static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            sql = sql.Replace("[dbo]", $"[{Settings.DatabaseSchema}]");

            using (SqlConnection connection = new SqlConnection(Settings.DatabaseServer))
            {
                connection.Open();
                connection.ChangeDatabase(Settings.DatabaseName);
                SqlCommand command = new SqlCommand(sql, connection as SqlConnection);
                foreach (SqlParameter parameter in parameters)
                    command.Parameters.Add(parameter);
                object result = command.ExecuteScalar();
                connection.Close();
                return result;
            }
        }

        static void ExecuteSqlBlock(string sql, params SqlParameter[] parameters)
        {
            sql = sql.Replace("[dbo]", $"[{Settings.DatabaseSchema}]");

            using (SqlConnection connection = new SqlConnection(Settings.DatabaseServer))
            {
                connection.Open();
                connection.ChangeDatabase(Settings.DatabaseName);

                bool error = false;
                try
                {
                    SqlCommand command = new SqlCommand(sql, connection as SqlConnection);
                    foreach (SqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = true;
                    // Display details
                    Console.Error.WriteLine("----[SQL ERROR]--------------------------------------------------------------");
                    Console.Error.WriteLine(ex.Message);

                    if (!String.IsNullOrEmpty(ex.Procedure))
                        Console.Error.WriteLine("Proc: " + ex.Procedure);

                    // Try to pick out the actual error line:
                    string[] parts = sql.Split('\n');
                    if (parts.Length > ex.LineNumber)
                        Console.Error.WriteLine("Line [" + ex.LineNumber.ToString() + "]: " + parts[ex.LineNumber - 1]);
                    Console.Error.WriteLine("-----------------------------------------------------------------------------");

                }
                if (error)
                {
                    Console.Error.WriteLine("----[SQL]--------------------------------------------------------------------");
                    Console.Error.WriteLine(FormatSqlBlock(sql) + FormatSqlParameters(parameters));
                    Console.Error.WriteLine("-----------------------------------------------------------------------------");
                    Console.ReadLine();
                }
            }
        }

        static string FormatSqlBlock(string sql)
        {
            string[] parts = sql.Split('\n');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                sb.Append(string.Format("{0,3 } | {1}\n", i + 1, parts[i]));
            }
            return sb.ToString();
        }

        static string FormatSqlParameters(params SqlParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return "";
            StringBuilder sb = new StringBuilder("Parameters:\n");
            foreach (SqlParameter parameter in parameters)
            {
                sb.Append(parameter.ParameterName + ": " + (parameter.Value ?? "").ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}
