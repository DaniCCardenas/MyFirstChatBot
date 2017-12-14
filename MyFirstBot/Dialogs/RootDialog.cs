using System;
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
            else if (message.Text.ToLower().Equals("Mostrar tarjeta", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call(new HeroCardDialog(), AfterChildDialogIsDone);
            }
            else      
            {
                await context.PostAsync("No te he entendido. Escribe Hola, Salir o Mostrar tarjeta para ver los disntintos comportamientos.");
                context.Wait(MessageReceivedAsync);
            }
               
            
        }

        private async Task AfterChildDialogIsDone(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }

    }
}