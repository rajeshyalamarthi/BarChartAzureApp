using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DALRepository;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LCC_Chatbot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public static string Conversationid = string.Empty;
        public static string chartUrl= "data:image/png;base64,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";
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            try
            {
                if (activity.GetActivityType() == ActivityTypes.Message)
                {
                    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity IsTyingReply = activity.CreateReply();
                
                    IsTyingReply.Type = ActivityTypes.Typing;
                    // var conversationid = await connector.Conversations.CreateDirectConversationAsync(activity.Recipient,activity.From);
                    //Conversationid = conversationid.Id.ToString();
                    await connector.Conversations.ReplyToActivityAsync(IsTyingReply);
                    await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
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
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
        
        private Activity HandleSystemMessage(Activity message)
        {
            try
            {
                string messageType = message.GetActivityType();
                if (messageType == ActivityTypes.DeleteUserData)
                {
                    // Implement user deletion here
                    // If we handle user deletion, return a real message
                }
                else if (messageType == ActivityTypes.ConversationUpdate)
                {
                    // chartUrl = "data:image/png;base64,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";
                    // Handle conversation state changes, like members being added and removed
                    // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                    // Not available in all channels   IConversationUpdateActivity updateActivity = message;
                    IConversationUpdateActivity updateActivity = message;

                    var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                  
                    if (updateActivity.MembersAdded != null && updateActivity.MembersAdded.Any())
                    {

                        foreach (var newuser in updateActivity.MembersAdded)
                        {
                            if (newuser.Id == message.Recipient.Id)
                            {
                                var replymessage = message.CreateReply();
                                replymessage.Attachments.Add(new Attachment()
                                {
                                    // ContentUrl = "https://www.clipartmax.com/png/middle/118-1180043_cartoon-angry-robot-cartoon-robot-cute-robot-angry-friendly-bot.png",
                                    ContentUrl=chartUrl,
                                    ContentType = "image/png",
                                    Name = "Bender_Rodriguez.png"
                                });
                                replymessage.Text = "Hello!"+updateActivity.From.Name+" ,  How Can I Help You??";
                                client.Conversations.ReplyToActivityAsync(replymessage);
                            }

                        }

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

                else if (messageType == ActivityTypes.MessageReaction)
                {
                    var Client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                    var Reply = message.CreateReply();

                    Reply.Text = "Thank You for poking";
                    Client.Conversations.ReplyToActivityAsync(Reply);
                }



                return null;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
    }
}