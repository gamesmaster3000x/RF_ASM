using BerryMVC.Data;
using BerryMVC.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Reflection;

namespace BerryMVC.Common
{
    public class DbInitialiser
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static void Initialise (BerryMVCContext context)
        {
            LOGGER.Info("Initialising database...");
            context.Database.Migrate();
            context.SaveChanges();

            //ExecuteMigrationScripts(context);

            // Users
            if (!context.UserModels.Any())
            {
                var students = new UserModel[]
                {
                new UserModel{ Name="Admin" },
                };
                context.UserModels.AddRange(students);
                context.SaveChanges();
                LOGGER.Info("Initialised Users");
            }


            // Berries
            if (!context.BerryModel.Any())
            {
                var berries = new BerryModel[]
                {
                };
                context.BerryModel.AddRange(berries);
                context.SaveChanges();
                LOGGER.Info("Initialised Berries");
            }
        }

        private static void ExecuteMigrationScripts (BerryMVCContext context)
        {
            LOGGER.Info("Finding migration scripts...");

            Assembly a = Assembly.GetExecutingAssembly();
            string[] resources = a.GetManifestResourceNames();
            string[] names = resources
                .Where(name => name.StartsWith("BerryMVC.Migrations.Scripts"))
                .ToArray();

            int i = 0;
            foreach (string name in names)
            {
                LOGGER.Info($"Applying database migration {i++}/{names.Length} - {name}...");
                using (var stream = a.GetManifestResourceStream(name))
                using (var reader = new StreamReader(stream!))
                {
                    string script = reader.ReadToEnd();
                    FormattableString exec = $"exec ({script})";
                    int rows = context.Database.ExecuteSql(exec);
                    LOGGER.Debug($"{rows} rows affected.");
                }
            }
        }
    }
}
