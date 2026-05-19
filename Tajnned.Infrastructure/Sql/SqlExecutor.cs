using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tajnned.Application.Interfaces.Sql;
using Tajnned.Infrastructure.Data;

namespace Tajnned.Infrastructure.Sql
{
    public class SqlExecutor : ISqlExecutor
    {
        private readonly AppDbContext _context;

        public SqlExecutor(AppDbContext context)
        {
            _context = context;
        }

        // SELECT MANY
        public async Task<List<T>> QueryAsync<T>(
            string sql,
            params SqlParameter[] parameters
        ) where T : class
        {
            return await _context.Database
                .SqlQueryRaw<T>(sql, parameters)
                .ToListAsync();
        }

        // SELECT ONE
        public async Task<T?> QuerySingleAsync<T>(
            string sql,
            params SqlParameter[] parameters
        ) where T : class
        {
            //return  await _context.Database
            //    .SqlQueryRaw<T>(sql, parameters).FirstOrDefaultAsync();
            return _context.Database
            .SqlQueryRaw<T>(sql, parameters).AsEnumerable().FirstOrDefault();

        }


      
        // INSERT / UPDATE / DELETE
        public async Task<int> ExecuteAsync(
            string sql,
            params SqlParameter[] parameters
        )
        {
            return await _context.Database
                .ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<List<T>> QueryMultipleAsync<T>(string sql, params SqlParameter[] parameters) where T : class
        {
         var x =   await  _context.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
            return await _context.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();   
            //  => await _entitySet.FromSqlRaw(sql, parameters).ToListAsync();

            // _entitySet.Set<LMS_SurveyTraineeViewModel>().FromSql("LMSSP_GetTraineeSurvey @surveyID = {0},@LanguageID = {1}", surveyID, AppTenant.SelectedLanguageID).ToList();

        }
    }
    //public class SqlExecutor : ISqlExecutor
    //{
    //    private readonly AppDbContext _context;

    //    public SqlExecutor(AppDbContext context)
    //    {
    //        _context = context;
    //    }


    //    // SELECT MANY
    //    public async Task<List<T>> QueryAsync<T>(
    //        string sql,
    //        params SqlParameter[] parameters
    //    ) where T : class
    //    {
    //        return await _context.Set<T>()
    //            .FromSqlRaw(sql, parameters)
    //            .AsNoTracking()
    //            .ToListAsync();
    //    }

    //    // SELECT ONE
    //    public async Task<T?> QuerySingleAsync<T>(
    //        string sql,
    //        params SqlParameter[] parameters
    //    ) where T : class
    //    {
    //        return await _context.Set<T>()
    //            .FromSqlRaw(sql, parameters)
    //            .AsNoTracking()
    //            .FirstOrDefaultAsync();
    //    }

    //    // INSERT / UPDATE / DELETE
    //    public async Task<int> ExecuteAsync(
    //        string sql,
    //        params SqlParameter[] parameters
    //    )
    //    {
    //        return await _context.Database
    //            .ExecuteSqlRawAsync(sql, parameters);
    //    }

    //}
}
