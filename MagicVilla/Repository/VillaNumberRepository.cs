using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Repository.IRepository;

namespace MagicVilla.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDBContext _db;
        public VillaNumberRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _db.VillasNumber.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
