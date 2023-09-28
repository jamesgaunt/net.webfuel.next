using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Repository.New
{
    public interface IRepositoryReader<TEntity>
    {
        TEntity Read(SqlDataReader dr);
    }

}
