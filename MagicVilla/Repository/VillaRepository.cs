using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla.Repository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
       
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
