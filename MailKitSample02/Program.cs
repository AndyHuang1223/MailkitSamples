using MailKitSample02.Models;
using MailKitSample02.Services;

namespace MailKitSample02;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Use the options pattern to load the configuration
        // https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#the-options-pattern
        builder.Services.Configure<MailServerSettings>(
            builder.Configuration.GetSection(MailServerSettings.MailServerSettingsKey));

        // Register the MailKitEmailSenderService in DI service.
        builder.Services.AddTransient<MailKitEmailSenderService>();
        
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

        app.Run();
    }
}