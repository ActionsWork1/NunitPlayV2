using NunitPlayV2.TestHelper;
using System.Text.Json;


namespace NunitPlayV2.DataDrivenTests
{
    public class JsonUpdaterTest:BaseTest
    {
        [Test]
        public void Test11() {
            var fake = new Bogus.Faker();
            string Name = fake.Name.FirstName();
            string Email = fake.Internet.Email();
            string Pwd = fake.Internet.Password();


            // JsonHandler jsonHandler = new JsonHandler();


            string _basePath = TestUtil.getBaseDirectory;
            var manager = new TestDataManager(_basePath+"/TestData/testdata.json");

            // 1. Add a new user (Appends to the list)
            manager.UpsertTestData(new UserData
            {
                Name = fake.Name.FirstName(),
                Email = fake.Internet.Email(),
                Password = fake.Internet.Password()
            });

            // 2. Update an existing user (Updates Test1's email)
            manager.UpsertTestData(new UserData
            {
                Name = "Test1",
                Email = "updated_test1@example.com",
                Password = "123"
            });





        }
    }

    public class TestDataRoot
    {
        public List<UserData> TestData { get; set; } = new List<UserData>();
    }

    public class UserData
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    //public class TestDataRoot
    //{
    //    public List<UserData>? TestData { get; set; }
    //}


    public class TestDataManager
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public TestDataManager(string filePath)
        {
            _filePath = filePath;
            // Makes the JSON file pretty-printed and easy to read
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        /// <summary>
        /// Overwrites the file with an entirely new set of data.
        /// </summary>
        public void WriteNewFile(List<UserData> users)
        {
            var root = new TestDataRoot { TestData = users };
            string json = JsonSerializer.Serialize(root, _options);
            File.WriteAllText(_filePath, json);
        }

        /// <summary>
        /// Updates an existing entry by Name, or Appends it if it doesn't exist.
        /// </summary>
        public void UpsertTestData(UserData user)
        {
            TestDataRoot root;

            // 1. Load existing data or initialize new if file doesn't exist
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                root = JsonSerializer.Deserialize<TestDataRoot>(json) ?? new TestDataRoot();
            }
            else
            {
                root = new TestDataRoot();
            }

            // 2. Logic to Update or Append
            var existingUser = root.TestData.FirstOrDefault(u => u.Name == user.Name);

            if (existingUser != null)
            {
                // Update the existing record
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
            }
            else
            {
                // Append the new record
                root.TestData.Add(user);
            }

            // 3. Save the updated list back to the file
            string updatedJson = JsonSerializer.Serialize(root, _options);
            File.WriteAllText(_filePath, updatedJson);

            // Inside UpsertTestData after deserializing:
            Console.WriteLine($"Found {root.TestData.Count} existing users.");
        }
    }



    //public class JsonHandler
    //{
    //    private string filePath = "testdata.json";

    //    public void AddOrUpdateUser(UserData newUser)
    //    {
    //        TestDataRoot root;

    //        // 1. Read existing data
    //        if (File.Exists(filePath))
    //        {
    //            string jsonString = File.ReadAllText(filePath);
    //            root = JsonSerializer.Deserialize<TestDataRoot>(jsonString) ?? new TestDataRoot { TestData = new List<UserData>() };
    //        }
    //        else
    //        {
    //            root = new TestDataRoot { TestData = new List<UserData>() };
    //        }

    //        // 2. Update or Append
    //        var existingUser = root.TestData!.FirstOrDefault(u => u.Name == newUser.Name);
    //        if (existingUser != null)
    //        {
    //            // Update existing user
    //            existingUser.Email = newUser.Email;
    //            existingUser.Password = newUser.Password;
    //        }
    //        else
    //        {
    //            // Append new user
    //            root.TestData!.Add(newUser);
    //        }

    //        // 3. Save back to file
    //        var options = new JsonSerializerOptions { WriteIndented = true };
    //        string updatedJson = JsonSerializer.Serialize(root, options);
    //        File.WriteAllText(filePath, updatedJson);
    //    }
    //}





}
