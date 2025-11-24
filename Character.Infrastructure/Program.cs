using Character.Infrastructure.Models;

namespace Character.Infrastructure
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new CharacterContext();
            
            context.Database.EnsureCreated(); 

            var mageRepository = new CharacterRepository<Mage>(context);
            var mageService = new CrudServiceAsync<Mage>(mageRepository);


            var newMage = new Mage
            {
                Id = Guid.NewGuid(),
                Name = "Harry",
                Level = 16,
                Luck = 100
            };

            Console.WriteLine($"Creating mage: {newMage.Name}...");
            await mageService.CreateAsync(newMage);

            Console.WriteLine("\nList of Mage in DB:");
            var mage = await mageService.ReadAllAsync();
            foreach (var w in mage)
            {
                Console.WriteLine($"- {w.Name} (Damage: {w.Level})");
            }

            newMage.Name = "Harry Potter";
            await mageService.UpdateAsync(newMage);
            Console.WriteLine($"\nUpdated name to: {newMage.Name}");

            var foundMage = await mageService.ReadAsync(newMage.Id);
            if (foundMage != null)
            {
                Console.WriteLine($"Found by ID: {foundMage.Name}");
            }

                // await mageService.RemoveAsync(newMage);
                // Console.WriteLine("\nMage deleted.");

            Console.ReadKey();
        }
    }
}
