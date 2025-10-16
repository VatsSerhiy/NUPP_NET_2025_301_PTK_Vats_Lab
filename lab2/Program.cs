namespace lab2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            

            var service = new CrudServiceAsync<Bus>("buses.txt");

            Console.WriteLine("Create 1000 obj Bus parallel...");
            var startTime = DateTime.Now;

            
            Parallel.For(0, 1000, async i =>
            {
                var bus = Bus.CreateNew();
                await service.CreateAsync(bus);
            });

            await Task.Delay(1000);
            var endTime = DateTime.Now;
            Console.WriteLine($"Time create: {(endTime - startTime).TotalSeconds:F2}");

           
            var allBuses = (await service.ReadAllAsync()).ToList();
            Console.WriteLine($"Created obj: {allBuses.Count}\n");

            Console.WriteLine($"Capacity: Min={allBuses.Min(b => b.Capacity)}, Max={allBuses.Max(b => b.Capacity)}, Avg={allBuses.Average(b => b.Capacity):F2}");
            Console.WriteLine($"Year: Min={allBuses.Min(b => b.Year)}, Max={allBuses.Max(b => b.Year)}, Avg={allBuses.Average(b => b.Year):F2}");
            Console.WriteLine($"Price: Min={allBuses.Min(b => b.Price):F2}, Max={allBuses.Max(b => b.Price):F2}, Avg={allBuses.Average(b => b.Price):F2}");

            
            Console.WriteLine("\nSave dict in file");
            var saved = await service.SaveAsync();
            Console.WriteLine(saved ? "File save in file" : "Error saving");

        }
    }
}
