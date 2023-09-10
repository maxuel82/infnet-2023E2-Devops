

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Aula20230826.HealthCheck;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add Health Check
builder.Services.AddHealthChecks()
                .AddUrlGroup(new Uri("http://httpbin.org/status/200"), "Api Terceiro n�o autenticada")
                .AddUrlGroup(uri: new Uri("http://viacep.com.br/ws/01001000/json/"), "Api Publica Cep n�o autenticada")
                .AddCheck<HealthCheckRandom>(name: "Api Terceiro Autenticada");
                

builder.Services.AddHealthChecksUI(s =>
{
    s.AddHealthCheckEndpoint("Infnet API", "https://localhost:7194/healthz");
    
})
.AddInMemoryStorage();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting()
   .UseEndpoints(config =>
   {
       config.MapHealthChecks("/healthz", new HealthCheckOptions
       {
           Predicate = _ => true,
           ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
       });

       config.MapHealthChecksUI();
   });

app.Run();
