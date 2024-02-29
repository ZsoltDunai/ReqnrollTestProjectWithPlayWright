using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using Reqnroll;
using ReqnrollTestProject.Settings;

namespace ReqnrollTestProject.Services;

public interface IPageDependencyService
{
    Task<IPage> Page { get; }
    IOptions<AppSettings> AppSettings { get; }
    ScenarioContext ScenarioContext { get; }
}

public class PageDependencyService : IPageDependencyService, IDisposable
{
    public PageDependencyService(Task<IPage> page, IOptions<AppSettings> appSettings, ScenarioContext scenarioContext)
    {
        Page = page;
        Page.Result.SetDefaultTimeout(240000);
        AppSettings = appSettings;
        ScenarioContext = scenarioContext;
    }

    public void Dispose()
    {
        Page.Result.Context.Browser?.CloseAsync();
    }

    public Task<IPage> Page { get; }
    public IOptions<AppSettings> AppSettings { get; }
    public ScenarioContext ScenarioContext { get; }
}
