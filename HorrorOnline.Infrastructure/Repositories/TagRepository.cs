using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using HorrorOnline.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HorrorOnline.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;

        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Tag> AddTag(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();

            return tag;
        }

        public async Task<bool> DeleteTagByID(Guid tagID)
        {
            _db.Tags.RemoveRange(_db.Tags.Where(item => item.TagId == tagID));

            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _db.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByID(Guid tagID)
        {
            Tag? foundTag = await _db.Tags
                .Include(nameof(Story))
                .FirstOrDefaultAsync(item => item.TagId == tagID);

            return foundTag;
        }

        public async Task<Tag?> GetTagByName(string name)
        {
            Tag? foundTag = await _db.Tags
                .Include(nameof(Story))
                .FirstOrDefaultAsync(item => item.TagName == name);

            return foundTag;
        }
    }
}
