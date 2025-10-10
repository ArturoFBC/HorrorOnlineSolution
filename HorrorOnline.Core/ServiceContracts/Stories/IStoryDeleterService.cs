using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Stories
{
    public interface IStoryDeleterService
    {
        public Task<bool> DeleteStoryById(Guid? storyID);
    }
}
