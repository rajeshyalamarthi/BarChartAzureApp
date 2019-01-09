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
    public class BiryaniOrderDialog: IDialog<object>
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
                Attachment attachment = BiryaniOrder();
                ReplyMessage.Attachments = new List<Attachment> { attachment };
                await context.PostAsync(ReplyMessage);
                
            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
            }

        }


        private static Attachment BiryaniOrder()
        {

            try
            {
                var herocard = new Microsoft.Bot.Connector.HeroCard

                {
                    Title = "BIRYANI",
                    Subtitle = "Experience The Savage",
                    Text = "Choose The Biryani",
                    Images = new List<CardImage> { new CardImage("http://res.cloudinary.com/prayuta/image/upload/v1500964175/Chicken-Biryani-1_fudjk7.jpg") },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "NonVeg", value: "NonVeg"), new CardAction(ActionTypes.OpenUrl, "Veg", value: "Veg") }

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