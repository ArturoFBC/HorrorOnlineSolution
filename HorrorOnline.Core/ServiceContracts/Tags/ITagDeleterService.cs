using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.ServiceContracts.Tags
{
    public interface ITagDeleterService
    {
        public Task<bool> DeleteTagById(Guid? tagID);
    }
}
