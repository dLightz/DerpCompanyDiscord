using Microsoft.EntityFrameworkCore;
using SYNTAXdb.DAL;
using SYNTAXdb.DAL.Models.Profiles;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SYNTAXMain.Core.ViewModels;

namespace SYNTAXMain.Core.Services.Profiles

{
    public interface IProfileService
    {
        Task<Profile> GetOrCreateProfileAsync(ulong discordId, ulong guildId, string discordName);

     }

    public class ProfileService : IProfileService
    {
        private readonly DbContextOptions<TicketVendingProtocol> _options;

        public ProfileService(DbContextOptions<TicketVendingProtocol> options)
        {
            _options = options;

        }

        public async Task<Profile> GetOrCreateProfileAsync(ulong discordId, ulong guildId, string discordName)
        {
            using var context = new TicketVendingProtocol(_options);

            var profile = await context.Profiles
                .Where(x => x.GuildId == guildId)
                .FirstOrDefaultAsync(x => x.DiscordId == discordId).ConfigureAwait(false);

            if (profile != null) { return profile; }

            profile = new Profile
            {
                DiscordId = discordId,
                GuildId = guildId,
                Tickets = 0,
                DiscordName = discordName
            };

            context.Add(profile);

            await context.SaveChangesAsync().ConfigureAwait(false);

            return profile;
        }
    }
}
