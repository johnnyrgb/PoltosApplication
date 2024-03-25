using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ReportService : IReportService
    {
        private IDbRepository database;

        public ReportService(IDbRepository database)
        {
            this.database = database;
        }

    }
}
