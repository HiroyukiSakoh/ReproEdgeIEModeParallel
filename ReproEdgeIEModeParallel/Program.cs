using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Drawing;
using System.IO;

namespace ReproEdgeIEModeParallel
{
    internal class Program
    {
        static void Main()
        {
            //IE 
            {
                using var driverA = OpenIE(logfile: "IE-A.log");
                driverA.Navigate().GoToUrl("https://github.com/");

                Console.WriteLine(driverA.Title);//Github

                using var driverB = OpenIE(logfile: "IE-B.log");
                driverB.Navigate().GoToUrl("https://bing.com/");

                Console.WriteLine(driverA.Title);//Github
                Console.WriteLine(driverB.Title);//Bing

                driverA.Quit();
                driverB.Quit();
            }

            //Edge(IEMode)
            {
                using var driverA = OpenEdgeIEMode(logfile: "EdgeIE-A.log");
                driverA.Navigate().GoToUrl("https://Github.com/");

                Console.WriteLine(driverA.Title);//Github

                using var driverB = OpenEdgeIEMode(logfile: "EdgeIE-B.log");
                driverB.Navigate().GoToUrl("https://bing.com/");

                Console.WriteLine(driverA.Title);//Bing ****
                Console.WriteLine(driverB.Title);//Bing

                driverA.Quit();
                driverB.Quit();//slowly ***
            }
        }

        private static WebDriver OpenEdgeIEMode(string logfile)
        {
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();
            ieOptions.AttachToEdgeChrome = true;
            ieOptions.EdgeExecutablePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft\\Edge\\Application\\msedge.exe");
            var ieService = InternetExplorerDriverService.CreateDefaultService();
            var ieLogFile = Path.Combine(Directory.GetCurrentDirectory(), logfile);
            ieService.LogFile = ieLogFile;
            ieService.LoggingLevel = InternetExplorerDriverLogLevel.Trace;
            var driver = new InternetExplorerDriver(ieService, ieOptions, TimeSpan.FromSeconds(10));
            return driver;
        }
        private static WebDriver OpenIE(string logfile)
        {
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();
            var ieService = InternetExplorerDriverService.CreateDefaultService();
            var ieLogFile = Path.Combine(Directory.GetCurrentDirectory(), logfile);
            ieService.LogFile = ieLogFile;
            ieService.LoggingLevel = InternetExplorerDriverLogLevel.Trace;
            var driver = new InternetExplorerDriver(ieService, ieOptions, TimeSpan.FromSeconds(10));
            return driver;
        }
    }
}
