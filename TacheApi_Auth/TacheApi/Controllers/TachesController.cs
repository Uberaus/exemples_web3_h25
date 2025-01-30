using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacheApi.Models;

namespace TacheApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TachesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TachesController(AppDbContext context)
        {
            _context = context;
        }

        [EndpointSummary("Récupère toutes les tâches")]
        [EndpointDescription("Récupère toutes les tâches de la base de données")]
        [ProducesResponseType<IEnumerable<TacheDTO>>(StatusCodes.Status200OK, "application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTaches()
        {
            return await _context.Taches
                .Select(tache => new TacheDTO(tache))
                .ToListAsync();
        }


        [EndpointSummary("Récupère une tâche")]
        [EndpointDescription("Récupère une tâche de la base de données en fonction de son identifiant")]
        [ProducesResponseType<TacheDetailsDTO>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TacheDetailsDTO>> GetTache(
            [Description("L'identifiant de la tâche à récupérer")] long id)
        {
            var tache = await _context.Taches
                .Include(tache => tache.Etapes)
                .SingleOrDefaultAsync(tache => tache.Id == id);

            if (tache == null)
            {
                return NotFound();
            }

            return new TacheDetailsDTO(tache);
        }


        [EndpointSummary("Met à jour une tâche")]
        [EndpointDescription("Met à jour une tâche de la base de données en fonction de son identifiant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTache(
            [Description("L'identifiant de la tâche à modifier")] long id,
            [FromBody][Description("La tâche modifiée")] TacheUpsertDTO tacheDTO)
        {
            var tache = await _context.Taches.FindAsync(id);

            if (tache == null)
            {
                return NotFound();
            }

            tache.AppliquerUpsertDTO(tacheDTO);

            _context.Entry(tache).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [EndpointSummary("Ajoute une tâche")]
        [EndpointDescription("Ajoute une tâche dans la base de données")]
        [ProducesResponseType<TacheDTO>(StatusCodes.Status201Created, "application/json")]
        [HttpPost]
        public async Task<ActionResult<TacheDTO>> PostTache(
            [FromBody][Description("La tâche à ajouter")] TacheUpsertDTO tacheDTO)
        {
            Tache tache = new Tache(tacheDTO);
            _context.Taches.Add(tache);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTache), new { id = tache.Id }, new TacheDTO(tache));
        }


        [EndpointSummary("Supprime une tâche")]
        [EndpointDescription("Supprime une tâche de la base de données en fonction de son identifiant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTache(
              [Description("L'identifiant de la tâche à supprimer")] long id)
        {
            var tache = await _context.Taches
                .Include(tache => tache.Etapes)
                .SingleOrDefaultAsync(tache => tache.Id == id);

            if (tache == null)
            {
                return NotFound();
            }

            _context.Taches.Remove(tache);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TacheExists(long id)
        {
            return _context.Taches.Any(e => e.Id == id);
        }
    }
}
