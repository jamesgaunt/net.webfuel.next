using Microsoft.Data.SqlClient;

namespace Webfuel
{
    public class RepositoryCommandBuffer
    {
        internal readonly List<RepositoryCommand> _commands = new List<RepositoryCommand>();

        internal RepositoryConnection? Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
            }
        }
        RepositoryConnection? _connection = null;

        internal void AddCommand(string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            _commands.Add(new RepositoryCommand { Sql = sql, Parameters = parameters });
        }

        public async Task Execute()
        {
            if (_commands.Count == 0)
                return;

            if (Connection == null)
                throw new InvalidOperationException("RepositoryCommandBuffer: No connection has been set");

            await Connection.ExecuteCommands(_commands);
        }
    }
}
