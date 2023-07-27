using Microsoft.Extensions.Logging;

namespace AcademicYearService.Infrastructure;

public class DataSeeder
{
    public static async Task SeedData(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataSeeder>();
                logger.LogError(ex.Message);
            }
            
            await context.SaveChangesAsync();
        }
}