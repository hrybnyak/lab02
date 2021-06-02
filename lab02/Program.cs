using lab02.CommandExecutor;
using lab02.Models;
using lab02.ObjManupilations;
using lab02.Renderer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            using var serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var execturor = provider.GetService<ICommandExecutor>();
            execturor?.ExecuteCommand(args);

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            services.AddTransient<IArgumentsListParser, ArgumentListParser>();
            services.AddTransient<IObjectLoader, ObjectLoader>();
            services.AddTransient<IMeshExtracter, MeshExtracter>();
            services.AddTransient<IImageCreator, ImageCreator>();
            services.AddTransient<IRayscaler, Rayscaler>();
            services.AddTransient<IShader, FlatShader>();
            services.AddTransient<IRenderer, Renderer.Renderer>();
            services.AddTransient<ICommandExecutor, CommandExecutor.CommandExecutor>();
        });
    }
}
