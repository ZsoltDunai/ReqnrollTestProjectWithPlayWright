using Microsoft.Playwright;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject.Pages;

public class HomePage(IPageDependencyService pageDependencyService)
{
    private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

    public ILocator HeaderNavigationOptions => _pageDependencyService.Page.Result.Locator("[class=' wp-block-navigation-item wp-block-navigation-link']");

    public async Task GoToPageAsync()
    {
        await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.UiUrl);
    }

    public async Task ClickOnSupportButtonAsync()
    {
        var supportButton = await ReturnSupportButtonFromHeaderOptionsAsync();

        await supportButton.ClickAsync();
    }

    private async Task<IElementHandle> ReturnSupportButtonFromHeaderOptionsAsync()
    {
        foreach (var option in await HeaderNavigationOptions.ElementHandlesAsync())
        {
            var spanElement = await option.QuerySelectorAsync("span");
            var optionText = await spanElement?.TextContentAsync();

            if (optionText.Equals("support", StringComparison.InvariantCultureIgnoreCase))
            {
                return option;
            }
        }
       
        return null;
    }
}
