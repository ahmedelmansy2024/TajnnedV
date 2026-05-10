using System;
using System.Collections.Generic;
using System.Text;
using Tajnned.Application.Interfaces.Hangfire;
using Tajnned.Application.Interfaces.Sql;

namespace Tajnned.Infrastructure.BackgroundJobs
{
    public class JobService : IJobService
    {
        private readonly ISqlExecutor _sql;

        public JobService(ISqlExecutor sql)
        {
            _sql = sql;
        }

        public async Task ExecuteAsync(string jobName)
        {
            switch (jobName)
            {
                case "SyncUsers":
                    await _sql.ExecuteAsync("EXEC sp_SyncUsers");
                    break;

                case "UpdateApplications":
                    await _sql.ExecuteAsync("EXEC sp_UpdateApplications");
                    break;

                case "CleanupLogs":
                    await _sql.ExecuteAsync("EXEC sp_CleanupLogs");
                    break;

                default:
                    throw new Exception("Job not found");
            }
        }
    }
}
