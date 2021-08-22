using System.IdentityModel.Tokens.Jwt;

using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using identity.API.Configuration;
using identity.API.Data;
using identity.API.Data.TenantConfiguration;
using identity.API.Data.TenantUser;
using identity.API.Repositories;
using identity.API.Repositories.IdentityService;
using identity.API.Repositories.Profile;
using identity.API.Data.Feature;
using identity.API.Repositories.TenantIdentity;

namespace identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AddDeveloperSigningCredential not something we want to use in a production environment;
            // For the production environment, you should use the AddSigningCredentials method and provide a valid certificate.
            services.AddIdentityServer()
                .AddProfileService<ProfileService>()
                .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
                //.AddTestUsers(InMemoryConfig.GetUsers())
                //.AddInMemoryClients(InMemoryConfig.GetClients())
                .AddDeveloperSigningCredential();

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", opt =>
           {
               opt.RequireHttpsMetadata = false;
               opt.Authority = "http://localhost:5000";
               opt.Audience = "IsAllowedToAccessMircoservice";
           });

            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "identity.API", Version = "v1" });
            });

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IClientStore, ClientStore>();

            services.AddScoped<IConfigurationContext, ConfigurationContext>();
            services.AddScoped<ITokenUserContext, TokenUserContext>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IFeatureContext, FeaturesContext>();

            services.AddScoped<ITenantIdentityRepository, TenantIdentityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "identity.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentityServer();
        }
    }
}
