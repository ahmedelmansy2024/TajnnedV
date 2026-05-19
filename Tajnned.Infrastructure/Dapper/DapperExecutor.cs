using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tajnned.Application.Interfaces.Dapper;
using Tajnned.Application.Interfaces.Sql;
using Tajnned.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Tajnned.Infrastructure.Dapper
{


    public class DapperExecutor : IDapperExecutor
    {
        private readonly AppDbContext _context;

        public DapperExecutor(AppDbContext context)
        {
            _context = context;
        }
        private IDbConnection Connection => _context.Database.GetDbConnection();

        private void EnsureOpen()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }
        public List<T> Query<T>(string sql, object? param = null)
        {
            EnsureOpen();

            return Connection.Query<T>(sql, param).ToList();
        }

        public T? QuerySingle<T>(string sql, object? param = null)
        {
            EnsureOpen();

            //return Connection.QuerySingle<T>(sql, param);
            return Connection.QueryFirstOrDefault<T>(sql, param);
        }



        public List<List<dynamic>> QueryMultiple(string storedProcedure, object? param = null)
        {
            EnsureOpen();

            using var multi = Connection.QueryMultiple(
                storedProcedure,
                param,
                commandType: CommandType.StoredProcedure
            );

            var results = new List<List<dynamic>>();

            while (!multi.IsConsumed)
            {
                var table = multi.Read().ToList(); // dynamic
                results.Add(table);
            }

            return results;
        }
        public int Execute(string sql, object? param = null)
        {
            EnsureOpen();
            return Connection.Execute(sql, param);
        }
    }
}