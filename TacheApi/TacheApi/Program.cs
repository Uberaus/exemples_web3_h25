using TacheApi.Models;
using TacheApi.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();
new AppDbContext(builder.Configuration).Database.EnsureCreated();

builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer<ExampleSchemaTransformer>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "";
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
