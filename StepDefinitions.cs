using FluentAssertions;
using Reqnroll;
using ReqnrollTestProject.Extensions;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject;

[Binding]
public class StepDefinitions(IPageService pageService)
{
    private readonly IPageService _pageService = pageService;

    [Given("The Reqnroll page is loaded")]
    public async Task GivenTheReqnrollPageIsLoadedAsync()
    {
        await _pageService.HomePage.GoToPageAsync();
    }

    [When("The support button is clicked")]
    public async Task WhenTheSupportButtonIsClicked()
    {
        await _pageService.HomePage.ClickOnSupportButtonAsync();
    }

    [Then("The following textx are visible")]
    public async Task ThenTheFollowingTextxAreVisible(DataTable dataTable)
    {
        var tableRows = dataTable.ToDictionary();
        foreach (var row in tableRows)
        {
            var textMatches = await _pageService.SupportPage.TextContainsGivenValueAsync(row.Value);
            textMatches.Should().BeTrue();
        }
    }

}
