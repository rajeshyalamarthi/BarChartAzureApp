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
    public class Beverages : IDialog<object>
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
                Attachment attachment = BeveragesOrder();
                ReplyMessage.Attachments = new List<Attachment> { attachment };
                await context.PostAsync(ReplyMessage);

            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
            }

        }


        private static Attachment BeveragesOrder()
        {

            try
            {
                var herocard = new Microsoft.Bot.Connector.HeroCard

                {
                    Title = "Beverages",
                    Subtitle = "Experience The Savage",
                    Text = "Feel The End",
                    Images = new List<CardImage> { new CardImage("https://st.depositphotos.com/1044737/4943/i/950/depositphotos_49431317-stock-photo-colorful-soda-drinks-with-cola.jpg") },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "SoftDrinks", value: "SoftDrinks"), new CardAction(ActionTypes.OpenUrl, "ThickShakes", value: "ThickShakes") }

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