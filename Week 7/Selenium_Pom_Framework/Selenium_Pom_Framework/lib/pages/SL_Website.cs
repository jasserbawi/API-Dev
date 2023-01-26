using OpenQA.Selenium;

//Responsible for the driver and its behaviours

namespace SL_Pom_Framework_Test.lib.pages;

public class SL_Website<T> where T : IWebDriver, new()
{
    public IWebDriver SeleniumDriver { get; set; }
    public HomePage SL_HomePage { get; set; }

    public SL_Website(int pageLoadInSecs = 1, int implicitWaitInSecs = 2)
    {
        SL_HomePage = new HomePage(SeleniumDriver);
    }
}
