using HorrorOnline.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Tags
{
    public interface ITagAdderService
    {
        public Task<TagResponse> AddTag(TagAddRequest tagAddRequest);
    }
}
