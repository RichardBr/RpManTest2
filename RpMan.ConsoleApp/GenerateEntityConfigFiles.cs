using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpMan.ConsoleApp
{
    public static class GenerateEntityConfigFiles
    {

        public static string FilePartTop(string entity)
        {
            string ret = @"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpMan.Domain.Entities;

namespace RpMan.Persistence.Configurations
{
    public class {entity}Configuration : IEntityTypeConfiguration<{entity}>
    {
        public void Configure(EntityTypeBuilder<{entity}> builder)
        {
";
            ret = ret.Replace("{entity}", entity);
            return ret;
        }
        public static string FilePartBottom()
        {
            string ret = @"        }
    }
}";
            return ret;
        }

        public static void process()
        {
            var ReverseDbDataDir = @"C:\Users\dad\Documents\Visual Studio 2017\Projects\RpManTest2\RpMan.Persistence\ReverseDbData";
            var dbContextDir = ReverseDbDataDir + @"\Context\";
            var entityConfigDir = dbContextDir + @"\Configuration\";
            var dbContextFile = dbContextDir + @"RpManDbContext.cs";

            try
            {
                StreamReader sr = new StreamReader(dbContextFile);

                var readline = sr.ReadLine();  //Read the first line of text

                Boolean entityBeingProcessed = false;
                var outputLines = new List<string>();
                string entityName = null;

                while (readline != null)  // Continue to read until you reach end of file
                {
                    if (readline.Replace(" ", "") == "});")
                    {
                        entityBeingProcessed = false;
                        outputLines.Add(FilePartBottom());
                        System.IO.File.WriteAllLines(entityConfigDir + entityName + "Configuration.cs", outputLines);
                        outputLines.Clear();

                        readline = sr.ReadLine(); // Read the next line
                        continue;
                    }

                    if (readline.Contains("modelBuilder.Entity<"))
                    {
                        entityBeingProcessed = true;
                        var lineParts = readline.Split(new Char[] { '<', '>' });
                        entityName = lineParts[1];

                        outputLines.Add(FilePartTop(entityName));

                        readline = sr.ReadLine(); // Read the next line
                        readline = sr.ReadLine(); // Read the next line
                        continue;
                    }

                    if (!entityBeingProcessed)
                    {
                        readline = sr.ReadLine(); // Read the next line
                        continue;
                    }

                    outputLines.Add(readline.Replace("entity.", "builder."));

                    readline = sr.ReadLine(); //Read the next line
                }
                if (entityBeingProcessed)
                {
                    outputLines.Add(FilePartBottom());
                    System.IO.File.WriteAllLines(entityConfigDir + entityName + "Configuration.cs", outputLines);
                }

                sr.Close();
                // Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }


    }
}
