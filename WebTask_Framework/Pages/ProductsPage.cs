using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTask_Framework.Utils;

namespace WebTask_Framework.Pages
{
    public class ProductsPage : BasePage
    {
        private readonly IWebDriver _driver;
        public ProductsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            _driver = driver;
        }
        IList<IWebElement> products => _driver.FindElements(By.CssSelector(".inventory_item .pricebar .btn"));
        IList<IWebElement> productsNames => _driver.FindElements(By.CssSelector(".inventory_item .inventory_item_name"));
        IWebElement burgerMenuBtn => Driver.FindElement(By.Id("react-burger-menu-btn"));
        IWebElement logoutBtn => Driver.FindElement(By.Id("logout_sidebar_link"));
        IWebElement sortByPrice => _driver.FindElement(By.ClassName("product_sort_container"));
        IList<IWebElement> productsPrices => _driver.FindElements(By.ClassName("inventory_item_price"));
        IWebElement shopingCartButton => _driver.FindElement(By.Id("shopping_cart_container"));

        public void AddProductToCart(int product)
        {
            WaitUntilElementClickable(products[product]);
            WaitAndClick(products[product]);
        }

        public void RemoveProductFromCart(int product)
        {
            WaitAndClick(products[product]);
        }

        public string GetProductName(int product)
        {
            var item = productsNames[product];
            WaitUntilElementVisible(item, 10);
            return item.Text;
        }

        public void GoToCart()
        {
            WaitAndClick(shopingCartButton);
        }
        public void Logout()
        {
            WaitAndClick(burgerMenuBtn);
            WaitAndClick(logoutBtn);
        }
        public void SortProductsByPriceDescending()
        {
            var sortDropdown = sortByPrice;
            var selectElement = new SelectElement(sortDropdown);
            selectElement.SelectByValue(ProductSorting.HighToLow);
        }
        public bool AreProductsSortedByPriceDescending()
        {
            var priceElements = productsPrices;
            var prices = new List<decimal>();

            foreach (var element in priceElements)
            {
                string rawText = element.Text.Trim();
                string numericPart = rawText.Replace("$", "").Trim();

            }

            var sortedPrices = prices.OrderByDescending(p => p).ToList();
            return prices.SequenceEqual(sortedPrices);
        }

    }
}
