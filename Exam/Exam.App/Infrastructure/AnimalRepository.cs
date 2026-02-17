using Exam.App.Domain;
using Exam.App.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Repository
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private AppDbContext _context;

        public AnimalsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnimalType>> GetAllAsync()
        {
            List<AnimalType> animalTypes = new List<AnimalType>();
            animalTypes = await _context.AnimalTypes
                .ToListAsync();
            return animalTypes;
        }

    }
}
