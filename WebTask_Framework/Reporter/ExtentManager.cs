using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using WebTask_Framework.Config;

namespace WebTask_Framework.Reporter
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazyInstance = new Lazy<ExtentReports>(() =>
        {
            var reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");
            Directory.CreateDirectory(reportDir);
            var spark = new ExtentSparkReporter(Path.Combine(reportDir, "ExtentReport.html"));

            var extent = new ExtentReports();
            extent.AttachReporter(spark);
            extent.AddSystemInfo("Environment", ConfigurationReader.Environment);
            extent.AddSystemInfo("User", ConfigurationReader.Username);
            return extent;
        });

        public static ExtentReports Instance => _lazyInstance.Value;
    }
}
