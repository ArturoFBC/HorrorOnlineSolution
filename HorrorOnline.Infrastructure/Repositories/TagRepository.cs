using HorrorOnline.Core.Domain.Entities;
using HorrorOnline.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HorrorOnline.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        public static List<Tag> Tags = new List<Tag>();

        public TagRepository()
        {   
            //Seed data
            string tagsJson = File.ReadAllText("tags.json");
            Tags = JsonSerializer.Deserialize<List<Tag>>(tagsJson);
        }

        public async Task<Tag> AddTag(Tag tag)
        {
            Tags.Add(tag);

            return tag;
        }

        public async Task<bool> DeleteTagByID(Guid tagID)
        {
            Tag? foundTag = Tags.Find(item => item.TagId == tagID);

            if (foundTag == null)
            {
                return false;
            }
            else
            {
                return Tags.Remove(foundTag);
            }
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return new List<Tag>(Tags);
        }

        public async Task<Tag?> GetTagByID(Guid tagID)
        {
            Tag? foundTag = Tags.Find(item => item.TagId == tagID);

            return foundTag;
        }

        public async Task<Tag?> GetTagByName(string name)
        {
            Tag? foundTag = Tags.Find(item => item.TagName == name);

            return foundTag;
        }
    }
}
