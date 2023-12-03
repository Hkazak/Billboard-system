using Microsoft.Extensions.FileProviders;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Configure logging
if (builder.Environment.IsProduction())
{
    builder.Host.ConfigureSerilog();
}

// Add services to the container.
var savedFilesFolderPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot/Pictures/Uploads");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureValidators();
builder.Services.ConfigureCqrs();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCache();
builder.Services.ConfigureFileProvider(savedFilesFolderPath);
builder.Services.ConfigureIoka(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

var options = new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(savedFilesFolderPath),
    RequestPath = "/pictures"
};
app.UseStaticFiles(options);

app.UseCors();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandling();

app.MapControllers();

app.Run();