using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure.Models
{
    public class Quests : IEntity
    {
        static int counterQuest = 0;

        public string? QuestName { get; set; }
        public string? QuestDescription { get; set; }
        public Guid Id { get; set; }

        static Quests()
        {
            counterQuest = 0;
            Console.WriteLine("Quest system starting");
        }

        public static int GetTotalQuests()
        {
            return counterQuest;
        }
    }
}
