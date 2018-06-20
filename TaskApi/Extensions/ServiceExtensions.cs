using Microsoft.Extensions.DependencyInjection;

namespace TaskApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
        }
    }
}