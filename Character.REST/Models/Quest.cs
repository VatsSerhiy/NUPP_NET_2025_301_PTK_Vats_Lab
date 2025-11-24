namespace Character.REST.Models
{
    public class Quest
    {

        public Guid Id { get; set; }
        public string? QuestName { get; set; }
        public string? QuestDescription { get; set; }
        public int RewardExp { get; set; }

    }
}
