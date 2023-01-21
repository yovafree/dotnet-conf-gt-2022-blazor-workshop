namespace BlazorApp.Data;
using Faker;
public class FakeDataService
{
    public Task<User[]> GetFakeDataAsync()
    {
        return Task.FromResult(Enumerable.Range(1, 50).Select(index => new User
        {
            Name = Faker.Name.FullName(NameFormats.WithPrefix),
            Followers = Faker.RandomNumber.Next(0, 10000),
            Area = $"{Faker.Address.Country()}, {Faker.Address.City()}",
            Bio = String.Join(" ", Faker.Lorem.Sentences(3))
        }).ToArray());
    }
}
