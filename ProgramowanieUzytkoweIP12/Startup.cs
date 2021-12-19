
using CQRS;
using CQRS.AuthorFiles.Commands;
using CQRS.AuthorFiles.Handlers;
using CQRS.AuthorFiles.Queries;
using CQRS.BookFiles.Commands;
using CQRS.BookFiles.Handlers;
using CQRS.BookFiles.Queries;
using CQRS.Helpers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.DTOs;
using Model.Models;
using Nest;
using ProgramowanieUzytkoweIP12.Configuration;
using RepositoryPattern;
using RepositoryPattern.Helpers;
using System;
using System.Collections.Generic;

namespace ProgramowanieUzytkoweIP12
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

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<IElasticClient>(x =>
                    new ElasticClient(new ElasticConnection(new Uri("http://localhost:9200")))
);

            var assembly = AppDomain.CurrentDomain.Load("MediatRProject");
            services.AddMediatR(assembly);

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBooksHelper, BooksHelper>();
            services.AddScoped<IAuthorsHelper, AuthorsHelper>();
            services.AddScoped<IElasticHelper, ElasticHelper>();

            services.AddScoped<CommandBus>();
            services.AddScoped<QueryBus>();

            services.AddScoped<ICommandHandler<AddBookCommand>, AddBookCommandHandler>();
            services.AddScoped<ICommandHandler<AddAuthorCommand>, AddAuthorCommandHandler>();

            services.AddScoped<ICommandHandler<AddBookRateCommand>, AddBookRateCommandHandler>();
            services.AddScoped<ICommandHandler<AddAuthorRateCommand>, AddAuthorRateCommandHandler>();

            services.AddScoped<ICommandHandler<DeleteBookCommand>, DeleteBookCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteAuthorCommand>, DeleteAuthorCommandHandler>();

            services.AddScoped<IQueryHandler<GetBooksQuery, List<GetBookDTO>>, GetBooksQueryHandler>();
            services.AddScoped<IQueryHandler<GetAuthorsQuery, List<GetAuthorDTO>>, GetAuthorsQueryHandler>();
            services.AddScoped<IQueryHandler<GetBookQuery, GetBookDTO>, GetBookQueryHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Programowanie U�ytkowe API");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
