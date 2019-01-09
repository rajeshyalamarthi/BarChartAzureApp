
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class ThickShakesDialog
    {
        public static IMessageActivity ThickShakesCount(Activity activity, IDialogContext context, string Userresponse)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.ThickShakesResponse = Userresponse;
                RootDialog.vegBirresponse = null; RootDialog.RegularPizzaResponse = null; RootDialog.MediumPizzaResponse = null; RootDialog.SoftDrinksResponse = null; RootDialog.NonvegBirresponse = null;

                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR ThickShakes[Rs 150/-]",
                    Text = "Please Enter No of " + Userresponse + " You want To Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("https://res.cloudinary.com/swiggy/image/upload/f_auto,q_auto,fl_lossy/rumcmcn1ytgdbycgciao") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
               // ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
                // await context.PostAsync(replymessage);
            }
            catch (Exception ex)
            {
                //await context.PostAsync(ex.Message);
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
           
        }

        public static IMessageActivity ThickShakesOrder(Activity activity, IDialogContext context, int drinkscount)
        {
           
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.ThickShakesCost = drinkscount * 150;
                RootDialog.ThickShakesCount = " " + drinkscount + "ThickShakes";

                var herocard = new ThumbnailCard
                {
                   
                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Order" + RootDialog.ThickShakesCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.ThickShakesResponse = null;
                replymessage.Attachments.Add(herocard.ToAttachment());
               // ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
                //await context.PostAsync(replymessage);
            }
            catch (Exception ex)
            {
                //await context.PostAsync(ex.Message);
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
          
        }
    }
}