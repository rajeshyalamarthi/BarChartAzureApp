
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FoodOrderMini.Dialogs
{
    public class CheckoutDialog
    {
        public static IMessageActivity Checkout(Activity activity, IDialogContext context)
        { 
            try
            {
                var userrequest = activity.Text;
                var replymessage = context.MakeMessage();

                RootDialog.Finalcost = RootDialog.ChickenBiryaniCost + RootDialog.VegBiryaniCost + RootDialog.RegularPizzaCost + RootDialog.MediumPizzaCost + RootDialog.SoftDrinksCost + RootDialog.ThickShakesCost;
                RootDialog.FinalOrderCount = "Your Final Order " + RootDialog.NonVegBiryaniCount + " " + RootDialog.VegBiryaniCount + " " + RootDialog.RegularPizzaCount + " " + RootDialog.MediumPizzaCount + " " + RootDialog.SoftDrinksCount + " " + RootDialog.ThickShakesCount + " " +" Of Cost Rs "+ RootDialog.Finalcost.ToString()+" "+"Please Confirm It *And Pay The Money When Your Food Is Deliverd";
                var herocard = new ThumbnailCard
                {

                    Text = RootDialog.FinalOrderCount,
                    Title = "Your Total Bill Is Rs" + " " + RootDialog.Finalcost.ToString() + "/-",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Confirm", value: "Confirm"), new CardAction(ActionTypes.ImBack, "Cancel", value: "DisplayFood") }

                };
                replymessage.Attachments.Add(herocard.ToAttachment());
                var UserName = context.Activity.From.Name;
               // FinalOrderLog.FinalOrderInfo(UserName,RootDialog.FinalOrderCount, DateTime.Now, context.Activity.ChannelId);
               // ConversationLog.ConversationLogg(UserName, userrequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                RootDialog.NonVegBiryaniCount = "";
                RootDialog.VegBiryaniCount = "";
                RootDialog.MediumPizzaCount = "";
                RootDialog.RegularPizzaCount = "";
                RootDialog.SoftDrinksCount = "";
                RootDialog.ThickShakesCount = "";
                RootDialog.FinalOrderCount = "";
                RootDialog.ThickShakesCost = 0;
                RootDialog.SoftDrinksCost = 0;
                RootDialog.MediumPizzaCost = 0;
                RootDialog.RegularPizzaCost = 0;
                RootDialog.VegBiryaniCost = 0;
                RootDialog.ChickenBiryaniCost = 0;
                RootDialog.Finalcost = 0;

                return replymessage;
            }
            catch (Exception ex)
            {
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
              // return null;
            }
           
        }
    }
}