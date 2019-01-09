
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class MediumPizzaDialog
    {
        public static IMessageActivity MediumPizzaCount(Activity activity, IDialogContext context, string Userresponse)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.MediumPizzaResponse = Userresponse;
                RootDialog.vegBirresponse = null; RootDialog.RegularPizzaResponse = null; RootDialog.NonvegBirresponse = null; RootDialog.SoftDrinksResponse = null; RootDialog.ThickShakesResponse = null;

                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR MEDIUM PIZZA [ Rs 200/-]",
                    Text = "Please Enter No of " + Userresponse + " You want To Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("http://pngimg.com/uploads/pizza/pizza_PNG44089.png") }
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


        public static IMessageActivity MediumPizzaOrder(Activity activity, IDialogContext context, int pizzacount)
        {
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.MediumPizzaCost = pizzacount * 200;
                RootDialog.MediumPizzaCount = " " + pizzacount + "Medium Pizza";

                var herocard = new ThumbnailCard
                {
               
                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Ordrer" + RootDialog.MediumPizzaCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.MediumPizzaResponse = null;
                replymessage.Attachments.Add(herocard.ToAttachment());
               // ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
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