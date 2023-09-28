using Microsoft.Data.SqlClient;

namespace Webfuel.Repository.New
{
    public class RepositoryTransaction
    {
        private readonly RepositoryConnection _connection;
        internal readonly List<RepositoryCommand> _commands = new List<RepositoryCommand>();

        internal RepositoryTransaction(RepositoryConnection connection)
        {
            _connection = connection;
        }

        public void AddCommand(string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            _commands.Add(new RepositoryCommand { Sql = sql, Parameters = parameters });
        }

        public Task Execute()
        {
            return _connection.ExecuteCommands(_commands);
        }
    }
}
