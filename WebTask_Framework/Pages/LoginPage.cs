using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebTask_Framework.Pages
{
    public class LoginPage : BasePage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            _driver = driver;
        }

        IWebElement UsernameField => _driver.FindElement(By.Id("user-name"));
        IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));

        public void Login(string username, string password)
        {
            WaitUntilElementClickable(UsernameField);
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            WaitUntilElementClickable(PasswordField);
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            WaitAndClick(LoginButton);
        }
    }
}
