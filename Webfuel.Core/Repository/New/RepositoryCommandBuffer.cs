using Microsoft.Data.SqlClient;

namespace Webfuel.Repository.New
{
    public class RepositoryCommandBuffer
    {
        private readonly RepositoryConnection _connection;
        internal readonly List<RepositoryCommand> _commands = new List<RepositoryCommand>();

        internal RepositoryCommandBuffer(RepositoryConnection connection)
        {
            _connection = connection;
        }

        public void AddCommand(string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            _commands.Add(new RepositoryCommand { Sql = sql, Parameters = parameters });
        }

        public Task ExecuteCommands()
        {
            return _connection.ExecuteCommands(_commands);
        }
    }
}
