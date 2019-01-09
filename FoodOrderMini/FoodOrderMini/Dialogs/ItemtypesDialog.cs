
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderMini.Dialogs
{
    public class ItemtypesDialog
    {
        public static IMessageActivity ItemTypes(Activity activity, IDialogContext context, string Title, string Subtitle, string Text, string Image, string button1, string button2)
        {
     
            try
            {
                var UserRequest = activity.Text;
                var replymessage = context.MakeMessage();

                var herocard = new HeroCard
                {
                    Title = Title,
                    Subtitle = Subtitle,
                    Text = Text,
                    Images = new List<CardImage> { new CardImage(Image) },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, button1, value: button1), new CardAction(ActionTypes.ImBack, button2, value: button2) }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
               // ConversationLog.ConversationLogg(context.Activity.From.Name, UserRequest, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;

            }
            catch (Exception ex)
            {
                //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                throw ex;
                //return null;
            }
        
           


        }
    }
}