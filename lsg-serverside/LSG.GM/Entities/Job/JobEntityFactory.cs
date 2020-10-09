using AltV.Net.Data;
using LSG.DAL.Enums;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Economy.Jobs.Base.Junker;
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
                case JobType.Junker: return new JunkerJob(jobEnityModel);
                default:
                    throw new NotSupportedException($"Nie ma takiej {jobEnityModel.JobType} pracy dorywczej!");
            }
        }

        public int CreateBlip(JobType jobType)
        {
            switch (jobType)
            {
                case JobType.None: return 0;
                case JobType.Courier: return 616;
                case JobType.Junker: return 318;
                default:
                    throw new NotSupportedException($"Nie ma takiej pracy {jobType}");
            }
        }

        public int CreateColor(JobType jobType)
        {
            switch (jobType)
            {
                case JobType.None: return 0;
                case JobType.Courier: return 52;
                case JobType.Junker: return 56;
                default:
                    throw new NotSupportedException($"Nie ma takiej pracy {jobType}");
            }
        }
    }
}
