using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    [Serializable]
    public class PizzaOrder : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var replymess = activity.CreateReply();

            try
            {
                var ReplyMessage = context.MakeMessage();
                Attachment attachment = PizzaOrderMethod();
                ReplyMessage.Attachments = new List<Attachment> { attachment };
                await context.PostAsync(ReplyMessage);

            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
            }

        }


        private static Attachment PizzaOrderMethod()
        {

            try
            {
                var herocard = new Microsoft.Bot.Connector.HeroCard

                {
                    Title = "Pizza",
                    Subtitle = "Experience The Savage",
                    Text = "Choose The Pizza",
                    Images = new List<CardImage> { new CardImage("https://images5.alphacoders.com/396/thumb-1920-396575.jpg") },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Regular", value: "Regular"), new CardAction(ActionTypes.OpenUrl, "Medium", value: "Medium") }

                };

                return herocard.ToAttachment();
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}