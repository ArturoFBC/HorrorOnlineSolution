using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorOnline.Core.Domain.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<BookMark>? BookMarks { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

        public virtual ICollection<Story>? Stories { get; set; }
    }
}
