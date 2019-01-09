
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class VegBiryaniDialog
    {
        public static IMessageActivity Vegbiryanicount(Activity activity, IDialogContext context, string Userresponse)
        {
           
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();
                RootDialog.vegBirresponse = Userresponse;
                RootDialog.NonvegBirresponse = null; RootDialog.RegularPizzaResponse = null; RootDialog.MediumPizzaResponse = null; RootDialog.SoftDrinksResponse = null; RootDialog.ThickShakesResponse = null;

                var herocard = new HeroCard
                {
                    Title = "ORDER YOUR VEG BIRYANI[ Rs 160/-]",
                    Text = "Please Enter No of " + Userresponse + " You want To Order?? Please Enter count Like 1,2,3.....And Maximum You can Order 15 Only",
                    Images = new List<CardImage> { new CardImage("http://media.gettyimages.com/photos/indian-pulav-vegetable-rice-veg-biryani-basmati-rice-picture-id495186954?s=170667a&w=1007") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
               // ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
                // await context.PostAsync(replymessage);
            }
            catch (Exception ex)
            {
                //await context.PostAsync(ex.Message);
              //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
      
            

        }

        public static IMessageActivity VegBiryaniOrder(Activity activity, IDialogContext context, int biryanicount)
        {
          
         
            try
            {
                 var  UserRequest = activity.Text;
                  var replymessage = context.MakeMessage();
                RootDialog.VegBiryaniCost = biryanicount * 160;
                RootDialog.VegBiryaniCount = " " + biryanicount + "Veg Biryanii";

                var herocard = new ThumbnailCard
                {
                   
                    Images = new List<CardImage> { new CardImage("https://img.freepik.com/free-vector/smiley-deliveryman-with-chinese-food_23-2147673680.jpg?size=338&ext=jpg") },
                    Title = "You are Willing To Order" + RootDialog.VegBiryaniCount,
                    Text = "If You Want To Checkout Press Checkout Button Below Or Else COntinue With your Ordering",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "CheckOut", value: "CheckOut") }
                };
                RootDialog.vegBirresponse = null;
                replymessage.Attachments.Add(herocard.ToAttachment());
              //  ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;
                // await context.PostAsync(replymessage);
            }
            catch (Exception ex)
            {
                // await context.PostAsync(ex.Message);
                //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
           

        }
    }
}