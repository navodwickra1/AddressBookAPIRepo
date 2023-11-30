var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//run swagger at startup
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baha'i Prayers API");
    c.InjectStylesheet("/swagger/custom.css");
    c.RoutePrefix = String.Empty;
});

app.MapControllers();
app.Run();
