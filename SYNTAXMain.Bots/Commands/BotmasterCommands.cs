using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using SYNTAXMain.Bots.Handlers.Dialogue;
using SYNTAXMain.Bots.Handlers.Dialogue.Steps;
using SYNTAXMain.Core.Services.Profiles;
using SYNTAXMain.Core.Services.Titles;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots.Commands
{
    public class BotmasterCommands : BaseCommandModule
    {

        [Command("afterme")]
        [RequireRoles(RoleCheckMode.Any, "Botmaster")]
        [Hidden]
        public async Task AfterMe(CommandContext ctx)
        {
            var _repeatchannel = ctx.Guild.Channels.Values.First(x => x.Id == 725160401150410822);

            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await _repeatchannel.SendMessageAsync($"{message.Result.Content}");
        }

        [Command("aftermeembed")]
        [RequireRoles(RoleCheckMode.Any, "Botmaster")]
        [Hidden]
        public async Task AfterMeEmbed(CommandContext ctx)
        {
            var emojiStep = new ReactionStep("Acknowledge", new Dictionary<DiscordEmoji, ReactionStepData> 
            {
                {DiscordEmoji.FromName(ctx.Client, ":ok_hand:"), new ReactionStepData { Content = "React to Continue", NextStep = null } },       
            });

            
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = ctx.Client.CurrentUser.AvatarUrl
            };

            var reactEmbed = new DiscordEmbedBuilder
            {
                Title = "cmVxdWVzdGluZyBlcnJvciBzdHJpbmc=",

                Thumbnail = thumbnailWorkAround,

                Description = message.Result.Content,

                Color = DiscordColor.Purple
            };

            var _repeatchannel = ctx.Guild.Channels.Values.First(x => x.Id == 725160401150410822);

            var aftermeembed = await _repeatchannel.SendMessageAsync(embed: reactEmbed).ConfigureAwait(false);
           // var okhandEmoji = DiscordEmoji.FromName(ctx.Client, ":ok_hand:");

           // await aftermeembed.CreateReactionAsync(okhandEmoji).ConfigureAwait(false);
        }
        
        [Command("addroletohere")]
        [RequireRoles(RoleCheckMode.Any, "Botmaster")]
        [Hidden]
        public Task AddRoleToHereUsers(CommandContext ctx, DiscordRole discordRole)
        {
            var role = ctx.Guild.GetRole(discordRole.Id);

            var memberhere = ctx.Channel.Users.ToList();

            memberhere.ForEach(x => x.GrantRoleAsync(role));

            memberhere.ForEach(async x =>
            {
                await ctx.Channel.SendMessageAsync($"{x.DisplayName} added to {role.Name} role").ConfigureAwait(false);
            });

            return Task.CompletedTask;
        }
    }
}
