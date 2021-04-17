using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using TechnicalCore.Api.Data;
using TechnicalCore.Api.GraphQL;
using TechnicalCore.Api.GraphQL.Messaging;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddDbContext<TechnicalCoreDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("TechnicalCore")));

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleReviewRepository, ArticleReviewRepository>();
            services.AddSingleton<ReviewMessageService>();
            services.AddScoped<TechnicalCoreSchema>();

            //services.TryAddSingleton<ISchema>(s =>
            //{
            //    string definitions = @"
            //      type User {
            //        id: ID
            //        name: String
            //      }

            //      type Query {
            //        viewer: User
            //        users: [User]
            //      }
            //    ";
            //    var schema = Schema.For(definitions, builder => builder.Types.Include<Query>());
            //    schema.AllTypes["User"].AuthorizeWith("AdminPolicy");
            //    return schema;
            //});

            // extension method defined in this project
            //services.AddGraphQLAuth((settings, provider) => settings.AddPolicy("AdminPolicy", p => p.RequireClaim("role", "Admin")));

            // claims principal must look something like this to allow access
            // var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("role", "Admin") }));

            services
                .AddGraphQL((options, provider) =>
                {
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                // It is required when both GraphQL HTTP and GraphQL WebSockets middlewares are mapped to the same endpoint (by default 'graphql').
                .AddDefaultEndpointSelectorPolicy()
                // Add required services for GraphQL request/response de/serialization
                .AddSystemTextJson() // For .NET Core 3+
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                .AddGraphTypes(typeof(TechnicalCoreSchema), ServiceLifetime.Scoped)
                .AddWebSockets() // Add required services for web socket support
                 //.AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
                .AddDataLoader();

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors();
            
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechnicalCore.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TechnicalCoreDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnicalCore.Api v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //dbContext.Database.EnsureCreated();

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();
            //app.UseAuthorization();

            // this is required for websockets support
            app.UseWebSockets();
            // use websocket middleware for TechnicalCoreSchema at default path /graphql
            app.UseGraphQLWebSockets<TechnicalCoreSchema>();
            // use HTTP middleware for TechnicalCoreSchema at default path /graphql
            app.UseGraphQL<TechnicalCoreSchema>();
            // use GraphiQL middleware at default path /ui/graphiql with default options
            app.UseGraphQLGraphiQL();
            // use GraphQL Playground middleware at default path /ui/playground with default options
            app.UseGraphQLPlayground();
        }
    }
}
