using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using SYNTAXMain.Core.Services.Profiles;
using SYNTAXMain.Core.Services.Titles;
using SYNTAXdb.DAL.Models.Titles;
using System.Threading.Tasks;
using SYNTAXMain.Bots.Handlers.Dialogue;
using SYNTAXMain.Bots.Handlers.Dialogue.Steps;
using SYNTAXdb.DAL.Models.Profiles;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using SYNTAXMain.Core.ViewModels;
using SYNTAXdb.DAL;
using System.Collections.Generic;

namespace SYNTAXMain.Bots.Commands
{
    public class TvpCommands : BaseCommandModule
    {
        private readonly ITitleService _titleService;
        private readonly IProfileService _profileService;
        private readonly ITicketService _ticketService;

        public TvpCommands(ITitleService titleService, IProfileService profileService, ITicketService ticketService)
        {
            _titleService = titleService;
            _profileService = profileService;
            _ticketService = ticketService;
        }

        [Command("addticket")]
        [RequireRoles(RoleCheckMode.Any, "Keymaster")]

        public async Task ProfileTicketAdd(CommandContext ctx, DiscordMember member)
        {
            await AddTickets(ctx, member.Id, member.Guild.Id);
        }

        public async Task AddTickets(CommandContext ctx, ulong memberId, ulong guildId)
        {
            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, guildId, null).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            var howmanyStep = new IntStep("Enter ticket amount:", null, minValue: 1, null);

            int response = 0;

            howmanyStep.OnValidResult += (result) => response = result;

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                member,
                howmanyStep
                );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            int tickets = response;

            await AddTicketLogic(ctx, memberId, guildId, member.DisplayName, tickets);

        }

        private async Task AddTicketLogic(CommandContext ctx, ulong memberId, ulong guildId, string discordName, int tickets)
        {

            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, guildId, discordName).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            GrantTicketViewModel viewModel = await _ticketService.GrantTicketsAsync(member.Id, member.Guild.Id, member.DisplayName, tickets).ConfigureAwait(false);

            if (!viewModel.Ticketschanged) { return; }

            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = member.AvatarUrl
            };

            var ticketsChangedEmbed = new DiscordEmbedBuilder
            {
                Title = "Ticket-Vending-Protocol.AddTickets",
                Description = $"{tickets} ticket(s) added to {member.DisplayName}!",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Green
            };

            await ctx.Channel.SendMessageAsync(embed: ticketsChangedEmbed).ConfigureAwait(false);

            await GetProfileToDisplayAsyncAdd(ctx, memberId, guildId, discordName);
        }
        private async Task GetProfileToDisplayAsyncAdd(CommandContext ctx, ulong memberId, ulong guildId, string discordName)
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

        [Command("removeticket")]
        [RequireRoles(RoleCheckMode.Any, "Keymaster")]
        public async Task ProfileTicketRemove(CommandContext ctx, DiscordMember member)
        {
            await RemoveTickets(ctx, member.Id, member.Guild.Id);
        }

        public async Task RemoveTickets(CommandContext ctx, ulong memberId, ulong guildId)
        {
            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, guildId, null).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            var howmanyStep = new IntStep("Enter ticket amount:", null, minValue: 1, null);

            int response = 0;

            howmanyStep.OnValidResult += (result) => response = result;

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                member,
                howmanyStep
                );

            bool succeeded = await inputDialogueHandler.ProcessRemoveDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            int tickets = response;

            await RemoveTicketLogic(ctx, memberId, guildId, member.DisplayName, tickets);

        }

        private async Task RemoveTicketLogic(CommandContext ctx, ulong memberId, ulong guildId, string discordName, int tickets)
        {

            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, guildId, discordName).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            GrantTicketViewModel viewModel = await _ticketService.RemoveTicketsAsync(member.Id, member.Guild.Id, member.DisplayName, tickets).ConfigureAwait(false);

            if (!viewModel.Ticketschanged) { return; }


            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = member.AvatarUrl
            };

            var ticketsChangedEmbed = new DiscordEmbedBuilder
            {
                Title = "Ticket-Vending-Protocol.AddTickets",
                Description = $"{tickets} ticket(s) removed from {member.DisplayName}'s ticket reel.",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: ticketsChangedEmbed).ConfigureAwait(false);

            await GetProfileToDisplayRemoveAsync(ctx, memberId, guildId, discordName);
        }

        private async Task GetProfileToDisplayRemoveAsync(CommandContext ctx, ulong memberId, ulong guildId, string discordName)
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


        //[Command("catalogue")]
        //public async Task Catalogue(CommandContext ctx)
        //{
        //    string redeemed = "Y";
        //    await _titleService.GetTitleCatalogue(ctx.Member, redeemed).ConfigureAwait(false);
        //    await ctx.Member.SendMessageAsync(<Title>).ConfigureAwait(false);
        //}

        [Command("redeemticket")]
        public async Task Redeemticket(CommandContext ctx) => await ctx.Channel.SendMessageAsync("REDEEMTICKET COMMAND FOUND. REGISTRY ERROR. AWAITING FURTHER PROCESSING").ConfigureAwait(false);
    }
}


