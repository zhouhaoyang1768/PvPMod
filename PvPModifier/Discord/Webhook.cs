using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Reflection;
using TShockAPI;

namespace PvPModifier.Discord {
    public class Webhook {
        public DiscordWebhookClient Client { get; private set; }
        private static CancellationTokenSource _token = new CancellationTokenSource();
        private DiscordEmbedBuilder _baseBuilder;
        private DiscordWebhook _webhook;

        public async Task RunAsync() {
            if (!PvPModifier.Config.EnableDiscordWebhook) {
                return;
            }

            Client = new DiscordWebhookClient();
            _webhook = await Client.AddWebhookAsync(PvPModifier.Config.DiscordID, PvPModifier.Config.DiscordToken);

            _baseBuilder = new DiscordEmbedBuilder() {
                Color = new DiscordColor(PvPModifier.Config.DiscordColor)
            };

            try {
                await Task.Delay(-1, _token.Token);
            } catch (TaskCanceledException) {
                return;
            }
        }

        public void Shutdown() => _token.Cancel();

        public void Send(string item, string type, string oldValue, string value) {
            if (!PvPModifier.Config.EnableDiscordWebhook) {
                return;
            }

            var logBuilder = new DiscordEmbedBuilder(_baseBuilder);
            logBuilder.AddField(item, $"Changed {type} from {oldValue} to {value}");
            Client.BroadcastMessageAsync(new DiscordWebhookBuilder().AddEmbed(logBuilder.Build()));
        }
    }
}
