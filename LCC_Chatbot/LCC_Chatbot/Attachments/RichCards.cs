using DALRepository;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCC_Chatbot.Attachments
{
    public static class RichCards
    {
        public static IMessageActivity Attachments(string activity,IDialogContext context,string Text,string button1, string button2,string Button1val,string Button2val)
        {
            try
            {
                var replymessage = context.MakeMessage();
                var herocard = new ThumbnailCard
                {
                    Title = "",
                    Subtitle = Text,
                    Text = "",
                    Images = new List<CardImage> { new CardImage("") },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, button1, value: Button1val), new CardAction(ActionTypes.ImBack, button2, value: Button2val) }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
                Logging.ConversationLogg(context.Activity.From.Name, activity, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                return replymessage;

            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }




        }
    }
}