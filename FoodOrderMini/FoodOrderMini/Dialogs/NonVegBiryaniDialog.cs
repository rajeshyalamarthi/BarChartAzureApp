
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class NonVegBiryaniDialog
    {
        public static IMessageActivity Nonvegbiryanicount(Activity activity, IDialogContext context, string Userresponse)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.NonvegBirresponse = Userresponse;
                RootDialog.vegBirresponse = null; RootDialog.RegularPizzaResponse = null; RootDialog.MediumPizzaResponse = null; RootDialog.SoftDrinksResponse = null; RootDialog.ThickShakesResponse = null;

                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR ChickenBiryani[ Rs 220/-]",
                    Text = "Please Enter No of " + Userresponse + " You want To Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("https://i0.wp.com/media.hungryforever.com/wp-content/uploads/2017/06/09121657/chicken-fry-biryani-recipes.jpg?ssl=1?w=356&strip=all&quality=80") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
              //  ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
            }
            catch (Exception ex)
            {
              // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
         

           
        }

        public static IMessageActivity NonVegBiryaniOrder(Activity activity, IDialogContext context, int biryanicount)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.ChickenBiryaniCost = biryanicount * 220;
                RootDialog.NonVegBiryaniCount = " " + biryanicount + "ChickenBiryani";

                var herocard = new ThumbnailCard
                {
                   
                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Order " + RootDialog.NonVegBiryaniCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.NonvegBirresponse = null;
                replymessage.Attachments.Add(herocard.ToAttachment());
             //   ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
            }
            catch (Exception ex)
            {
              //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
       

        }
    }
}