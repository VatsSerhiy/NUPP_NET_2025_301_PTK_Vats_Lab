using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Bus : IObject
    {
        public Guid Id { get; set; }
        public string? Model { get; set; }
        public int Capacity { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public static Bus CreateNew()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string[] models = { "Mercedes-Benz", "Volvo", "Scania", "MAN", "Setra" };

            return new Bus
            {
                Id = Guid.NewGuid(),
                Model = models[random.Next(models.Length)],
                Capacity = random.Next(20, 80),
                Year = random.Next(2010, 2025),
                Price = Math.Round(random.NextDouble() * 500000 + 100000, 2)
            };
        }

        public override string ToString()
        {
            return $"{Id}|{Model}|{Capacity}|{Year}|{Price}";
        }


    }
}
