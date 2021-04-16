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
using Microsoft.OpenApi.Models;
using TechnicalCore.Api.Data;
using TechnicalCore.Api.GraphQL;
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
                .AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(TechnicalCoreSchema), ServiceLifetime.Scoped)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<TechnicalCoreSchema>();
            //app.UseGraphQLGraphiQL();
            app.UseGraphQLPlayground(new PlaygroundOptions());
        }
    }
}
