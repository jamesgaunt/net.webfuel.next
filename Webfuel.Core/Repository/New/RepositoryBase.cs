using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Repository.New;

namespace Webfuel
{
    public abstract class RepositoryBase<M, E> where M : IRepositoryMetadata<E> where E : class
    {
        protected readonly IRepositoryConnection _connection;

        protected RepositoryBase(IRepositoryConnection connection)
        {
            _connection = connection;

        }

        /*
        public async Task<Job?> GetJob(Guid id)
        {
            var sql = @"SELECT * FROM [Job] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReader<Job>(sql, parameters)).SingleOrDefault();
        }
        */
    }
}
