using Character.Infrastructure;
using Character.REST.Models;
using Microsoft.AspNetCore.Mvc;
using DbMage = Character.Infrastructure.Models.Mage;

namespace Character.REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MageController : ControllerBase
    {
        private readonly ICrudServiceAsync<DbMage> _mageService;

        public MageController(ICrudServiceAsync<DbMage> mageService)
        {
            _mageService = mageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mage>>> GetAll()
        {
            var entities = await _mageService.ReadAllAsync();
            var response = entities.Select(m => new Mage
            {
                Id = m.Id,
                Name = m.Name,
                MoveSpeed = m.MoveSpeed,
                Luck = m.Luck,
                Level = m.Level,
                HitPoint = m.HitPoint,
                CanUseSpell = m.CanUseSpell
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mage>> GetById(Guid id)
        {
            var entity = await _mageService.ReadAsync(id);
            if (entity == null) return NotFound();

            var response = new Mage
            {
                Id = entity.Id,
                Name = entity.Name,
                MoveSpeed = entity.MoveSpeed,
                Luck = entity.Luck,
                Level = entity.Level,
                HitPoint = entity.HitPoint,
                CanUseSpell = entity.CanUseSpell
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MageRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newMage = new DbMage
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                MoveSpeed = request.MoveSpeed,
                Luck = request.Luck,
                Level = 1,
                HitPoint = 80,
                CanUseSpell = true
            };

            var created = await _mageService.CreateAsync(newMage);
            if (created)
            {

                var response = new Mage
                {
                    Id = newMage.Id,
                    Name = newMage.Name,
                    MoveSpeed = newMage.MoveSpeed
                };
                return CreatedAtAction(nameof(GetById), new { id = newMage.Id }, response);
            }
            return BadRequest("Could not create mage");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MageRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _mageService.ReadAsync(id);
            if (existing == null) return NotFound();

            existing.Name = request.Name;
            existing.MoveSpeed = request.MoveSpeed;
            existing.Luck = request.Luck;

            var updated = await _mageService.UpdateAsync(existing);
            if (updated) return NoContent();

            return BadRequest("Could not update mage");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _mageService.ReadAsync(id);
            if (entity == null) return NotFound();

            await _mageService.RemoveAsync(entity);
            return NoContent();
        }
    }
}
