
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;
// using TodoApi.Models.HeroSearchPayload;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public HeroController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Hero
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<HeroItemDTO>>> GetHeroItems()
        // {
          
        //     return await _repositoryWrapper.HeroItems
        //     .Select(x => ItemToDTO(x))
        //     .ToListAsync();
        // }
        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroItemDTO>>> GetHeroItems()
        {
            var heroItems =  await _repositoryWrapper.HeroItem.FindAllAsync();
            return heroItems
                .Select(x => ItemToDTO(x))
                .ToList();
        }

        // GET: api/Hero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroItemDTO>> GetHeroItem(int id)
        {
        
            var heroItem = await _repositoryWrapper.HeroItem.FindByIDAsync(id);

            if (heroItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(heroItem);
        }

        // PUT: api/Hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroItem(int id, HeroItemDTO heroItemDTO)
        {
            if (id != heroItemDTO.Id)
            {
                return BadRequest();
            }

            var heroItem = await _repositoryWrapper.HeroItem.FindByIDAsync(id);
            if (heroItem == null)
            {
                return NotFound();
            }

            heroItem.Name = heroItemDTO.Name;
            heroItem.Address = heroItemDTO.Address;

            try
            {
                await _repositoryWrapper.HeroItem.UpdateAsync(heroItem);
            }
            catch (DbUpdateConcurrencyException) when (!HeroItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Hero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeroItemDTO>> PostHeroItem(HeroItemDTO heroItemDTO)
        {
          var heroItem = new HeroItem
        {
            Address = heroItemDTO.Address,
            Name = heroItemDTO.Name
        };

        
        await _repositoryWrapper.HeroItem.CreateAsync(heroItem, true);

        return CreatedAtAction(
            nameof(GetHeroItem),
            new { id = heroItem.Id },
            ItemToDTO(heroItem));
        }

        // DELETE: api/Hero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroItem(int id)
        {
            if (_repositoryWrapper.HeroItem == null)
            {
                return NotFound();
            }
            var heroItem = await _repositoryWrapper.HeroItem.FindByIDAsync(id);
            if (heroItem == null)
            {
                return NotFound();
            }

            // _repositoryWrapper.HeroItem.Delete(heroItem);
            await _repositoryWrapper.HeroItem.DeleteAsync(heroItem, true);

            return NoContent();
        }
        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<HeroItemDTO>>>  SearchHero(string term)
        {
            var empList = await _repositoryWrapper.HeroItem.SearchHero(term);
            return Ok(empList);           
        }

        [HttpPost("searchhero")]
        public async Task<ActionResult<IEnumerable<HeroItemDTO>>>  SearchHeroMultiple(HeroSearchPayload SearchObj)
        {
            var empList = await _repositoryWrapper.HeroItem.SearchHeroMultiple(SearchObj);
            return Ok(empList);           
        }


         private bool HeroItemExists(long id)
        {
            return _repositoryWrapper.HeroItem.IsExists(id);
        }

        private static HeroItemDTO ItemToDTO(HeroItem heroItem) =>
            new HeroItemDTO
            {
                Id = heroItem.Id,
                Name = heroItem.Name,
                Address = heroItem.Address
            };
    }
}
