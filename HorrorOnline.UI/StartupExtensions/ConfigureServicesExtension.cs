using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using HorrorOnline.Core.Services.Stories;
using HorrorOnline.Core.Services.Tags;
using HorrorOnline.Infrastructure.DbContext;
using HorrorOnline.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HorrorOnline.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            // Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>
                (
                    options =>
                    {
                        options.Password.RequiredLength = 5;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireDigit = false;
                    }
                )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
                .AddDefaultTokenProviders();

            // Authorization
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.AddPolicy("NotAuthenticated", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.Identity.IsAuthenticated == false;
                    });
                });
            });

            // Services and repos
            services.AddScoped<IStoryAdderService, StoryAdderService>();
            services.AddScoped<IStoryGetterService, StoryGetterService>();
            services.AddScoped<IStoryDeleterService, StoryDeleterService>();

            services.AddScoped<IStoryRepository, StoryRepository>();

            services.AddScoped<ITagAdderService, TagAdderService>();
            services.AddScoped<ITagGetterService, TagGetterService>();
            services.AddScoped<ITagDeleterService, TagDeleterService>();

            services.AddScoped<ITagRepository, TagRepository>();

            // Database
            if (environment.IsEnvironment("Test") == false)
            {
                IServiceCollection serviceCollection = services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            }

            return services;
        }
    }
}
