using Character.Infrastructure;
using Character.REST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DbWarrior = Character.Infrastructure.Models.Warrior;
using DbWeapon = Character.Infrastructure.Models.WeaponModel;

namespace Character.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarriorController : ControllerBase
    {

        private readonly ICrudServiceAsync<DbWarrior> _warriorService;

        public WarriorController(ICrudServiceAsync<DbWarrior> warriorService)
        {
            _warriorService = warriorService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Warrior>>> GetAll()
        {
            var entities = await _warriorService.ReadAllAsync();

            var response = entities.Select(w => new Models.Warrior
            {
                Name = w.Name,
                MoveSpeed = w.MoveSpeed,
                Luck = w.Luck,
                Weapons = w.Weapons.Select(wp => new Models.Weapon
                {
                    WeaponName = wp.WeaponName,
                    WeaponDamage = wp.WeaponDamage,
                    WeaponLevel = wp.WeaponLevel
                }).ToList()
            });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Warrior>> GetById(Guid id)
        {
            var entity = await _warriorService.ReadAsync(id);
            if (entity == null) return NotFound();

            var response = new Models.Warrior
            {
                Name = entity.Name,
                MoveSpeed = entity.MoveSpeed,
                Luck = entity.Luck,
                Weapons = entity.Weapons.Select(wp => new Models.Weapon
                {
                    WeaponName = wp.WeaponName,
                    WeaponDamage = wp.WeaponDamage,
                    WeaponLevel = wp.WeaponLevel
                }).ToList()
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarriorRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newWarrior = new DbWarrior
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                MoveSpeed = request.MoveSpeed,
                Luck = request.Luck,
                Level = 1,
                HitPoint = 100,
                Weapons = request.StartingWeapons.Select(w => new DbWeapon
                {
                    Id = Guid.NewGuid(),
                    WeaponName = w.WeaponName,
                    WeaponDamage = w.WeaponDamage,
                    WeaponLevel = w.WeaponLevel
                }).ToList()
            };

            var created = await _warriorService.CreateAsync(newWarrior);
            if (created)
            {
                return CreatedAtAction(nameof(GetById), new { id = newWarrior.Id }, newWarrior);
            }
            return BadRequest("Could not create warrior");
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WarriorRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _warriorService.ReadAsync(id);
            if (existing == null) return NotFound();

            existing.Name = request.Name;
            existing.MoveSpeed = request.MoveSpeed;
            existing.Luck = request.Luck;


            var updated = await _warriorService.UpdateAsync(existing);
            if (updated) return NoContent();

            return BadRequest("Could not update warrior");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _warriorService.ReadAsync(id);
            if (entity == null) return NotFound();

            await _warriorService.RemoveAsync(entity);
            return NoContent();
        }
    }
}
