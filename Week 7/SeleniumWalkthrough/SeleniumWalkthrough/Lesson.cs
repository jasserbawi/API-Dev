using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace SeleniumWalkthrough;

public class Tests
{
    [Test]
    [Category("Happy")]
    public void GivenIAmOnHomepage_WhenIEnterAValidEmailAndValidPassword_ThenIShouldLandOnTheInventoryPage()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless");
        //Use the web driver
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            //Navigate to saucedemo.com in the driver
            driver.Navigate().GoToUrl("http://saucedemo.com");

            //Grab the username field
            var userNameField = driver.FindElement(By.Id("user-name"));

            //Enter username into it
            userNameField.SendKeys("standard_user");

            //Grab the password field
            var passwordField = driver.FindElement(By.Id("password"));

            //Enter password into it
            passwordField.SendKeys("secret_sauce");

            //Click the sign-in button
            driver.FindElement(By.Id("login-button")).Click();

            //Assert that we are not on the sign-in page 
            Assert.That(driver.Url, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
        }
    }

    [Test]
    [Category("Sad")]
    public void GivenIAmOnHomepage_WhenIEnterAValidEmailButInvalidPassword_ThenIStayGetAnErrorMessage()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless");
        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Manage().Window.Maximize();

            //Navigate to saucedemo.com in the driver
            driver.Navigate().GoToUrl("http://saucedemo.com");

            //Grab the username field
            var userNameField = driver.FindElement(By.Id("user-name"));

            //Enter username into it
            userNameField.SendKeys("standard_user");

            //Grab the password field
            var passwordField = driver.FindElement(By.Id("password"));

            //Enter password into it
            passwordField.SendKeys("wrongpassword");

            //Click the sign-in button
            driver.FindElement(By.Id("login-button")).Click();

            //Grab the error message container
            var errorMessage = driver.FindElement(By.ClassName("error-message-container"));

            //Assert that we are not on the sign-in page 
            Assert.That(errorMessage.FindElement(By.TagName("h3")).Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
        }
    }
}