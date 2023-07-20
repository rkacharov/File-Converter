using FileConverter.Api.Options;
using FileConverter.Api.Services;
using FileConverter.Api.Services.FileConverter;
using FileConverter.Api.Services.FileConverter.Base;
using FileConverter.Api.Services.FileStorage;
using FileConverter.Api.Services.FileStorage.Base;
using FileConverter.Api.Services.FileValidator;
using FileConverter.Api.Services.FileValidator.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PhysicalStorageOptions>(
    builder.Configuration.GetSection(PhysicalStorageOptions.PhysicalStorage));

builder.Services.AddScoped<XmlFileValidator>();
builder.Services.AddScoped<XmlToJsonFileConverter>();
builder.Services.AddScoped<PhysicalStorageFileManager>();

builder.Services.AddScoped<IFileConverterFactory, FileConverterFactory>();
builder.Services.AddScoped<IFileValidatorFactory, FileValidatorFactory>();
builder.Services.AddScoped<IFileManagerFactory, FileManagerFactory>();
builder.Services.AddScoped<IFileServiceFactory, FileServiceFactory>();

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

var logger = new LoggerFactory().CreateLogger<Program>();
try
{
    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Application start-up failed");
}