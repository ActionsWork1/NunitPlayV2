using NunitPlayV2.TestHelper;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace NunitPlayV2.DataDrivenTests
{
    public class JsonHandlerTest:BaseTest
    {
        [Test]
        public void Test2()
        {
            var fake = new Bogus.Faker();
            var manager = new TestDataManager1();

            // This will APPEND TC003 because it doesn't exist
            manager.UpsertById(new UserData1
            {
                Id = "TC"+fake.Random.Number(100,200).ToString(),
                Name = "Test3",
                Email = "test3@example.com",
                Password = "789"
            });

            //// This will UPDATE TC001 because the ID matches
            //manager.UpsertById(new UserData1
            //{
            //    Id = "TC001",
            //    Name = "Updated Name",
            //    Email = "newemail@example.com",
            //    Password = "123"
            //});
        }
    }



public class UserData1
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Email")]
        public string? Email { get; set; }

        [JsonPropertyName("Password")]
        public string? Password { get; set; }
    }

    public class TestDataRoot1
    {
        [JsonPropertyName("TestData1")]
        public List<UserData1> TestData1 { get; set; } = new List<UserData1>();
    }


public class TestDataManager1
    {
        string _basePath = TestUtil.getBaseDirectory;
        private readonly string _filePath =   "testdata.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public void UpsertById(UserData1 inputData)
        {
            TestDataRoot1 root;

            // 1. Load existing data
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                root = JsonSerializer.Deserialize<TestDataRoot1>(json) ?? new TestDataRoot1();
            }
            else
            {
                root = new TestDataRoot1();
            }

            // 2. Find entry by ID (Primary Key)
            var existingEntry = root.TestData1.FirstOrDefault(x => x.Id == inputData.Id);

            if (existingEntry != null)
            {
                // UPDATE: Modify the existing fields
                existingEntry.Name = inputData.Name;
                existingEntry.Email = inputData.Email;
                existingEntry.Password = inputData.Password;
                Console.WriteLine($"Record {inputData.Id} updated successfully.");
            }
            else
            {
                // APPEND: Add as a new record
                root.TestData1.Add(inputData);
                Console.WriteLine($"Record {inputData.Id} appended successfully.");
            }

            // 3. Save back to the file
            string updatedJson = JsonSerializer.Serialize(root, _options);
            File.WriteAllText(_filePath, updatedJson);
        }
    }
}
