using CqrsTodo.Command.Concrete;
using CqrsTodo.Command.Dispatcher.Abstract;
using CqrsTodo.Command.Dispatcher.Concrete;
using CqrsTodo.Command.Handler.Abstract;
using CqrsTodo.Command.Handler.Concrete;
using CqrsTodo.EF;
using CqrsTodo.Models;
using CqrsTodo.Query.Concrete;
using CqrsTodo.Query.Dispatcher.Abstract;
using CqrsTodo.Query.Dispatcher.Concrete;
using CqrsTodo.Query.Handler.Abstract;
using CqrsTodo.Query.Handler.Concrete;
using CqrsTodo.SignalR;
using CqrsTodo.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTodo
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
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(connection));

            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<Todo>, TodoValidator>();
            services.AddSignalR();

            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

            services.AddTransient<ICommandHandler<CreateTodo>, CreateTodoHandler>();
            services.AddTransient<ICommandHandler<DeleteTodo>, DeleteTodoHandler>();
            services.AddTransient<ICommandHandler<UpdateTodo>, UpdateTodoHandler>();
            services.AddTransient<ICommandHandler<MakeComplete>, MakeCompleteHandler>();

            services.AddTransient<IQueryHandler<GetAllTodo, Task<IEnumerable<Todo>>>, GetAllTodoHandler>();
            services.AddTransient<IQueryHandler<GetTodoById, Task<Todo>>, GetTodoByIdHandler>();
            services.AddTransient<IQueryHandler<GetTodoCount, Task<int>>, GetTodoCountHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSignalR(b => b.MapHub<Notifier>("hub"));
        }
    }
}
