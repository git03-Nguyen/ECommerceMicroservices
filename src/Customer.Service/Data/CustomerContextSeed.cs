using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Data;

public static class CustomerContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedData = new Models.Customer[]
        {
            new()
            {
                Id = 1, Name = "John Doe", Email = "john-doe@gmail.com", Phone = "1234567890",
                Address = "123 Main St"
            },
            new()
            {
                Id = 2, Name = "Jane Doe", Email = "jane-doe@gmail.com", Phone = "0987654321",
                Address = "456 Elm St"
            },
            new Models.Customer
            {
                Id = 3, Name = "Bob Smith", Email = "bob.smith@gmail.com", Phone = "1112223333",
                Address = "789 Oak St"
            },
            new()
            {
                Id = 4, Name = "Alice Johnson", Email = "alice.johnson@gmail.com", Phone = "4445556666",
                Address = "321 Maple St"
            },
            new()
            {
                Id = 5, Name = "Charlie Brown", Email = "charlie.brown@gmail.com", Phone = "7778889999",
                Address = "654 Pine St"
            },
            new()
            {
                Id = 6, Name = "Emily Davis", Email = "emily.davis@gmail.com", Phone = "2223334444",
                Address = "987 Cedar St"
            },
            new()
            {
                Id = 7, Name = "Frank Wilson", Email = "frank.wilson@gmail.com", Phone = "5556667777",
                Address = "123 Birch St"
            },
            new()
            {
                Id = 8, Name = "Grace Lee", Email = "grace.lee@gmail.com", Phone = "8889990000",
                Address = "456 Spruce St"
            },
            new()
            {
                Id = 9, Name = "Henry Clark", Email = "henry.clark@gmail.com", Phone = "1112224444",
                Address = "789 Redwood St"
            },
            new()
            {
                Id = 10, Name = "Isabella Martinez", Email = "isabella.martinez@gmail.com", Phone = "3334445555",
                Address = "321 Fir St"
            }
        };
        
        modelBuilder.Entity<Models.Customer>().HasData(seedData);
    }
}