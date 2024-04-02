using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Microsoft.Data.SqlClient;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;
using System.Runtime;
using Org.BouncyCastle.Bcpg;

Meter s_meter = new("Metric", "1.0.0");
Counter<int> s_go_gc_duration = s_meter.CreateCounter<int>(
	name: "hats-sold",
	unit: "Hats",
	description: "The number of hats sold in our store");

s_meter.CreateObservableCounter(
	"process.runtime.dotnet.jit.il_compiled.size",
	() => JitInfo.GetCompiledILBytes(),
	unit: "bytes",
	description: "Count of bytes of intermediate language that have been compiled since the process start.");

s_meter.CreateObservableUpDownCounter(
	"process.runtime.dotnet.gc.objects.size",
	() => GC.GetTotalMemory(false),
	unit: "bytes",
	description: "Count of bytes currently in use by objects in the GC heap that haven't been collected yet. Frafmentation and other GC committed memory pools are excluded.");

Main();

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddDbContext<Hotels.Models.HotelsContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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

    app.Run();
void Main()
{
	using MeterProvider meterProvider = Sdk.CreateMeterProviderBuilder()
			.AddMeter("Mertic")
			.AddPrometheusHttpListener(options => options.UriPrefixes = new string[] { "http://localhost:9184/" })
			.Build();

	var rand = Random.Shared;
	Console.WriteLine("Press any key to exit");
	while (!Console.KeyAvailable)
	{
		//// Simulate hat selling transactions.
		Thread.Sleep(rand.Next(100, 2500));
		s_go_gc_duration.Add(rand.Next(0, 1000));
	}
}