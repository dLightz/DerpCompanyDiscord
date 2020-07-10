using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SYNTAXMain.Bots.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        [Command("ping")]
        [Description("Connection Test")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);

        }

        [Command("derp")]
        [Description("b25saXRyY3Z3YmRlamdwbWFodXNxZnhrenk=")]
        public async Task Derp(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Herp.").ConfigureAwait(false);
        }

        [Command("derpcompany")]
        [Description("b25saXRyY3Z3YmRlamdwbWFodXNxZnhrenk=")]
        [Hidden]
        public async Task DerpIdent(CommandContext ctx)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                int delay = 3600000;
                await ctx.Channel.SendMessageAsync("'TVP INTEGRATION COMMENCING. STAND BY.'").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 0/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 5/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 10/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 15/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 20/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 25/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 30/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 35/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 40/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 45/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 50/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 55/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 60/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 65/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 70/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 75/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 80/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 85/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 90/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 95/100%'").ConfigureAwait(false);
                await Task.Delay(delay).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'PROCESSING. 100/100%'").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("'DERP_COMPANY INTEGRATED. REBOOT REQUIRED. SYNTAX SHUTTING DOWN.'").ConfigureAwait(false);
                await ctx.Client.DisconnectAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        [Command("hello")]
        [Description("Simple reply for a simple greeting.")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello. I am SYNTAX.").ConfigureAwait(false);
        }

        [Command("pendingtickets")]
        public async Task Pendingtickets(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("DISTRIBUTION PENDING").ConfigureAwait(false);
        }
    }
}
