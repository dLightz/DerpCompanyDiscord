using SYNTAXdb.DAL.Models.Titles;
using System.Collections.Generic;

namespace SYNTAXdb.DAL.Models.Profiles
{
    public class Profile : Entity
    {
        public ulong DiscordId { get; set; }
        public string DiscordName { get; set; }
        public ulong GuildId { get; set; }
        public int Tickets { get; set; }

        public List<ProfileTitle> Titles { get; set; } = new List<ProfileTitle>();
    }
}
