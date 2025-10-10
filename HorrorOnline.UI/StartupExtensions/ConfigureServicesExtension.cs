using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using HorrorOnline.Core.Services.Stories;
using HorrorOnline.Core.Services.Tags;
using HorrorOnline.Infrastructure.Repositories;

namespace HorrorOnline.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddScoped<IStoryAdderService, StoryAdderService>();
            services.AddScoped<IStoryGetterService, StoryGetterService>();
            services.AddScoped<IStoryDeleterService, StoryDeleterService>();

            services.AddSingleton<IStoryRepository, StoryRepository>();

            services.AddScoped<ITagAdderService, TagAdderService>();
            services.AddScoped<ITagGetterService, TagGetterService>();
            services.AddScoped<ITagDeleterService, TagDeleterService>();

            services.AddSingleton<ITagRepository, TagRepository>();

            return services;
        }
    }
}
