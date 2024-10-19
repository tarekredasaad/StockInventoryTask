using Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using StayCation.API.VerticalSlicing.Common.Constants;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.Middlewares;
using StayCation.API.VerticalSlicing.Common.Helpers;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using StayCation.API.VerticalSlicing.Data.Data;
using StayCation.API.VerticalSlicing.Data;
using StayCation.API.VerticalSlicing.Common.DTOs;
using Autofac.Core;
using Hangfire;
using StayCation.API.VerticalSlicing.Features.Product.LogLowStock.LogStockOrchastratorCommand;
using StayCation.API.VerticalSlicing.Features.Transaction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddDbContext<Context>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//    .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
//    .EnableSensitiveDataLogging();//.AddInterceptors(builder.Services.BuildServiceProvider().GetRequiredService<MyCustomInterceptor>());
//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Reservation System API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. " +
                        "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = JwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = JwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // Reduce the default clock skew (allowable token time discrepancy)
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Key))
    };
});

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
    //.Enrich.WithMachineName()
    //.Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
    //.WriteTo.Seq("http://localhost:5341/")
    .CreateLogger();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); // instead name of each one 
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddLogging();

builder.Services.AddHangfire(cfg =>
           cfg.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new AutofacModule()));



var app = builder.Build();



MapperHelper.Mapper = app.Services.GetService<IMapper>();
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<LogLowStockService>(
          
            recurringJobId: "log-low-stock",
    methodCall: x => x.LogLowStock(),
    cronExpression: Cron.Daily);
RecurringJob.AddOrUpdate<BackgroundTransactionService>(
          
            recurringJobId: "ArchivesTransactions",
    methodCall: x => x.ArchivesTransactions(),
    cronExpression: Cron.Yearly);
app.UseAuthorization();

app.MapControllers();

app.Run();
