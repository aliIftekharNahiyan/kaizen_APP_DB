using Kaizen.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices(services =>
      {
          services.AddSingleton<HttpClient>();
          services.AddDbContext<KaizenDbContext>(
              x =>
              {
                  var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:Default");
                  x.UseSqlServer(connectionString);
              });
      }
     )
    .Build();

host.Run();
