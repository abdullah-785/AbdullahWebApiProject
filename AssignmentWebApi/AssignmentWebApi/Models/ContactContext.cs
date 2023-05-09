using Microsoft.EntityFrameworkCore;

namespace AssignmentWebApi.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }
        public DbSet<Contact> contact { get; set; }
    }
}
