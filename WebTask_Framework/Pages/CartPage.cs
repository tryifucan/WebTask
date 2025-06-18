using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebTask_Framework.Pages
{
    public class CartPage : BasePage
    {
        private readonly IWebDriver _driver;
        public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            _driver = driver;
        }
        IList<IWebElement> cartItemLabels => _driver.FindElements(By.CssSelector(".cart_item .cart_item_label"));
        IWebElement cartCheckoutBtn => _driver.FindElement(By.Id("checkout"));
        IWebElement cartContinueShoppingBtn => _driver.FindElement(By.Id("continue-shopping"));

        public IList<string> GetCartItems()
        {
            var items = new List<string>();
            foreach (var item in cartItemLabels)
            {
                items.Add(item.Text);
            }
            return items;
        }

        public bool VerifyCartItem(string itemName)
        {
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                if (item.Contains(itemName))
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool IsCartEmpty()
        {
            return IsElementDisplayed((By.CssSelector(".cart_item")));
        }

        public void ProceedToCheckout()
        {
            WaitAndClick(cartCheckoutBtn);
        }

        public void ContinueShopping()
        {
            WaitAndClick(cartContinueShoppingBtn);
        }
    }
}
