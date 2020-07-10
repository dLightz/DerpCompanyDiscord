using Microsoft.EntityFrameworkCore;
using SYNTAXdb.DAL;
using SYNTAXdb.DAL.Models.Profiles;
using SYNTAXMain.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SYNTAXMain.Core.Services.Profiles
{
    public interface ITicketService
    {
        Task<GrantTicketViewModel> GrantTicketsAsync(ulong discordId, ulong guildId, string discordName, int tickets);
        Task<GrantTicketViewModel> RemoveTicketsAsync(ulong discordId, ulong guildId, string discordName, int tickets);

    }

    public class TicketService : ITicketService
    {
        private readonly DbContextOptions<TicketVendingProtocol> _options;
        private readonly IProfileService _profileService;

        public TicketService(DbContextOptions<TicketVendingProtocol> options, IProfileService profileService)
        {
            _options = options;
            _profileService = profileService;
        }

        public async Task<GrantTicketViewModel> GrantTicketsAsync(ulong discordId, ulong guildId, string discordName, int tickets)
        {
            using var context = new TicketVendingProtocol(_options);

            Profile profile = await _profileService.GetOrCreateProfileAsync(discordId, guildId, discordName).ConfigureAwait(false);

            int ticketsBefore = profile.Tickets;

            profile.Tickets += tickets;

            context.Profiles.Update(profile);

            await context.SaveChangesAsync().ConfigureAwait(false);

            int ticketsAfter = profile.Tickets;

            return new GrantTicketViewModel
            {
                Profile = profile,
                Ticketschanged = ticketsBefore != ticketsAfter
            };
        }

        public async Task<GrantTicketViewModel> RemoveTicketsAsync(ulong discordId, ulong guildId, string discordName, int tickets)
        {
            using var context = new TicketVendingProtocol(_options);

            Profile profile = await _profileService.GetOrCreateProfileAsync(discordId, guildId, discordName).ConfigureAwait(false);

            int ticketsBefore = profile.Tickets;

            profile.Tickets -= tickets;

            context.Profiles.Update(profile);

            await context.SaveChangesAsync().ConfigureAwait(false);

            int ticketsAfter = profile.Tickets;

            return new GrantTicketViewModel
            {
                Profile = profile,
                Ticketschanged = ticketsBefore != ticketsAfter
            };
        }
    }
}
