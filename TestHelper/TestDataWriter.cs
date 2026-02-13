using Bogus;
using System.Text.Json;
using System.Threading.Tasks;

namespace NunitPlayV2.TestHelper
{
    public static class TestDataWriter
    {

        static string filePath = TestUtil.getBaseDirectory + "/FreeFlowTests/TestData/testdata.json";

        public static void WriteTestData(string? id = null,
                                         string? domain = null,
                                         string? name = null,
                                         string? email = null,
                                         string? pwd = null)
        {

            // Read existing JSON
            Root root;
            Faker fake = new Bogus.Faker();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                root = JsonSerializer.Deserialize<Root>(json) ?? new Root();
            }
            else
            {
                root = new Root();
            }

            // Add new record
            var newData = new UserTestData
            {
                Id = id! ?? fake.Random.Int(1000, 5000).ToString(),
                Domain = domain!,
                Name = name! ?? fake.Name.FirstName(),
                Email = email! ?? fake.Name.FirstName() + "01@yopmail.com",
                Password = pwd! ?? fake.Internet.Password(),
                Date = DateTime.Now.ToString("MM-dd-yyyy_hh:mm:sstt")
            };

            root.TestData1.Add(newData);

            // Write back to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(root, options));
        }


        public static void ReadTestData()
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Test data file not found.");

            string json = File.ReadAllText(filePath);

            Root root = JsonSerializer.Deserialize<Root>(json)!;

            foreach (var item in root.TestData1)
            {
                Console.WriteLine($"Id: {item.Id}");
                Console.WriteLine($"Domain: {item.Domain}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Email: {item.Email}");
                Console.WriteLine($"Password: {item.Password}");
                Console.WriteLine($"Date: {item.Date}");
                Console.WriteLine("----------------------");
            }
        }


        public static UserTestData SearchTestData(string data_id)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Test data file not found.");

            string json = File.ReadAllText(filePath);
            Root root = JsonSerializer.Deserialize<Root>(json)!;

            Console.WriteLine($"Total TestData: {root.TestData1.Count}");

            UserTestData? testCase = root.TestData1.FirstOrDefault(x => x.Id == data_id);
            Console.WriteLine($"Test Data Record: {testCase!.Email}");

            //foreach (var item in root.TestData1)
            //{

            //    Console.WriteLine($"Id: {item.Id}");
            //    Console.WriteLine($"Domain: {item.Domain}");
            //    Console.WriteLine($"Name: {item.Name}");
            //    Console.WriteLine($"Email: {item.Email}");
            //    Console.WriteLine($"Password: {item.Password}");
            //    Console.WriteLine($"Date: {item.Date}");
            //    Console.WriteLine("----------------------");
            //}
            return testCase;
        }

    }






    //    public class Root
    //    {
    //        public List<TestData> TestData1 { get; set; } = new();
    //    }

    //    public class TestData
    //    {
    //        public string Id { get; set; }
    //        public string Domain { get; set; }
    //        public string Name { get; set; }
    //        public string Email { get; set; }
    //        public string Password { get; set; }
    //        public string Date { get; set; }
    //    }

    //}







}
