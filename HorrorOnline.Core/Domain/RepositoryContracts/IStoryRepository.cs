using HorrorOnline.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Domain.RepositoryContracts
{
    public interface IStoryRepository
    {
        Task<Story> AddStory(Story story);

        Task<IEnumerable<Story>> GetAllStories();

        Task<Story?> GetStoryByID(Guid storyID);

        Task<IEnumerable<Story>> GetFilteredStories(Expression<Func<Story, bool>> predicate);

        Task<bool> DeleteStoryByID(Guid storyID);

        Task<IEnumerable<Story>> GetStoryByTag(Guid tagID);
    }
}
