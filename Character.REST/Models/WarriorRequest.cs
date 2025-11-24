namespace Character.REST.Models
{
    public class WarriorRequest : CharacterClassRequest
    {
        public List<WeaponRequest> StartingWeapons { get; set; } = new();
    }
}
