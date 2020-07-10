using Microsoft.EntityFrameworkCore;
using SYNTAXdb.DAL.Models.Profiles;
using SYNTAXdb.DAL.Models.Titles;

namespace SYNTAXdb.DAL
{
    public class TicketVendingProtocol : DbContext
    {

        public TicketVendingProtocol(DbContextOptions<TicketVendingProtocol> options) : base(options) { }
      
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<ProfileTitle> ProfileTitles { get; set; }
    }
}
