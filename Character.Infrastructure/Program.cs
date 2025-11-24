using Character.Infrastructure.Models;

namespace Character.Infrastructure
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new CharacterContext();
            
            context.Database.EnsureCreated(); 

            var warriorRepository = new CharacterRepository<Warrior>(context);
            var warriorService = new CrudServiceAsync<Warrior>(warriorRepository);


            var newWarrior = new Warrior
            {
                Id = Guid.NewGuid(),
                Name = "Conan",
                Level = 12,
                Luck = 50
            };

            Console.WriteLine($"Creating warrior: {newWarrior.Name}...");
            await warriorService.CreateAsync(newWarrior);

            Console.WriteLine("\nList of Warriors in DB:");
            var warriors = await warriorService.ReadAllAsync();
            foreach (var w in warriors)
            {
                Console.WriteLine($"- {w.Name} (Damage: {w.Level})");
            }

            newWarrior.Name = "Conan the King";
            await warriorService.UpdateAsync(newWarrior);
            Console.WriteLine($"\nUpdated name to: {newWarrior.Name}");

            var foundWarrior = await warriorService.ReadAsync(newWarrior.Id);
            if (foundWarrior != null)
            {
                Console.WriteLine($"Found by ID: {foundWarrior.Name}");
            }

                // 7. Тестируем удаление (Delete)
                // await warriorService.RemoveAsync(newWarrior);
                // Console.WriteLine("\nWarrior deleted.");

            Console.ReadKey();
        }
    }
}
