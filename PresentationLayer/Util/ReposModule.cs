using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;


namespace PresentationLayer.Util
{
    //public class ReposModule : NinjectModule
    //{
    //    private DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
    
    //    public ReposModule(string connectionString)
    //    {
    //        optionsBuilder.UseSqlite(connectionString);
    //    }
    
    //    public override void Load()
    //    {
    //        Bind<IDbRepository>().To<DbRepository>().InSingletonScope().WithConstructorArgument(optionsBuilder);
    //    }
    //}
    public class ReposModule : Module
    {
        //private DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        private string connectionString;
        public ReposModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbRepository>().As<IDbRepository>().SingleInstance().WithParameter(new TypedParameter(typeof(string), connectionString)); ;
            builder.RegisterType<ReportService>().As<IReportService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<GoalService>().As<IGoalService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<TransactionCategoryService>().As<ITransactionCategoryService>();
        }
    }

}
