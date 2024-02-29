using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using Reqnroll.Autofac;
using ReqnrollTestProject.Pages;
using ReqnrollTestProject.Services;
using ReqnrollTestProject.Settings;

namespace ReqnrollTestProject;

public static class TestStartup
{
    [ScenarioDependencies]
    public static void CreateServices(ContainerBuilder builder)
    {
        builder.RegisterConfiguration();
        builder.RegisterPlaywright();
        builder.RegisterAppSettings();
        builder.RegisterPages();
        builder.RegisterPagesHandler();
        builder.RegisterPageDependencyService();
        builder.RegisterSteps();
    }

    private static void RegisterSteps(this ContainerBuilder builder)
    {
        builder.RegisterType<StepDefinitions>().InstancePerDependency();
    }

    private static void RegisterConfiguration(this ContainerBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("Settings/appsettings.json", false, true)
            .Build();

        builder.RegisterInstance(configuration)
            .As<IConfiguration>()
            .SingleInstance();
    }

    private static void RegisterAppSettings(this ContainerBuilder builder)
    {
        builder.Register(c =>
        {
            var configuration = c.Resolve<IConfiguration>();
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            return Options.Create(appSettings);
        }).As<IOptions<AppSettings>>();
    }

    private static void RegisterPlaywright(this ContainerBuilder builder)
    {
        builder.Register(async _ =>
        {
            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 200
            }).ConfigureAwait(false);
            return await browser.NewPageAsync().ConfigureAwait(false);
        }).As<Task<IPage>>().InstancePerDependency();
    }

    private static void RegisterPages(this ContainerBuilder builder)
    {
        builder.RegisterType<HomePage>().AsSelf().InstancePerDependency();
        builder.RegisterType<SupportPage>().AsSelf().InstancePerDependency();
    }

    private static void RegisterPagesHandler(this ContainerBuilder builder)
    {
        builder.RegisterType<PagesService>().As<IPageService>().InstancePerLifetimeScope();
    }

    private static void RegisterPageDependencyService(this ContainerBuilder builder)
    {
        builder.RegisterType<PageDependencyService>().As<IPageDependencyService>().InstancePerLifetimeScope();
    }
}
