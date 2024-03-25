using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ReportRepository : IReportRepository
    {
        private DatabaseContext database;

        public ReportRepository(DatabaseContext database)
        {
            this.database = database;
        }
    }
}
