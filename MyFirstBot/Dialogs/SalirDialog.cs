using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class SalirDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "¿Seguro que te quieres ir?",
                    "La respuesta no es valida",
                    promptStyle: PromptStyle.Keyboard);
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("Espero verte pronto!");
            }
            else
            {
                await context.PostAsync("¿No te vas? ¡Me alegro!");
            }

            context.Done(new object());
        }
    }
}