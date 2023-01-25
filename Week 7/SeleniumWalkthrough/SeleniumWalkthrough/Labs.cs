using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace SeleniumWalkthrough;

public class LabTests
{
    [Test]
    [Category("Login Page")]
    public void GivenIAmOnLoginPage_WhenIEnterAValidUsernameAndValidPassword_ThenIShouldLandOnTheSecurePage()
    {
        //Given that I am on the login page
        //When I enter a valid username and valid password
        //Then I should land on the secure page
        
        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

            //Grab the username field
            var userNameField = driver.FindElement(By.Id("username"));

            //Enter username into it
            userNameField.SendKeys("tomsmith");

            //Grab the password field
            var passwordField = driver.FindElement(By.Id("password"));

            //Enter password into it
            passwordField.SendKeys("SuperSecretPassword!");

            //Click the sign-in button
            driver.FindElement(By.ClassName("radius")).Click();

            //Assert that we are not on the sign-in page 
            Assert.That(driver.Url, Is.EqualTo("https://the-internet.herokuapp.com/secure"));
        }
    }

    [Test]
    [Category("Status Code")]
    public void GivenIAmOnStatusCodesPage_WhenIPressOnThe200HyperLink_ThenIGetRedirectedToThe200StatusCodePage()
    {
        //Given that I am on the status codes page
        //When I enter on the 200 hyperlink
        //Then I should land on a 200 status code page
        
        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/status_codes");

            //Click on 200 hyperlink
            driver.FindElement(By.ClassName("example")).FindElement(By.LinkText("200")).Click();

            Assert.That(driver.Url, Is.EqualTo("https://the-internet.herokuapp.com/status_codes/200"));
        }
    }

    [Ignore("ERROR")]
    [Test]
    [Category("Geolocation")]
    public void GivenIAmOnTheGeolocationPage_WhenIClickTheWhereAmIButton_ThenIGetASpecificLatitudeAndLongitude()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/geolocation");

            //Click on the where am I button
            driver.FindElement(By.ClassName("example")).FindElement(By.TagName("button")).Click();

            //Latitude information
            var latitude = driver.FindElement(By.ClassName("example")).FindElement(By.Id("demo")).FindElement(By.Id("lat-value"));

            Assert.That(latitude.Text, Is.EqualTo("51.593216"));
        }
    }

    [Test]
    [Category("Key Presses")]
    public void GivenIAmOnTheKeyPressesPage_WhenITypeB_ThenIShouldGetAMessageSayingITypedB()
    {
        //Given that I am on the key presses page
        //When I enter the letter B into the input bar
        //Then I should get a message saying that I entered B

        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/key_presses");

            //Type B into bar
            driver.FindElement(By.Id("target")).SendKeys("B");

            //Find the message
            var message = driver.FindElement(By.Id("result"));

            Assert.That(message.Text, Is.EqualTo("You entered: B"));
        }
    }

    [Test]
    [Category("Redirection")]
    public void GivenIAmOnTheRedirectionPage_WhenIPressTheHereHyperlink_ThenIShouldGetRedirectedToAnotherPage()
    {
        //Given that I am on the redirection page
        //When I click on the "here" hyperlink
        //Then I should be redirected to the status code website

        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/redirector");

            //Click on the redirect button
            driver.FindElement(By.Id("redirect")).Click();

            //Assert that we are not on the sign-in page 
            Assert.That(driver.Url, Is.EqualTo("https://the-internet.herokuapp.com/status_codes"));
        }
    }
}