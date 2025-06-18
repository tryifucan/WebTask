using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebTask_Framework.Pages
{
    public class BasePage
    {
        public BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }

        protected void WaitUntilElementClickable(IWebElement element, int timeoutInSeconds = 10)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(driver => element.Displayed && element.Enabled);
        }

        protected void WaitUntilElementVisible(IWebElement element, int timeoutInSeconds = 10)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(driver => element.Displayed);
        }

        protected void WaitAndClick(IWebElement element, int timeoutInSeconds = 10)
        {
            WaitUntilElementClickable(element, timeoutInSeconds);
            element.Click();
        }

        public bool IsElementDisplayed(By bySelector)
        {
            try
            {
                var elements = Driver.FindElement(bySelector);
                return elements.Displayed;
            }
            catch
            {
                return false;
            }
        }

    }
}
