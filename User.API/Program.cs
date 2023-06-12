using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Reflection;
using User.API.Middleware;
using User.Domain.Helper;
using User.Domain.Interfaces;
using User.Infraestructure.Context;
using User.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string stringConnection = builder.Configuration.GetConnectionString("WebApiDatabase")!;
builder.Services.Configure<InfoDataBase>(InfoDataBase =>
{
    InfoDataBase.StringConnection = stringConnection;
});

builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.Converters.Add(new StringEnumConverter());
        x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo() { Title = "API Usuarios", Version = "v1" });
    opt.EnableAnnotations();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataContext>();
    await dbContext.Init();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ErrorHandlerMiddleware>();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
