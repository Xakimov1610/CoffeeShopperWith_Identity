using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var assebly = typeof(Program).Assembly.GetName().Name;
var defaultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assebly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assebly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
