namespace Character.REST.Models
{
    public class CharacterClassRequest
    {
        public string? Name { get; set; }
        public float MoveSpeed { get; set; }
        public bool CanUseSpell { get; set; } = false;
        public int Level { get; set; }
        public int HitPoint { get; set; }
        public float Luck { get; set; }
    }
}
