using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace MiniFoodOrderUsingLuis.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        public RootDialog() : base(new LuisService(new LuisModelAttribute(
           ConfigurationManager.AppSettings["7b90b679-aaef-458e-9b9d-33a03cf9fd70"],
           ConfigurationManager.AppSettings["12b0368d58924683b7b0907c56a89525"],
           domain: ConfigurationManager.AppSettings["MiniFoodOrder"])))
        {
        }
        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
        [LuisIntent("None")]
        private async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            var activity =result;
            await this.ShowLuisResult(context, result);
        }
    }
}