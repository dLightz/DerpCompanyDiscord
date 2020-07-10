using SYNTAXMain.Core.Services.Profiles;
using SYNTAXdb.DAL.Models.Profiles;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots.Commands
{
    public class ProfileCommands : BaseCommandModule
    {
        private readonly IProfileService _profileService;
        private readonly ITicketService _ticketService;

        public ProfileCommands(IProfileService profileService, ITicketService ticketService)
        {
            _profileService = profileService;
            _ticketService = ticketService;
        }

        [Command("loadallprofiles")]
        [RequireRoles(RoleCheckMode.Any, "Botmaster")]
        [Hidden]
        public async Task LoadAllProfiles(CommandContext ctx)
        {
            var profile = ctx.Channel.Users.ToList();
            profile.ForEach(async x =>
            {
                await _profileService.GetOrCreateProfileAsync(x.Id, ctx.Guild.Id, x.DisplayName).ConfigureAwait(false);
            });

            profile.ForEach(async x =>
            {
                await ctx.Member.SendMessageAsync($"{x.DisplayName} - profile created. Ticket registry remains offline.").ConfigureAwait(false);
            });

            await Task.Delay(-1);
        }

        [Command("checktickets")]       
        public async Task Profile(CommandContext ctx)
        {
            await GetProfileToDisplayAsync(ctx, ctx.Member.Id, ctx.Guild.Id, ctx.Member.DisplayName);
        }

        [Command("checktickets")]
        public async Task Profile(CommandContext ctx, DiscordMember member)
        {
            await GetProfileToDisplayAsync(ctx, member.Id, member.Guild.Id ,member.DisplayName);
        }

        private async Task GetProfileToDisplayAsync(CommandContext ctx, ulong memberId, ulong guildId, string discordName)
        {
            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, guildId, discordName).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];
            
            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = member.AvatarUrl
            };

            var profileEmbed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName}'s Ticket Reel",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Purple
            };

            profileEmbed.AddField("Tickets", profile.Tickets.ToString());

            await ctx.Channel.SendMessageAsync(embed: profileEmbed).ConfigureAwait(false);
        }

    }
}
