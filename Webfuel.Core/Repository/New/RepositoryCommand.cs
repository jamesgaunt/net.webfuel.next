using Microsoft.Data.SqlClient;

namespace Webfuel.Repository.New
{
    internal class RepositoryCommand
    {
        public required string Sql { get; init; }

        public IEnumerable<SqlParameter>? Parameters { get; init; }
    }
}
