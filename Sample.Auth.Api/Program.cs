using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Sample.Auth.Api.Middleware;
using Sample.Auth.Api.Repositories;
using Sample.Auth.Api.Repositories.Interfaces;
using Sample.Auth.Api.Sinks;
using Sample.Auth.DataAccess.MsSql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ReadWriteDbConnection");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthDataAccessMsSqlModule(connectionString);

builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseFailureEvents = true;
	options.Events.RaiseSuccessEvents = true;
})
	.AddConfigurationStore(o =>
	{
		o.ConfigureDbContext = b => b.UseSqlServerWithCustomMigrationAssembly(connectionString);
	})
	.AddOperationalStore(o =>
	{
		o.ConfigureDbContext = b => b.UseSqlServerWithCustomMigrationAssembly(connectionString);
	})
	.AddDeveloperSigningCredential();

builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IEventSink, EventLogSink>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapDefaultControllerRoute();
});

app.MapControllers();

app.Run();