using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class SaludoDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hola, ¿como te llamas?");
            context.Wait(MessageSaludoReceivedAsync);
        }

        public async Task MessageSaludoReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            await context.PostAsync($"Bienvenido {message.Text}");
            context.Done(new object());
        }
    }
}