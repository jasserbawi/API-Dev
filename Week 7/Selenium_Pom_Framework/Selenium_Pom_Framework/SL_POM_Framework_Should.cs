using OpenQA.Selenium.Chrome;
using SL_Pom_Framework_Test.lib.pages;

namespace SL_Pom_Framework_Test;

public class SL_Signin_Tests
{
    private SL_Website<ChromeDriver> SL_Website = new();

    [Test]
    [Category("Happy")]
    public void GivenIAmOnTheHomePage_WhenIEnterAValidEmailAndValidPassword_ThenIShouldLandOnTheInventoryPage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();
        // Navigate to home page
        SL_Website.SL_HomePage.VisitHomePage();
        // Enter valid username
        SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
        // Enter valid password
        SL_Website.SL_HomePage.EnterPassword(AppConfigReader.Password);
        // Click login button
        SL_Website.SL_HomePage.ClickLoginButton();
        // Check landing page is correct
        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.InventoryPageUrl));
    }

    // Will run once after all tests have finished
    [OneTimeTearDown]
    public void CleanUp()
    {
        // Quite the driver, closing associated window
        SL_Website.SeleniumDriver.Quit();
        // Releases unmanaged resources
        SL_Website.SeleniumDriver.Dispose();
    }
}
