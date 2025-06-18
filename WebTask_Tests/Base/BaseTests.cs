using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebTask_Framework.Config;
using WebTask_Framework.Driver;
using WebTask_Framework.Pages;
using WebTask_Framework.Reporter;

namespace WebTask_Tests.Base
{
    public class BaseTests
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        protected DriverFactory DriverFactory { get; private set; }
        protected string BaseUrl => ConfigurationReader.BaseUrl;

        protected LoginPage LoginPage => new LoginPage(Driver, Wait);
        protected ProductsPage ProductsPage => new ProductsPage(Driver, Wait);
        protected CartPage CartPage => new CartPage(Driver, Wait);
        protected CheckoutPage CheckoutPage => new CheckoutPage(Driver, Wait);

        protected ExtentReports Extent => ExtentManager.Instance;
        protected ExtentTest Test;

        [SetUp]
        public void Setup()
        {
            DriverFactory = new DriverFactory();
            Driver = DriverFactory.GetDriver();

            Driver.Navigate().GoToUrl(BaseUrl);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(ConfigurationReader.ImplicitWait));

            var testName = TestContext.CurrentContext.Test.Name;
            var category = TestContext.CurrentContext.Test.Properties.Get("Category")?.ToString();

            Test = Extent.CreateTest(testName);
            if (!string.IsNullOrEmpty(category))
                Test.AssignCategory(category);
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;

            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshotPath = ScreenshotHelper.CaptureScreenshot(Driver, TestContext.CurrentContext.Test.Name);
                if (screenshotPath != null)
                    Test.AddScreenCaptureFromPath(screenshotPath);

                Test.Fail("Test Failed");
                Test.Fail(message);
                Test.Fail(stacktrace);
            }
            else if (outcome == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                Test.Pass("Test Passed");
            }
            else
            {
                Test.Skip("Test Skipped");
            }

            Extent.Flush();
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
