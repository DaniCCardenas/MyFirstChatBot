using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if(message.Text.ToLower().Equals("Hola", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call(new SaludoDialog(), AfterChildDialogIsDone);
            }
            else if (message.Text.ToLower().Equals("Salir", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call(new SalirDialog(), AfterChildDialogIsDone);
            }
            else if(message.Text.ToLower().Equals("Mostrar tarjeta", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call(new HeroCardDialog(), AfterChildDialogIsDone);
            }
            else if(message.Text.ToLower().Equals("Orden", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Forward(new NuevaOrdenDialog(), ResumeAfterNewOrderDialog, message, CancellationToken.None);
            }
            else
            {
                context.Wait(MessageReceivedAsync);
            }

        }


        private async Task AfterChildDialogIsDone(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<object> result)
        {
            // Store the value that NewOrderDialog returned. 
            // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
            var resultFromNewOrder = await result;

            await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");

            // Again, wait for the next message from the user.
            context.Wait(MessageReceivedAsync);
        }


    }
}