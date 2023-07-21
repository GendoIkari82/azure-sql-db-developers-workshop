using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using System.Threading.Tasks;
using System.Text.Json;
namespace Person.Function;
public static class streamPeople
{
    [FunctionName("changeDataStream")]
    public static async Task RunAsync(
        [SqlTrigger("[dbo].[person]", "SqlConnectionString")]
            IReadOnlyList<SqlChange<Person>> changes,
        ILogger logger)
   {
      foreach (SqlChange<Person> change in changes)
      {
          var person = JsonSerializer.Serialize(change.Item);
          var message = $"{change.Operation} {person}";
          logger.LogInformation(message);
        }
    }
}