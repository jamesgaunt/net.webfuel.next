using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class WriteToDatabase
    {
        public static void WriteDataSetToDatabase(SchemaData data, string databaseName, string databaseServer)
        {
            using (var connection = new SqlConnection(databaseServer))
            {
                connection.Open();
                connection.ChangeDatabase(databaseName);
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, null))
                    {
                        bulkCopy.DestinationTableName = "[" + Settings.DatabaseSchema + "].[" + data.Entity.Name + "]";
                        foreach (var member in data.Entity.Members)
                        {
                            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(member.Name, member.Name));
                        }
                        bulkCopy.WriteToServer(new SchemaDataReader(data));
                    }
                }
                connection.Close();
            }
        }

        public static void CheckConstraints(string databaseName, string databaseServer)
        {
            using (var connection = new SqlConnection(databaseServer))
            {
                connection.Open();
                connection.ChangeDatabase(databaseName);

                var command = connection.CreateCommand();
                command.CommandText = "exec sp_msforeachtable 'alter table ? with check check constraint all'";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public class SchemaDataReader : DbDataReader
        {
            private readonly SchemaData Data;
            private int RowIndex = -1;

            public SchemaDataReader(SchemaData data)
            {
                Data = data;
            }

            protected override void Dispose(bool disposing)
            {
            }

            public override object this[int ordinal]
            {
                get
                {
                    return GetValue(ordinal);
                }
            }

            public override object this[string name]
            {
                get
                {
                    return GetValue(GetOrdinal(name));
                }
            }

            public override int Depth => 1;

            public override int FieldCount => Data.Rows.Count;

            public override bool HasRows => true;

            public override bool IsClosed
            {
                get
                {
                    return RowIndex >= Data.Rows.Count;
                }
            }

            public override int RecordsAffected
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public override bool GetBoolean(int ordinal)
            {
                return (bool)GetValue(ordinal)!;
            }

            public override byte GetByte(int ordinal)
            {
                return (byte)GetValue(ordinal)!;
            }

            public override long GetBytes(int ordinal, long dataOffset, byte[]? buffer, int bufferOffset, int length)
            {
                throw new NotImplementedException();
            }

            public override char GetChar(int ordinal)
            {
                return (char)GetValue(ordinal)!;
            }

            public override long GetChars(int ordinal, long dataOffset, char[]? buffer, int bufferOffset, int length)
            {
                throw new NotImplementedException();
            }

            public override string GetDataTypeName(int ordinal)
            {
                throw new NotImplementedException();
            }

            public override DateTime GetDateTime(int ordinal)
            {
                return (DateTime)GetValue(ordinal)!;
            }

            public override decimal GetDecimal(int ordinal)
            {
                return (decimal)GetValue(ordinal)!;
            }

            public override double GetDouble(int ordinal)
            {
                return (double)GetValue(ordinal)!;
            }

            public override IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public override Type GetFieldType(int ordinal)
            {
                var value = GetValue(ordinal);
                if (value == null)
                    return typeof(object);

                return value.GetType();
            }

            public override float GetFloat(int ordinal)
            {
                return (float)GetValue(ordinal)!;
            }

            public override Guid GetGuid(int ordinal)
            {
                return (Guid)GetValue(ordinal)!;
            }

            public override short GetInt16(int ordinal)
            {
                return (short)GetValue(ordinal)!;
            }

            public override int GetInt32(int ordinal)
            {
                return (int)GetValue(ordinal)!;
            }

            public override long GetInt64(int ordinal)
            {
                return (long)GetValue(ordinal)!;
            }

            public override string GetString(int ordinal)
            {
                return (string)GetValue(ordinal)!;
            }

            public override string GetName(int ordinal)
            {
                return Data.Entity.Members.ElementAt(ordinal).Name;
            }

            public override int GetOrdinal(string name)
            {
                var i = 0;
                foreach(var member in Data.Entity.Members)
                {
                    if (member.Name == name)
                        return i;
                    i++;
                }
                return -1;
            }

            public override object GetValue(int ordinal)
            {
                var name = GetName(ordinal);
                if (name == null)
                    return DBNull.Value;

                if (RowIndex < 0 || IsClosed)
                    return DBNull.Value;

                return Data.Rows[RowIndex].GetValue(name);
            }

            public override int GetValues(object?[] values)
            {
                int max = Math.Min(values.Length, FieldCount);
                for (var i = 0; i < max; i++)
                {
                    values[i] = IsDBNull(i) ? DBNull.Value : GetValue(i);
                }

                return max;
            }

            public override bool IsDBNull(int ordinal)
            {
                return GetValue(ordinal) == null;
            }

            public override bool NextResult()
            {
                return false;
            }

            public override bool Read()
            {
                RowIndex++;
                return !IsClosed;
            }

            /*
            object GetDynamicValue(SchemaMember member, string attributeValue)
            {
                if (attributeValue == "{NewGuid}")
                    return Guid.NewGuid();

                if (attributeValue == "{ZeroGuid}")
                    return Guid.Empty;

                if (attributeValue == "{Today}")
                    return DateTime.Today;

                if (attributeValue == "{NULL}")
                    return null;

                if (member is SchemaReference)
                {
                    var name = attributeValue.Substring(1, attributeValue.Length - 2);
                    var reference = member as SchemaReference;
                    var data = reference.ReferenceEntity.Data;
                    foreach(var row in data.Rows)
                    {
                        if (row.Attributes.ContainsKey("Name") && row.Attributes["Name"] == name)
                            return Guid.Parse(row.Attributes["Id"]);
                    }
                }

                throw new InvalidOperationException("Unable to parse dynamic data value: " + attributeValue);
            }
            */
        }
    }
}
