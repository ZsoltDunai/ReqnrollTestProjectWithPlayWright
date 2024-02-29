using ReqnrollTestProject.Pages;

namespace ReqnrollTestProject.Services;

public interface IPageService
{
    HomePage HomePage { get; }
    SupportPage SupportPage { get; }
}

public class PagesService(HomePage homePage, SupportPage supportPage) : IPageService
{
    public HomePage HomePage { get; } = homePage;

    public SupportPage SupportPage { get; } = supportPage;
}
