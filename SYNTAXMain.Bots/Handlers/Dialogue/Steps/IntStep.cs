using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots.Handlers.Dialogue.Steps
{
    public class IntStep : DialogueStepBase
    {
        private readonly int? _minValue;
        private readonly int? _maxValue;

        private IDialogueStep _nextStep;

        public IntStep(
            string content,
            IDialogueStep nextStep,
            int? minValue = null,
            int? maxValue = null) : base(content)
        {
            _nextStep = nextStep;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public Action<int> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public void SetNextStep(IDialogueStep nextStep)
        {
            _nextStep = nextStep;
        }

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user, DiscordMember member)
        {
            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = member.AvatarUrl
            };

            var ticketEmbed = new DiscordEmbedBuilder
            {

                Title = "Ticket-Vending-Protocol.AddTickets",
                Description = $"How many tickets to give to {member.DisplayName}?",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Green
            };

            ticketEmbed.AddField("To Stop The Dialogue:", "Use the '!syn cancel' command");

            var interactivity = client.GetInteractivity();

            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: ticketEmbed).ConfigureAwait(false);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("!syn cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (!int.TryParse(messageResult.Result.Content, out int inputValue))
                {
                    await TryAgain(channel, $"Please try again with a valid # of tickets (Minimum:1, Format: Numbers ONLY)").ConfigureAwait(false);
                    continue;
                }

                if (_minValue.HasValue)
                {
                    if (inputValue < _minValue.Value)
                    {
                        await TryAgain(channel, $"Your input value: {inputValue} is smaller than: {_minValue}").ConfigureAwait(false);
                        continue;
                    }
                }
                if (_maxValue.HasValue)
                {
                    if (inputValue > _maxValue.Value)
                    {
                        await TryAgain(channel, $"Your input value {inputValue} is larger than {_maxValue}").ConfigureAwait(false);
                        continue;
                    }
                }

                OnValidResult(inputValue);

                return false;
            };
        }

        public override async Task<bool> ProcessRemoveStep(DiscordClient client, DiscordChannel channel, DiscordUser user, DiscordMember member)                   
        {           
            DiscordEmbedBuilder.EmbedThumbnail thumbnailWorkAround = new DiscordEmbedBuilder.EmbedThumbnail
            
            {
                Url = member.AvatarUrl
            };

            var ticketEmbed = new DiscordEmbedBuilder
            {

                Title = "Ticket-Vending-Protocol.AddTickets",
                Description = $"How many tickets to remove from {member.DisplayName}?",
                Thumbnail = thumbnailWorkAround,
                Color = DiscordColor.Orange
            };

            ticketEmbed.AddField("To Stop The Dialogue:", "Use the '!syn cancel' command");

            var interactivity = client.GetInteractivity();

            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: ticketEmbed).ConfigureAwait(false);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("!syn cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (!int.TryParse(messageResult.Result.Content, out int inputValue))
                {
                    await TryAgain(channel, $"Please try again with a valid # of tickets (Minimum:1, Format: Numbers ONLY)").ConfigureAwait(false);
                    continue;
                }

                if (_minValue.HasValue)
                {
                    if (inputValue < _minValue.Value)
                    {
                        await TryAgain(channel, $"Your input value: {inputValue} is smaller than: {_minValue}").ConfigureAwait(false);
                        continue;
                    }
                }
                if (_maxValue.HasValue)
                {
                    if (inputValue > _maxValue.Value)
                    {
                        await TryAgain(channel, $"Your input value {inputValue} is larger than {_maxValue}").ConfigureAwait(false);
                        continue;
                    }
                }

                OnValidResult(inputValue);

                return false;
            }
        }
    }
}