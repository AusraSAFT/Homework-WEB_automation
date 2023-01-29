using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Homework_WEB_automation
{
    public class Demoblaze_test
    {
        public IWebDriver driver;
        private string email = "Ausrine350@gmail.com";
        private string password = "baravykas";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void SignIn()
        {
            driver.FindElement(By.Id("signin2")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@class='btn btn-primary'])[2]")));

            driver.FindElement(By.Id("sign-username")).SendKeys(email);
            driver.FindElement(By.Id("sign-password")).SendKeys(password);
            driver.FindElement(By.XPath("(//button[@class='btn btn-primary'])[2]")).Click();

            IAlert SignUpAlert = wait.Until(ExpectedConditions.AlertIsPresent());
            Assert.That(SignUpAlert.Text.Contains("Sign up successful"));
            SignUpAlert.Accept();
        }

        [Test, Order(2)]
        public void Login()
        {
            driver.FindElement(By.Id("login2")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@class='btn btn-primary'])[3]")));

            driver.FindElement(By.Id("loginusername")).SendKeys(email);
            driver.FindElement(By.Id("loginpassword")).SendKeys(password);
            driver.FindElement(By.XPath("(//button[@class='btn btn-primary'])[3]")).Click();

            var welcomeUsername = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nameofuser")));
            Assert.That(welcomeUsername.Text.Contains("Welcome"));
        }

        [Test, Order(3)]
        public void Select_Item()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.LinkText("Laptops")).Click();
            driver.FindElement(By.LinkText("MacBook air")).Click();
            driver.FindElement(By.LinkText("Add to cart")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alertProductAdded = wait.Until(ExpectedConditions.AlertIsPresent());
            Assert.That(alertProductAdded.Text.Contains("Product added"));
            alertProductAdded.Accept();
        }

        [Test, Order(4)]
        public void Purchase()
        {
            driver.FindElement(By.Id("cartur")).Click();
            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@class='btn btn-primary'])[3]")));

            driver.FindElement(By.Id("name")).SendKeys("Ausrine");
            driver.FindElement(By.Id("country")).SendKeys("Lietuva");
            driver.FindElement(By.Id("city")).SendKeys("Kaunas");
            driver.FindElement(By.Id("card")).SendKeys("159159159");
            driver.FindElement(By.Id("month")).SendKeys("12");
            driver.FindElement(By.Id("year")).SendKeys("2023");

            driver.FindElement(By.XPath("(//button[@class='btn btn-primary'])[3]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2 [text()='Thank you for your purchase!']")));
            var alertSuccess = driver.FindElement(By.XPath("//h2 [text()='Thank you for your purchase!']"));
            Assert.That(alertSuccess.Text, Is.EqualTo("Thank you for your purchase!"));
        }
    }
}