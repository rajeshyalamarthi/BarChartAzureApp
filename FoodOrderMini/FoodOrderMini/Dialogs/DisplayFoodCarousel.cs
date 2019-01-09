
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class DisplayFoodCarousel
    {
        public static Activity MenuCarousel(Activity activity, IDialogContext context)
        {        
            try
            {
                var UserRequest = activity.Text;
                Activity replyToConversation = activity.CreateReply("!!Welcome to the Southern Restaurant,..Order Your Food Buddy, Have a Greatday!!!");

                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                replyToConversation.Attachments = new List<Attachment>();
                Dictionary<string, string> cardContentList = new Dictionary<string, string>();
                cardContentList.Add("Biryani", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQj8J-RY4ikjj3qBpIt_3E9fBhDpjoZs6YeoHuKVUkLTdjgrs2t");
                cardContentList.Add("Pizza", "https://images.wallpaperscraft.com/image/pizza_mushrooms_olives_tomatoes_cheese_95004_300x168.jpg");
                cardContentList.Add("Beverages", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAVqZQ9ZCd0QPdlH2OjSFiNKraCqzX-RoXyXlMkIF2cZaq9lXS");
                foreach (KeyValuePair<string, string> cardContent in cardContentList)
                {
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: cardContent.Value));
                    List<CardAction> cardButtons = new List<CardAction> {
                            new CardAction(ActionTypes.ImBack, "OrderNow",value:cardContent.Key+"Order") };
                    HeroCard plCard = new HeroCard()
                    {
                        Title = cardContent.Key,
                        Subtitle = cardContent.Key + "Taste it And Have Fun.Food Here Never Ever Bores U",
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);

                }
              //  ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replyToConversation.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replyToConversation;

            }
            catch (Exception ex)
            {
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
          
        }
    }
}