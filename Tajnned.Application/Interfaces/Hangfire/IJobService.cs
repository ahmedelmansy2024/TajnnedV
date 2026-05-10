using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.Interfaces.Hangfire
{
     
    public interface IJobService
    {
        Task ExecuteAsync(string jobName);
    }
}
