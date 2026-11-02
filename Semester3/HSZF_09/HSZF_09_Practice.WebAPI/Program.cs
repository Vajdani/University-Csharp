using HSZF_09_Practice.WebAPI.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

namespace HSZF_09_Practice.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();

            builder.Services.RegisterCustomServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieHandler API v1");
                });
            }

            //app.UseExceptionHandler(x => x.Run(async ctx =>
            //{
            //    var exception = ctx.Features.Get<IExceptionHandlerPathFeature>().Error;
            //    var response = new { Error = exception.Message }; // The whole exception cannot be passed here
            //    await ctx.Response.WriteAsJsonAsync(response);
            //}));
            app.UseExceptionHandler();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
