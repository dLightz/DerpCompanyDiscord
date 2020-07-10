using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using SYNTAXdb.DAL;
using SYNTAXdb.DAL.Models.Profiles;
using SYNTAXdb.DAL.Models.Titles;
using SYNTAXMain.Core.Services.Profiles;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYNTAXMain.Core.Services.Titles
{
    public interface ITitleService
    {
        Task CreateNewTitleAsync(Title title);
        //Task<Title> GetTitleCatalogue(DiscordMember member,string redeemed);
        //Task<bool> PurchaseTitleAsync(ulong discordId, ulong guildId, string discordName, string titleName);
    }

    public class TitleService : ITitleService
    {
        private readonly DbContextOptions<TicketVendingProtocol> _options;
        private readonly IProfileService _profileService;

        public TitleService(DbContextOptions<TicketVendingProtocol> options, IProfileService profileService)
        {
            _options = options;
            _profileService = profileService;
        }

        public async Task CreateNewTitleAsync(Title title)
        {
            using var context = new TicketVendingProtocol(_options);

            context.Add(title);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        //public async Task<Title> GetTitleCatalogue(DiscordMember member, string redeemed)
        //{
        //    using var context = new TicketVendingProtocol(_options);

        //    return await context.Titles.ToArrayAsync(x => x.Redeemed == redeemed).ConfigureAwait(false);

        //}

        //public async Task<bool> PurchaseTitleAsync(ulong discordId, ulong guildId, string discordName, string titleId)
        //{
        //    using var context = new TicketVendingProtocol(_options);

        //    Title title = await GetRandomTitleByIdAsync(titleName).ConfigureAwait(false);

        //    if (title == null) { return false; }

        //    Profile profile = await _profileService.GetOrCreateProfileAsync(discordId, guildId, discordName).ConfigureAwait(false);

        //    if (profile.Tickets < title.Price) { return false; }

        //    profile.Tickets -= title.Price;
        //    profile.Titles.Add(new ProfileTitle
        //    {
        //        TitleId = title.Id,
        //        ProfileId = profile.Id
        //    });

        //    context.Profiles.Update(profile);

        //    await context.SaveChangesAsync().ConfigureAwait(false);

        //    return true;
        //}
    }
}
