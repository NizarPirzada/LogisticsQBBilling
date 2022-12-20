using FTBusiness.BaseRepository;
using FTData.Context;
using FTData.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class DriverRepo : Repository<Driver>
    {
        public DriverRepo(SLCContext dbContext) : base(dbContext)
        {
        }


    }
}
