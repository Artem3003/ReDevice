using ReDevice.Business;
using ReDevice.Database;
using ReDevice.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register 4-layer architecture dependencies
builder.Services.AddDatabaseLayer(builder.Configuration);
builder.Services.AddPersistenceLayer();
builder.Services.AddBusinessLayer();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:4200"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AngularClient");
app.UseAuthorization();
app.MapControllers();

app.Run();
