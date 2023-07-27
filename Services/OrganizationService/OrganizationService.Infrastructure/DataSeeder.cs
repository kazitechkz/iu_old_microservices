using Microsoft.Extensions.Logging;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure;

public class DataSeeder
{
    public static async Task SeedData(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Areas.Any())
                {
                    await context.Areas.AddRangeAsync(GetAreaRaw());
                }
                if (!context.LegalForms.Any())
                {
                    await context.LegalForms.AddRangeAsync(GetLegalFormRaw());
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataSeeder>();
                logger.LogError(ex.Message);
            }
            
            await context.SaveChangesAsync();
        }
    
    private static IEnumerable<Area> GetAreaRaw()
    {
        return new List<Area>
        {
            new Area()
            {
                Id = 1,
                TitleEn = "Astana",
                TitleKk = "Астана",
                TitleRu = "Астана"
            },
            new Area()
            {
                Id = 2,
                TitleEn = "Almaty",
                TitleKk = "Алматы",
                TitleRu = "Алматы"
            },
            new Area()
            {
                Id = 3,
                TitleEn = "Shymkent",
                TitleKk = "Шымкент",
                TitleRu = "Шымкент"
            },
            new Area()
            {
                Id = 4,
                TitleEn = "Abai Region",
                TitleKk = "Абай облысы",
                TitleRu = "Абайская область",
                
            },
            new Area()
            {
                Id = 5,
                TitleEn = "Aqmola Region",
                TitleKk = "Ақмола облысы",
                TitleRu = "Акмолинская область",
            },
            new Area()
            {
                Id = 6,
                TitleEn = "Aktobe Region",
                TitleKk = "Ақтөбе облысы",
                TitleRu = "Актюбинская область",
            },
            new Area()
            {
                Id = 7,
                TitleEn = "Almaty Region",
                TitleKk = "Алматы облысы",
                TitleRu = "Алматинская область",
            },
            new Area()
            {
                Id = 8,
                TitleEn = "Atyrau Region",
                TitleKk = "Атырау облысы",
                TitleRu = "Атырауская область",
            },
            new Area()
            {
                Id = 9,
                TitleEn = "East Kazakhstan Region",
                TitleKk = "Шығыс Қазақстан облысы",
                TitleRu = "Восточно-Казахстанская область",
            },
            new Area()
            {
                Id = 10,
                TitleEn = "Jambyl Region",
                TitleKk = "Жамбыл облысы",
                TitleRu = "Жамбылская область",
            },
            new Area()
            {
                Id = 11,
                TitleEn = "Jetisu Region",
                TitleKk = "Жетісу облысы",
                TitleRu = "Жетысуская область",
            },
            new Area()
            {
                Id = 12,
                TitleEn = "West Kazakhstan Region",
                TitleKk = "Батыс Қазақстан облысы",
                TitleRu = "Западно-Казахстанская область",
            },
            new Area()
            {
                Id = 13,
                TitleEn = "Karagandy Region",
                TitleKk = "Қарағанды облысы",
                TitleRu = "Карагандинская область",
            },
            new Area()
            {
                Id = 14,
                TitleEn = "Kostanay Region",
                TitleKk = "Қостанай облысы",
                TitleRu = "Костанайская область",
            },
            new Area()
            {
                Id = 15,
                TitleEn = "Kyzylorda Region",
                TitleKk = "Қызылорда облысы",
                TitleRu = "Кызылординская область",
            },
            new Area()
            {
                Id = 16,
                TitleEn = "Mangistau Region",
                TitleKk = "Маңғыстау облысы",
                TitleRu = "Мангистауская область",
            },
            new Area()
            {
                Id = 17,
                TitleEn = "Pavlodar Region",
                TitleKk = "Павлодар облысы",
                TitleRu = "Павлодарская область",
            },
            new Area()
            {
                Id = 18,
                TitleEn = "North Kazakhstan Region",
                TitleKk = "Солтүстік Қазақстан облысы",
                TitleRu = "Северо-Казахстанская область",
            },
            new Area()
            {
                Id = 19,
                TitleEn = "Turkestan Region",
                TitleKk = "Түркістан облысы",
                TitleRu = "Туркестанская область",
            },
            new Area()
            {
                Id = 20,
                TitleEn = "Ulytau Region",
                TitleKk = "Ұлытау облысы",
                TitleRu = "Улытауская область",
            },
        };
    }
    private static IEnumerable<LegalForm> GetLegalFormRaw()
    {
        return new List<LegalForm>
        {
            new LegalForm
            {
                TitleEn = "Legal Entity",
                TitleKk = "Заңды тұлға",
                TitleRu = "Юридическое лицо",
                Code = "legal"
            },
            new LegalForm
            {
                TitleEn = "Individual",
                TitleKk = "Жеке тұлға",
                TitleRu = "Физическое лицо",
                Code = "individual"
            }
        };
    }
}