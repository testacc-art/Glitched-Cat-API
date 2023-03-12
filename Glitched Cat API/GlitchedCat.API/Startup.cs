using System;
using System.Collections.Generic;
using System.Reflection;
using GlitchedCat.Application.Behaviors;
using GlitchedCat.Infra.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Handlers;
using GlitchedCat.Application.Mapping;
using GlitchedCat.Application.Queries.Blog;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Common.Logging;
using GlitchedCat.Domain.Common.Models.Blog;
using GlitchedCat.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GlitchedCat.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePostCommand).GetTypeInfo().Assembly));
            
            // Register the MediatR pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Register the MediatR handlers
            services.AddScoped<IRequestHandler<CreatePostCommand, Guid>, CreatePostCommandHandler>();
            services.AddScoped<IRequestHandler<DeletePostCommand>, DeletePostCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllPostsQuery, IEnumerable<PostResponse>>, GetAllPostsQueryHandler>();
            services.AddScoped<IRequestHandler<GetPostByIdQuery, PostResponse>, GetPostByIdQueryHandler>();
            services.AddScoped<IRequestHandler<SearchPostsQuery, IEnumerable<PostResponse>>, SearchPostsQueryHandler>();
            services.AddScoped<IRequestHandler<UpdatePostCommand>, UpdatePostCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>, GetAllUsersQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserResponse>, GetUserByIdQueryHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();

            services.AddTransient<IRequest<PostResponse>, GetPostByIdQuery>();
            services.AddTransient<IRequest<IEnumerable<PostResponse>>, SearchPostsQuery>();

            services.AddDbContext<BlogContext>();
            // services.AddEntityFrameworkSqlServer().AddDbContext<BlogContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlitchedCat API", Version = "v1" });
            });
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            #region Register Services
            services.AddScoped<IDomainService<Post>, PostService>();
            services.AddScoped<IDomainService<Comment>, CommentService>();
            services.AddScoped<IDomainService<User>, UserService>();
            #endregion


            #region Register Repos
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlitchedCat API V1");
            });

            app.UseRouting();
            
            app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
