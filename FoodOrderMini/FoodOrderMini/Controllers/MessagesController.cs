using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace FoodOrderMini
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            try
            {
                if (activity.GetActivityType() == ActivityTypes.Message)
                {
                   await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
                    //if (activity.Text == "DisplayFood")
                    //{
                    //    await Conversation.SendAsync(activity, () => new Dialogs.DisplayFoodItemsDialog());
                    //}
                    //else if (activity.Text == "BiryaniOrder")
                    //{
                    //    await Conversation.SendAsync(activity, () => new Dialogs.BiryaniOrderDialog());
                    //}
                    //else if (activity.Text == "PizzaOrder")
                    //{
                    //   // await Conversation.SendAsync(activity, () => new Dialogs.DisplayFoodItemsDialog());
                    //}
                    //else if (activity.Text == "BeveragesOrder")
                    //{
                    //    //await Conversation.SendAsync(activity, () => new Dialogs.DisplayFoodItemsDialog());
                    //}
                }
                else
                {
                    HandleSystemMessage(activity);
                }
                 var response = Request.CreateResponse(HttpStatusCode.OK);
                 return response;
            }
            catch (Exception ex)
            {
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }

        private Activity HandleSystemMessage(Activity message)
        {
            try {
                string messageType = message.GetActivityType();
                if (messageType == ActivityTypes.DeleteUserData)
                {
                    // Implement user deletion here
                    // If we handle user deletion, return a real message
                }
                else if (messageType == ActivityTypes.ConversationUpdate)
                {
                    try
                    {
                        // Handle conversation state changes, like members being added and removed
                        // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                        // Not available in all channels
                        IConversationUpdateActivity updateActivity = message;
                        var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                        if (updateActivity.MembersAdded != null && updateActivity.MembersAdded.Any())
                        {

                            foreach (var newuser in updateActivity.MembersAdded)
                            {
                                if (newuser.Id == message.Recipient.Id)
                                {
                                    var replymessage = message.CreateReply();
                                    var Card = new HeroCard
                                    {

                                        Title = "Southern Eats",
                                        Subtitle = "Good Food Is Good Mood",
                                        Tap = new CardAction(ActionTypes.OpenUrl, "Visit", value: "https://www.zomato.com/hyderabad/southern-village-restaurant-madhapur"),
                                        Text = "Food Available on workdays are form 12:00 PM to 12:30 AM. On weekends from 8:00 AM to 1:00 AM.",
                                        Images = new List<CardImage> { new CardImage("https://i.imgur.com/gCiLw2d.jpg") },
                                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Dig Into The Food", value: "DisplayFood") }
                                    };

                                    replymessage.Attachments.Add(Card.ToAttachment());


                                    client.Conversations.ReplyToActivityAsync(replymessage);
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                      //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                    }
                }
                else if (messageType == ActivityTypes.ContactRelationUpdate)
                {
                    // Handle add/remove from contact lists
                    // Activity.From + Activity.Action represent what happened
                }
                else if (messageType == ActivityTypes.Typing)
                {
                    // Handle knowing that the user is typing
                }
                else if (messageType == ActivityTypes.Ping)
                {
                }

            }

            catch (Exception ex)
            {
              //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
            }

            return null;
        }
    }
}