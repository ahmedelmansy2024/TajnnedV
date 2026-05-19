using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.Interfaces.Dapper
{
    public interface IDapperExecutor
    {
        List<T> Query<T>(string sql, object? param = null);

        T? QuerySingle<T>(string sql, object? param = null);

        int Execute(string sql, object? param = null);

        List<List<dynamic>> QueryMultiple(string storedProcedure, object? param = null);

    }
}
