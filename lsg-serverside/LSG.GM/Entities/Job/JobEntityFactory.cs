using AltV.Net.Data;
using LSG.DAL.Enums;
using LSG.GM.Economy.Jobs.Base.Courier;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Job
{
    public class JobEntityFactory
    {
        public JobEntity Create(JobEntityModel jobEnityModel)
        {
            switch (jobEnityModel.JobType)
            {
                case JobType.Courier: return new CourierJob(jobEnityModel);
                default:
                    throw new NotSupportedException($"Nie ma takiej {jobEnityModel.JobType} pracy dorywczej!");
            }
        }
    }
}
