using System;
namespace Characters.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrudService<Quests> questService = new CrudService<Quests>();

            System.Console.WriteLine("Create");

            Quests q1 = new Quests("Gather herbs", "You need to find 10 medicinal herbs.");
            Quests q2 = new Quests("Defeat the wolf", "The gray wolf is bothering the villagers.");
            Quests q3 = new Quests("Find an artifact", "An ancient vase has been lost in a cave.");

            questService.Create(q1);
            questService.Create(q2);
            questService.Create(q3);

            PrintAll(questService);

            System.Console.WriteLine($"\nRead");
            try
            {
                var foundQuest = questService.Read(q2.Id);
                System.Console.WriteLine($"Quest found: {foundQuest.QuestName}");
            }
            catch (Exception)
            {
                System.Console.WriteLine("Quest not found");
            }

            System.Console.WriteLine("\nUpdate");

            Quests q1_Updated = new Quests("Collect RARE herbs", "Now you need to find 20 herbs!");

            q1_Updated.Id = q1.Id;

            questService.Update(q1_Updated);

            PrintAll(questService);

            System.Console.WriteLine("\nRemove");

            questService.Remove(q3); 

            PrintAll(questService);


            

        }
        static void PrintAll(CrudService<Quests> service)
        {
            System.Console.WriteLine("Current quest list:");
            var allQuests = service.ReadAll();

            foreach (var quest in allQuests)
            {
                // Выводим свойства напрямую
                System.Console.WriteLine($" - [ID: {quest.Id.ToString().Substring(0, 4)}...] {quest.QuestName}: {quest.QuestDescription}");
            }
        }

    }
}
