using Characters.Common;
using System;

namespace Character.Infrastructure.Models
{
    abstract public class CharacterClassModel : IEntity
    {
        //public delegate void CharacterActionHandler(string message);
        //public event CharacterActionHandler? OnAction;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public float MoveSpeed { get; set; }
        public bool CanUseSpell { get; set; } = false;
        public int Level { get; set; }
        public int HitPoint { get; set; }
        public float Luck { get; set; }


        public CharacterProfileModel? characterProfile { get; set; }

        //public CharacterClass(string name, int hp, int level)
        //{
        //    Name = name;
        //    HitPoint = hp;
        //    Level = level;
        //    MoveSpeed = 10.0f; 
        //}

        //public void TakeDamage(int damage)
        //{
        //    HitPoint -= damage;
        //    OnAction?.Invoke($"{Name} take {damage} damage. Hp : {HitPoint}");
        //}
    }
}
