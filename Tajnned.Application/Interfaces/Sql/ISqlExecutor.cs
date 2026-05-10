using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.Interfaces.Sql
{
    public interface ISqlExecutor
    {
        Task<List<T>> QueryAsync<T>(
            string sql,
            params SqlParameter[] parameters
        ) where T : class;

        Task<T?> QuerySingleAsync<T>(
            string sql,
            params SqlParameter[] parameters
        ) where T : class;

        Task<int> ExecuteAsync(
            string sql,
            params SqlParameter[] parameters
        );
    }
}
