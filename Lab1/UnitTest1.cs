using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1
{
    [TestFixture]
    public class Tests
    {
        private EdgeDriver driver;
        private WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new EdgeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2000));
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://pastebin.com/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("PostForm[text]")));
            driver.FindElement(By.Name("PostForm[text]")).SendKeys("Some text");
            driver.FindElement(By.Id("select2-postform-expiration-container")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("select2-results")));
            driver.FindElement(By.XPath("//li[contains(text(), '10 Minutes')]")).Click();
            driver.FindElement(By.Name("PostForm[name]")).SendKeys("Title");
            driver.FindElement(By.CssSelector(".btn.-big")).Click();
            Thread.Sleep(2000);
            var element = driver.FindElement(By.XPath("//div[@class='info-top']/h1"));
            string text1 = element.Text;
            string expectedText1 = "Title";
            element = driver.FindElement(By.ClassName("expire"));
            string text2 = element.Text;
            string expectedText2 = "10 MIN";
            element = driver.FindElement(By.ClassName("de1"));
            string text3 = element.Text;
            string expectedText3 = "Some text";
            Assert.That(text1, Is.EqualTo(expectedText1));
            Assert.That(text2, Is.EqualTo(expectedText2));
            Assert.That(text3, Is.EqualTo(expectedText3));
            Thread.Sleep(2000);
        }
        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("q")));
            driver.FindElement(By.CssSelector("div.column.information ul li:last-child a")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("send-email")));
            driver.FindElement(By.Name("FullName")).SendKeys("Roman");
            driver.FindElement(By.Name("Email")).SendKeys("test@mailinator.com");
            driver.FindElement(By.Name("Enquiry")).SendKeys("Hi, I have an issue with buttons on the main page of your site");
            driver.FindElement(By.Name("send-email")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("result")));
            var resultElement = driver.FindElement(By.ClassName("result"));
            string text = resultElement.Text;
            string expectedText = "Your enquiry has been successfully sent to the store owner.";
            Assert.That(text, Is.EqualTo(expectedText));
            Thread.Sleep(2000);
        }
        [TearDown]
        public void Exit()
        {
            driver.Quit();
        }
    }
}