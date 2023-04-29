using Berry.DB;
using Berry.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog;
using System.Reflection;

namespace Berry.src
{
    public class Berry
    {

        private static void InitialiseDatabase (BerryDbContext berryDbContext)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Berry.Models.initialise.sql"))
            using (var reader = new StreamReader(stream!))
            {
                string query = reader.ReadToEnd();
                _ = berryDbContext.Database.ExecuteSql($"{query}");
            }
        }
    }
}