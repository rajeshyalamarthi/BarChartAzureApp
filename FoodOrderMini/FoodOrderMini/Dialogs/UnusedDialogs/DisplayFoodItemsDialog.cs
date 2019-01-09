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
    public class DisplayFoodItemsDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            
            return Task.CompletedTask;
          

        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var replymessage = activity.CreateReply();
            try
            {

                await context.PostAsync(DisplayFoodItems(context, activity));
                
            }
            catch (Exception ex)
            {
                replymessage.Text = ex.Message;
                await context.PostAsync(replymessage);
            }

        }
        public Activity DisplayFoodItems(IDialogContext context, Activity activity)
        {
            try
            {
                Activity replyToConversation = activity.CreateReply("Welcome to the Southern Restaurant");
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                replyToConversation.Attachments = new List<Attachment>();

                Dictionary<string, string> cardContentList = new Dictionary<string, string>();

                cardContentList.Add("Biryani", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQj8J-RY4ikjj3qBpIt_3E9fBhDpjoZs6YeoHuKVUkLTdjgrs2t");
                cardContentList.Add("Pizza", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-DBSNVXvH34L8viGPcNEZoDcnA1Pe_O9IfaFHoakKoT0388vwPA");
                cardContentList.Add("Beverages", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAVqZQ9ZCd0QPdlH2OjSFiNKraCqzX-RoXyXlMkIF2cZaq9lXS");


                foreach (KeyValuePair<string, string> cardContent in cardContentList)
                {
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: cardContent.Value));

                    List<CardAction> cardButtons = new List<CardAction> {
                        new CardAction(ActionTypes.ImBack, "OrderNow",value:cardContent.Key+"Order") };
                    //CardAction plButton = new CardAction()
                    //{
                    //    Value = cardContent.Key + "Order",
                    //    Type = "ImBack",
                    //    Title = "Order"
                    //};

                    //cardButtons.Add(plButton);


                    Microsoft.Bot.Connector.HeroCard plCard = new Microsoft.Bot.Connector.HeroCard()
                    {
                        Title = cardContent.Key,
                        Images = cardImages,
                        Buttons = cardButtons
                    };

                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                }

                return replyToConversation;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}