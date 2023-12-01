using AddressBookAPI.Data.Implementations;
using AddressBookAPI.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Logging.AddLog4Net(configuration.GetValue<string>("Log4netConfig"));
builder.Services.AddScoped<IDataRepo,FileDataRepo>();
builder.Services.AddScoped<IDataService, FileDataService>();
var app = builder.Build();

//run swagger at startup
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressBookAPI");
    c.InjectStylesheet("/swagger/custom.css");
    c.RoutePrefix = String.Empty;
});

app.MapControllers();
app.Run();
