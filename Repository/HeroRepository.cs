using System.Data;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Repositories
{
    public class HeroRepository : RepositoryBase<HeroItem>, IHeroRepository
    {
        public HeroRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<HeroItem>> SearchHero(string searchTerm)
        {
            return await RepositoryContext.HeroItems
                        .Where(s => s.Name.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<HeroItem>> SearchHeroMultiple(HeroSearchPayload SearchObj)
        {
            return await RepositoryContext.HeroItems
                        .Where(s => s.Name.Contains(SearchObj.NameTerm ?? "") || s.Address.Contains(SearchObj.AddressTerm ?? ""))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public bool IsExists(long id)
        {
            return RepositoryContext.HeroItems.Any(e => e.Id == id);
        }
    }

}