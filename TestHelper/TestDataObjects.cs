using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitPlayV2.TestHelper
{
   
    public class Root
    {
        public List<UserTestData> TestData1 { get; set; } = new();
    }

    public class UserTestData
    {
        public string? Id { get; set; }
        public string? Domain { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Date { get; set; }
    }
    public static class DataGenerator
    {
        public static List<UserTestData1> GetUserTests(int count)
        {
            return new Bogus.Faker<UserTestData1>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password(10))
                .Generate(count);
        }
    }
    public class UserTestData1
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

}
