using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class NuevaOrdenDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Se esta creando una nueva orden, espera por favor...");
            var idOrden = new Guid().ToString();

            context.Done<object>(idOrden); 
        }

    }
}