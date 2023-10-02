using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Webfuel
{
    internal class RepositoryCommand
    {
        /// <summary>
        /// Sql to be executed for this Command
        /// </summary>
        public required string Sql { get; init; }

        /// <summary>
        /// List of Sql Parameters for this Command
        /// </summary>
        public IEnumerable<SqlParameter>? Parameters { get; init; }
    }
}
