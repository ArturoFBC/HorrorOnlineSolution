using Microsoft.AspNetCore.Identity;


namespace HorrorOnline.Core.Domain.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<BookMark>? BookMarks { get; } = new List<BookMark>();

        public virtual ICollection<Review>? Reviews { get; } = new List<Review>();

        public virtual ICollection<Story>? Stories { get; } = new List<Story>();
    }
}
