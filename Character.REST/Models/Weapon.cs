namespace Character.REST.Models
{
    public class Weapon
    {
        public Guid Id { get; set; }
        public string? WeaponName { get; set; }
        public int WeaponLevel { get; set; }
        public float WeaponDamage { get; set; }

    }
}
