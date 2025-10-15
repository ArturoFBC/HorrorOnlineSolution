using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.ServiceContracts.Tags;
using HorrorOnline.Core.Services.Stories;
using HorrorOnline.Core.Services.Tags;
using HorrorOnline.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

            services.AddScoped<IStoryAdderService, StoryAdderService>();
            services.AddScoped<IStoryGetterService, StoryGetterService>();
            services.AddScoped<IStoryDeleterService, StoryDeleterService>();

            services.AddSingleton<IStoryRepository, StoryRepository>();

            services.AddScoped<ITagAdderService, TagAdderService>();
            services.AddScoped<ITagGetterService, TagGetterService>();
            services.AddScoped<ITagDeleterService, TagDeleterService>();

            services.AddSingleton<ITagRepository, TagRepository>();

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

            return services;
        }
    }
}
