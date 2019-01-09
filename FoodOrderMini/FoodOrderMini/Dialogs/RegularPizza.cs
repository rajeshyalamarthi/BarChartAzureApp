
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class RegularPizza
    {
        public static IMessageActivity RegularPizzaCount(Activity activity, IDialogContext context, string Userresponse)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.RegularPizzaResponse = Userresponse;
                RootDialog.vegBirresponse = null; RootDialog.NonvegBirresponse = null; RootDialog.MediumPizzaResponse = null; RootDialog.SoftDrinksResponse = null; RootDialog.ThickShakesResponse = null;
                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR Regular Pizza [ Rs 120/-]",
                    Text = "Please Enter No of " + Userresponse + " You want Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("http://www.pngall.com/wp-content/uploads/2016/05/Pizza-PNG-Clipart.png") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
             //   ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
            }
            catch (Exception ex)
            {
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
           
        }


        public static IMessageActivity Regularpizzaorder(Activity activity, IDialogContext context, int pizzacount)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.RegularPizzaCost = pizzacount * 120;
                RootDialog.RegularPizzaCount = " " + pizzacount + "Regular Pizza";

                var herocard = new ThumbnailCard
                {

                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Order" + RootDialog.RegularPizzaCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.RegularPizzaResponse = null;
                replymessage.Attachments.Add(herocard.ToAttachment());
              //  ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
            }
            catch (Exception ex)
            {
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
         

        }
    }
}