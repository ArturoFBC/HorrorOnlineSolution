using HorrorOnline.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Domain.RepositoryContracts
{
    public interface ITagRepository
    {
        Task<Tag> AddTag(Tag tag);

        Task<IEnumerable<Tag>> GetAllTags();

        Task<Tag?> GetTagByID(Guid tagID);

        Task<Tag?> GetTagByName(string name);

        Task<bool> DeleteTagByID(Guid tagID);
    }
}
