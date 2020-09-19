using GraphQL.Server;
using GraphQL.Types;
using GraphQl_V2.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace GraphQl_V2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TestType>();

            services.AddScoped<TestQuery>();

            services.AddSingleton<ISchema, TestSchema>();

            services.AddLogging(builder => builder.AddConsole());
            
            services.AddHttpContextAccessor();

            _ = services.AddGraphQL(options =>
              {
                  options.EnableMetrics = true;
                // var logger = options.GetRequiredService<ILogger<Startup>>();
                // options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);

            })
            .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(TestSchema));
            //.AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            app.UseWebSockets();

            app.UseGraphQLWebSockets<TestSchema>("/graphql");

            app.UseGraphQL<TestSchema>("/graphql");

            app.UseGraphQLPlayground();
        }
    }
}
