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

    [Test]
    [Category("Sad")]
    public void GivenIAmOnTheHomePage_WhenIEnterAValidEmailAndInvalidPassword_ThenIStayOnTheHomePage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();
        // Navigate to home page
        SL_Website.SL_HomePage.VisitHomePage();
        // Enter valid username
        SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
        // Enter invalid password
        SL_Website.SL_HomePage.EnterPassword("InvalidPassword");
        // Click login button
        SL_Website.SL_HomePage.ClickLoginButton();
        // Check landing page is correct
        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.BaseUrl));
    }

    [Test]
    [Category("Sad")]
    public void GivenIAmOnTheHomePage_WhenIEnterAnInvalidEmailAndValidPassword_ThenIStayOnTheHomePage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();
        // Navigate to home page
        SL_Website.SL_HomePage.VisitHomePage();
        // Enter valid username
        SL_Website.SL_HomePage.EnterUserName("InvalidUsername");
        // Enter invalid password
        SL_Website.SL_HomePage.EnterPassword(AppConfigReader.Password);
        // Click login button
        SL_Website.SL_HomePage.ClickLoginButton();
        // Check landing page is correct
        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.BaseUrl));
    }

    [Test]
    [Category("Sad")]
    public void GivenIAmOnTheHomePage_WhenIEnterAnInvalidEmailAndInvalidPassword_ThenIStayOnTheHomePage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();
        // Navigate to home page
        SL_Website.SL_HomePage.VisitHomePage();
        // Enter valid username
        SL_Website.SL_HomePage.EnterUserName("InvalidUsername");
        // Enter invalid password
        SL_Website.SL_HomePage.EnterPassword("InvalidPassword");
        // Click login button
        SL_Website.SL_HomePage.ClickLoginButton();
        // Check landing page is correct
        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.BaseUrl));
    }

    [Test]
    [Category("Sad")]
    public void GivenIAmOnTheHomePage_WhenIPressLoginButton_ThenIGetAnErrorMessage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();
        // Navigate to home page
        SL_Website.SL_HomePage.VisitHomePage();
        // Click login button
        SL_Website.SL_HomePage.ClickLoginButton();
        // Check error message is correct
        Assert.That(SL_Website.SL_HomePage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username is required"));
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

public class SL_Inventory_Tests
{
    private SL_Website<ChromeDriver> SL_Website = new();

    [Test]
    [Category("Happy")]
    public void GivenIAmOnTheInventoryPage_WhenIClickOnTheCart_ThenIShouldGoToTheCartUrl()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();

        SL_Website.SL_InventoryPage.VisitInventoryPage();

        SL_Website.SL_InventoryPage.PressCartIcon();

        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo("https://www.saucedemo.com/cart.html"));
    }

    [Test]
    [Category("Happy")]
    public void GivenIAmOnTheInventoryPage_WhenIClickOnTheCartThenContinueShopping_ThenIShouldGoBackToTheInventoryPage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();

        SL_Website.SL_InventoryPage.VisitInventoryPage();

        SL_Website.SL_InventoryPage.PressCartIcon();
        
        SL_Website.SL_InventoryPage.ContinueShopping();

        Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
    }

    [Test]
    [Category("Happy")]
    public void GivenIAmOnTheInventoryPage_WhenIAddAllItemsThenRemoveBackpack_ThenCartNumberShouldBe5()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();

        SL_Website.SL_InventoryPage.VisitInventoryPage();

        SL_Website.SL_InventoryPage.AddAllItemsToCart();

        SL_Website.SL_InventoryPage.RemoveSauceLabsBackpackFromCart();

        Assert.That(SL_Website.SL_InventoryPage.CartItemNumber, Is.EqualTo("5"));
    }

    [Test]
    [Category("Sad")]
    public void GivenIAmOnTheInventoryPage_WhenICheckoutAndContinueWithoutTypingLoginDetails_ThenIGetAnErrorMessage()
    {
        // Maximise browser
        SL_Website.SeleniumDriver.Manage().Window.Maximize();

        SL_Website.SL_InventoryPage.VisitInventoryPage();

        SL_Website.SL_InventoryPage.PressCartIcon();
        
        SL_Website.SL_InventoryPage.Checkout();
        
        SL_Website.SL_InventoryPage.CheckoutContinue();

        var error = SL_Website.SL_InventoryPage.CheckoutContinueError();

        Assert.That(error, Is.EqualTo("Error: First Name is required"));
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
