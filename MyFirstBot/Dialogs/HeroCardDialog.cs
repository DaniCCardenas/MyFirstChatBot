using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class HeroCardDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Voy a mostrar una tarjeta...");
            await MostrarTarjeta(context);
        }

        public async Task MostrarTarjeta(IDialogContext context)
        {

            var message = context.MakeMessage();

            var attachment = GetHeroCard();
            message.Attachments.Add(attachment);

            await context.PostAsync(message);

            context.Done(new object());
        }

        private Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "BotFramework Hero Card",
                Subtitle = "Your bots — wherever your users are talking",
                Text = "Build and connect intelligent bots to interact with your users naturally wherever they are, from text/sms to Skype, Slack, Office 365 mail and other popular services.",
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Get Started", value: "https://docs.microsoft.com/bot-framework") }
            };

            return heroCard.ToAttachment();
        }

    }
}