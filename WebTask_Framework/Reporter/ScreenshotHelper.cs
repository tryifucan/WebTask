using OpenQA.Selenium;
using System;
using System.IO;

namespace WebTask_Framework.Reporter
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string testName)
        {
            var screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var filePath = Path.Combine(screenshotsDir, fileName);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);

            return filePath;
        }
    }
}
