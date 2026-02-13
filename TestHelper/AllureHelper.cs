using System;
using System.Text.Json;
using System.Xml.Linq;

namespace NunitPlayV2.TestHelper
{
    public static class AllureHelper
    {
        // Dynamically find the allure-results folder inside the bin folder
        private static string ResultsPath =>  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "allure-results");

        public static void GenerateMetadata(string buildNumber, string envName = "Staging")
        {
            if (!Directory.Exists(ResultsPath)) Directory.CreateDirectory(ResultsPath);

            CreateEnvironmentFile(buildNumber, envName);
            CreateCategoriesFile();
        }

        private static void CreateEnvironmentFile(string build, string env)
        {
            var doc = new XElement("environment",
                new XElement("parameter", new XElement("key", "Build"), new XElement("value", build)),
                new XElement("parameter", new XElement("key", "Environment"), new XElement("value", env)),
                new XElement("parameter", new XElement("key", "Runtime"), new XElement("value", ".NET 8"))
            );
            doc.Save(Path.Combine(ResultsPath, "environment.xml"));
        }

        private static void CreateCategoriesFile()
        {
            var categories = new[] {
            new { name = "Product Defects", matchedStatuses = new[] { "failed" }, messageRegex = ".*Assertion.*" },
            new { name = "System/Infra Issues", matchedStatuses = new[] { "broken" }, messageRegex = ".*Timeout.*|.*Selector.*" }
        };

            File.WriteAllText(Path.Combine(ResultsPath, "categories.json"), JsonSerializer.Serialize(categories)
            );
        }
    }
}
