using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
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

        public Quests(string name, string desc)
        {
            QuestName = name;
            QuestDescription = desc;
            Id = Guid.NewGuid();
            counterQuest++;
        }

        public static int GetTotalQuests()
        {
            return counterQuest;
        }
    }
}
