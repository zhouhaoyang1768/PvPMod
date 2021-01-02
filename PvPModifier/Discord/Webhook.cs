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

        public void Send(string user, string type, string item, string attribute, string value) {
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

            if (attribute.Equals(DbConsts.Shoot) || attribute.Equals(DbConsts.UseAmmoIdentifier)) {
                value = MiscUtils.GetNameIDProjectile(int.Parse(value));
            } else if (attribute.Equals(DbConsts.InflictBuffID) || attribute.Equals(DbConsts.ReceiveBuffID)) {
                value = MiscUtils.GetNameIDBuff(int.Parse(value));
            } else if (attribute.Equals(DbConsts.NotAmmo)) {
                value = attribute.Equals("0") ? "True" : "False";
            }

            eb.AddField(item, $"Changed {attribute} to {value}");

            Embed[] embedArray = new Embed[] { eb.Build() };

            _client.SendMessageAsync(embeds: embedArray);
        }
    }
}