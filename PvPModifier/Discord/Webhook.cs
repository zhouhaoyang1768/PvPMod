using Discord;
using Discord.Webhook;
using PvPModifier.Utilities;
using PvPModifier.Utilities.PvPConstants;

namespace PvPModifier.Discord {

    public class Webhook {
        private DiscordWebhookClient _client;

        public Webhook() {
            if (!PvPModifier.Config.EnableDiscordWebhook) {
                return;
            }

            _client = new DiscordWebhookClient(PvPModifier.Config.DiscordWebhookID, PvPModifier.Config.DiscordWebhoookToken);
        }

        public void Send(string user, string type, string item, string attribute, string oldValue, string value) {
            if (!PvPModifier.Config.EnableDiscordWebhook) {
                return;
            }

            var ea = new EmbedAuthorBuilder {
                Name = type
            };

            var ef = new EmbedFooterBuilder {
                Text = user
            };

            var eb = new EmbedBuilder {
                Author = ea,
                Footer = ef
            };

            if (attribute.Equals(DbConsts.Shoot)) {
                oldValue = string.Join(" ", MiscUtils.GetNameFromInput(DbTables.ProjectileTable, int.Parse(oldValue)), $"({oldValue})");
                value = string.Join(" ", MiscUtils.GetNameFromInput(DbTables.ProjectileTable, int.Parse(value)), $"({value})");
            }

            eb.AddField(item, $"Changed {attribute} from {oldValue} to {value}");

            Embed[] embedArray = new Embed[] { eb.Build() };

            _client.SendMessageAsync(embeds: embedArray);
        }
    }
}