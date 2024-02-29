using Microsoft.Playwright;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject.Pages;

public class SupportPage(IPageDependencyService pageDependencyService)
{
    private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

    public ILocator StrongTexts => _pageDependencyService.Page.Result.Locator("strong");

    public async Task<bool> TextContainsGivenValueAsync(string value)
    {
        foreach (var text in await StrongTexts.ElementHandlesAsync())
        {
            var textValue = await text.TextContentAsync();
            if (textValue.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }

        return false;
    }
}
