using Character.Infrastructure;
using Character.REST.Models;
using Microsoft.AspNetCore.Mvc;

using DbQuest = Character.Infrastructure.Models.Quests;

namespace Character.REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class QuestController : ControllerBase
    {

        private readonly ICrudServiceAsync<DbQuest> _questService;

        public QuestController(ICrudServiceAsync<DbQuest> questService)
        {
            _questService = questService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetAll()
        {
            var entities = await _questService.ReadAllAsync();
            var response = entities.Select(q => new Quest
            {
                Id = q.Id,
                QuestName = q.QuestName,
                QuestDescription = q.QuestDescription,
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetById(Guid id)
        {
            var entity = await _questService.ReadAsync(id);
            if (entity == null) return NotFound();

            var response = new Quest
            {
                Id = entity.Id,
                QuestName = entity.QuestName,
                QuestDescription = entity.QuestDescription
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newQuest = new DbQuest
            {
                Id = Guid.NewGuid(),
                QuestName = request.Title,
                QuestDescription = request.Description
            };

            var created = await _questService.CreateAsync(newQuest);
            if (created)
            {
                var response = new Quest
                {
                    Id = newQuest.Id,
                    QuestName = newQuest.QuestName,
                    QuestDescription = newQuest.QuestDescription
                };
                return CreatedAtAction(nameof(GetById), new { id = newQuest.Id }, response);
            }
            return BadRequest("Could not create quest");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] QuestRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _questService.ReadAsync(id);
            if (existing == null) return NotFound();

            existing.QuestName = request.Title;
            existing.QuestDescription = request.Description;

            var updated = await _questService.UpdateAsync(existing);
            if (updated) return NoContent();

            return BadRequest("Could not update quest");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _questService.ReadAsync(id);
            if (entity == null) return NotFound();

            await _questService.RemoveAsync(entity);
            return NoContent();
        }

    }
}
