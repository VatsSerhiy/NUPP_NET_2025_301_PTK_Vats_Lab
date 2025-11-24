namespace Character.REST.Models
{
    public class Warrior : CharacterClass 
    {
        public List<Weapon> Weapons { get; set; } = new();
    }
}
