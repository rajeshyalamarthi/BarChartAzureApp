
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class SoftDrinksDialog
    {
        public static IMessageActivity SoftDrinksCount(Activity activity, IDialogContext context, string Userresponse)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.SoftDrinksResponse = Userresponse;
              //  RootDialog.vegBirresponse = null; RootDialog.RegularPizzaResponse = null; RootDialog.MediumPizzaResponse = null; RootDialog.NonvegBirresponse = null; RootDialog.ThickShakesResponse = null;

                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR SoftDrinks[ Rs 30/-]",
                    Text = "Please Enter No of " + Userresponse + " You want Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("https://ak5.picdn.net/shutterstock/videos/8057335/thumb/1.jpg") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
                //await context.PostAsync(replymessage);
              //  ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
            }
            catch (Exception ex)
            {
                //await context.PostAsync(ex.Message);
              // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
           
        }


        public static IMessageActivity SoftDrinksOrder(Activity activity, IDialogContext context, int drinkscount)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.SoftDrinksCost = drinkscount * 30;
                RootDialog.SoftDrinksCount = " " + drinkscount + "SoftDrinks";
              
                var herocard = new ThumbnailCard
                {
               
                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Order" + RootDialog.SoftDrinksCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.SoftDrinksResponse = null;
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
               // return null;
            }
          
        }
    }
}