using IncirAgaci.API.Models;

namespace IncirAgaci.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Seçkin Bostancı",
                Email = "seckinbostanci@gmail.com",
                Address = new Address()
                {
                    Street = "123 Sokak",
                    City = "Mars Kolonisi 1",
                    ZipCode = "0001"
                }
            },
            new User
            {
                Id = 2,
                Name = "User2",
                Email = "user2@gmail.com",
                Address = new Address()
                {
                    Street = "456 Sokak",
                    City = "Mars Kolonisi 1",
                    ZipCode = "0001"
                }
            },
            new User
            {
                Id = 3,
                Name = "User3",
                Email = "User3@gmail.com",
                Address = new Address()
                {
                    Street = "123 Sokak",
                    City = "Mars Kolonisi 2",
                    ZipCode = "0002"
                }
            }
        };
}
