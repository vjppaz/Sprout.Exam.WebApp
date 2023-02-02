using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Tests
{
    public class Common
    {
        public static ApplicationDbContext CreateInMemoryContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var operationalStoreOptions = new OperationalStoreOptions();
            var optionsOps = Options.Create(operationalStoreOptions);

            return new ApplicationDbContext(options, optionsOps);
        }
    }
}
