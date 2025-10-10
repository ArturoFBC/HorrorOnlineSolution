using HorrorOnline.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Tags
{
    public interface ITagGetterService
    {
        public Task<TagResponse?> GetTagByID(Guid? guid);

        public Task<TagResponse?> GetTagByName(string? name);

        public Task<IEnumerable<TagResponse>> GetAllTags(); 
    }
}
