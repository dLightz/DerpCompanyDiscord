using DSharpPlus;
using DSharpPlus.Entities;
using SYNTAXMain.Bots.Handlers.Dialogue.Steps;
using System;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots.Handlers.Dialogue
{
    public abstract class DialogueStepBase : IDialogueStep
    {
        protected readonly string _content;

        public DialogueStepBase(string content)
        {
            _content = content;
        }

        public Action<DiscordMessage> OnMessageAdded { get; set; } = delegate { };

        public abstract IDialogueStep NextStep { get; }

        public abstract Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user, DiscordMember member);
        public abstract Task<bool> ProcessRemoveStep(DiscordClient client, DiscordChannel channel, DiscordUser user, DiscordMember member);


        protected async Task TryAgain(DiscordChannel channel, string problem)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Please Try Again",
                Color = DiscordColor.Red,
            };

            embedBuilder.AddField("There was a problem with your previous input", problem);

            var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

            OnMessageAdded(embed);
        }
    }
}
