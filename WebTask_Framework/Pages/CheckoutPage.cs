using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebTask_Framework.Utils;

namespace WebTask_Framework.Pages
{
    public class CheckoutPage : BasePage
    {
        private readonly IWebDriver _driver;
        public CheckoutPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            _driver = driver;
        }
        IWebElement cartCheckoutBtn => _driver.FindElement(By.Id("checkout"));
        IWebElement cartContinueShoppingBtn => _driver.FindElement(By.Id("continue-shopping"));
        IWebElement firstNameInput => _driver.FindElement(By.Id("first-name"));
        IWebElement lastNameInput => _driver.FindElement(By.Id("last-name"));
        IWebElement postalCodeInput => _driver.FindElement(By.Id("postal-code"));
        IWebElement continueBtn => _driver.FindElement(By.Id("continue"));
        IWebElement finishBtn => _driver.FindElement(By.Id("finish"));
        IWebElement checkoutCompleteContainer => _driver.FindElement(By.ClassName("checkout_complete_container"));
        IWebElement backHomeBtn => _driver.FindElement(By.Id("back-to-products"));

        public void ProceedToCheckout()
        {
            WaitAndClick(cartCheckoutBtn);
        }

        public void ContinueShopping()
        {
            WaitAndClick(cartContinueShoppingBtn);
        }

        public void EnterInformation()
        {
            WaitUntilElementVisible(firstNameInput, 10);
            firstNameInput.SendKeys(Generator.GenerateRandomString(5));
            lastNameInput.SendKeys(Generator.GenerateRandomString(5));
            postalCodeInput.SendKeys(Generator.GenerateRandomZipCode());
        }

        public void FinishCheckout()
        {
            WaitAndClick(continueBtn);
            WaitAndClick(finishBtn);
            IsCheckoutComplete();
            GoBackToHomePage();
        }

        public bool IsCheckoutComplete()
        {
            WaitUntilElementVisible(checkoutCompleteContainer, 10);
            return checkoutCompleteContainer.Displayed;
        }

        public void GoBackToHomePage()
        {
            WaitAndClick(backHomeBtn);
        }
    }
}
