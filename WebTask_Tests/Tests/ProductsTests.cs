using WebTask_Framework.Config;
using WebTask_Framework.Pages;
using WebTask_Tests.Base;

namespace WebTask_Tests.Tests
{
    [TestFixture]
    public class Tests : BaseTests
    {
        [Test]
        [Category("Smoke")]
        [Description("Verify that the cart functionality works as expected.")]
        public void VerifyCartFunctionality()
        {
            Test.Info("Starting VerifyCartFunctionality test.");
            Test.Info("Navigate to the login page and log in.");

            LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);

            string firstProduct = ProductsPage.GetProductName(0);
            string secondProduct = ProductsPage.GetProductName(5);
            string thirdProduct = ProductsPage.GetProductName(4);

            Test.Info("Adding first and last product to the cart.");

            ProductsPage.AddProductToCart(0);
            ProductsPage.AddProductToCart(5);

            Test.Info("Verifying that the cart contains the added products.");

            ProductsPage.GoToCart();
            Assert.That(CartPage.VerifyCartItem(firstProduct), Is.True, $"Cart does not contain the product: {firstProduct}");
            Assert.That(CartPage.VerifyCartItem(secondProduct), Is.True, $"Cart does not contain the product: {secondProduct}");

            Test.Info("Removing the first product from the cart and adding another product.");
            CartPage.ContinueShopping();
            ProductsPage.RemoveProductFromCart(0);
            ProductsPage.AddProductToCart(4);

            Test.Info("Verifying that the cart contains the remaining product and the newly added product.");

            ProductsPage.GoToCart();
            Assert.That(CartPage.VerifyCartItem(thirdProduct), Is.True, $"Cart does not contain the product: {thirdProduct}");
            Assert.That(CartPage.VerifyCartItem(secondProduct), Is.True, $"Cart does not contain the product: {secondProduct}");

            Test.Info("Proceeding to checkout and completing the purchase.");

            CartPage.ProceedToCheckout();
            CheckoutPage.EnterInformation();
            CheckoutPage.FinishCheckout();

            Test.Info("Verifying that the cart is empty after checkout.");

            ProductsPage.GoToCart();
            Assert.That(CartPage.IsCartEmpty(), Is.False, "Cart is not empty after checkout");

            Test.Info("Logging out after completing the test.");

            ProductsPage.Logout();
        }

        [Test]
        [Category("Smoke")]
        [Description("Verify that the Products sorting functionality works as expected.")]
        public void VerifyProductsSortingFunctionality()
        {
            Test.Info("Starting VerifyProductsSortingFunctionality test.");
            Test.Info("Navigate to the login page and log in.");

            LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);

            Test.Info("Sorting products by price in descending order and verify sorting.");

            ProductsPage.SortProductsByPriceDescending();
            Assert.That(ProductsPage.AreProductsSortedByPriceDescending(), Is.True, "Products are not sorted correctly");

            Test.Info("Logging out after completing the test.");

            ProductsPage.Logout();
        }
    }
}