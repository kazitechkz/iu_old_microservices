using Microsoft.Extensions.Logging;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Infrastructure.Database
{
    public class DbSeeder
    {
        public async static Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<DbSeeder>();
            try
            {
               
                if(!context.Languages.Any())
                {
                    await context.Languages.AddRangeAsync(
                        new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                TitleRu = "Казахский",
                                TitleEn = "Kazakh",
                                TitleKk = "Қазақша",
                                Code = "kk"
                            },
                            new LanguageModel
                            {
                                TitleRu = "Русский",
                                TitleEn = "Russian",
                                TitleKk = "Орыс",
                                Code = "ru"
                            },
                            new LanguageModel
                            {
                                TitleRu = "Английский",
                                TitleEn = "English",
                                TitleKk = "Ағылшын",
                                Code = "en"
                            },
                         }
                        );
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeding Data");
                }
                else
                {
                    logger.LogInformation("Everything up to update - Seeding");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

            }
        }


    }
}
