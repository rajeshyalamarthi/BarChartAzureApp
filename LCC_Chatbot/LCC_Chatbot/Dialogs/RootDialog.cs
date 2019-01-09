using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using DALRepository;
using LCC_Chatbot.Attachments;
using LCC_Chatbot.LuisResponse;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LCC_Chatbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public static string Intent = string.Empty;
        public static string ActivityValue;
        public static string TicketsGenerated = string.Empty;
        public static List<TicketsList> Tickets = new List<TicketsList>();
        private readonly string[] _cards =
                 {
                     @"G:\BOT\V3Practice\BotTrainingRepo\LCC_Chatbot\LCC_Chatbot\AdaptiveJosn\Adaptive.json"
                 };

        public Task StartAsync(IDialogContext context)
        {
            try
            {

                context.Wait(MessageReceivedAsync);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
        /// <summary>
        /// This Method Is for The Intent And Entity which it gets from the luis it will maps according to that
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                Rootobject rootobject = new Rootobject();
                //LuisData Luisdata = new LuisData();
                string BotResponseMessage;

                var Entity = string.Empty;
                var activity = await result as Activity;

                var Userresponse = activity.Text;
                rootobject = await LuisData.ReturnLuisData(Userresponse);

                if (Userresponse.Equals("ChartsTesting", StringComparison.InvariantCultureIgnoreCase))
                {

                    string reply = string.Empty;

                    HttpClient hclient = new HttpClient();
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:7071/api/Function1");
                    httpWebRequest.Method = "Post";
                    httpWebRequest.ContentType = "application/json";
                    Stream stream = httpWebRequest.GetRequestStream();
                    string json = JsonConvert.SerializeObject(activity);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    stream.Write(buffer, 0, buffer.Length);
                    //HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                    string url;

                    HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                    WebHeaderCollection webHeader = res.Headers;
                    var encoding = ASCIIEncoding.ASCII;
                    using (var reader = new System.IO.StreamReader(res.GetResponseStream(), encoding))
                    {
                        url = reader.ReadToEnd();
                    }
                    url = url.Trim('"');
                    url = url.Replace("\\n", "\n");

                   






                    IMessageActivity message = context.MakeMessage();
                    message.Attachments.Add(new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JObject.Parse($@"
                    
             {{
	""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
	""type"": ""AdaptiveCard"",
	""version"": ""1.0"",
	""body"": [
		{{
			""type"": ""Container"",
			""items"": [
				{{
					""type"": ""ColumnSet"",
					""columns"": [
						{{
							""type"": ""Column"",
							""width"": ""auto"",
							""items"": [
								{{  
                            ""type"": ""Image"",
                            ""url"": ""{url}"",
                            ""altText"":""{Userresponse}"",
                            ""size"": ""Stretch"",
								}}
							]
						}},
					]
				}}
			]
		}},
	],
	""actions"": [
		{{
			""type"": ""Action.Showcard"",
			""title"": ""Set due date"",
			""card"": {{
				""type"": ""AdaptiveCard"",
				""body"": [
					{{
						    ""type"": ""Image"",
                            ""url"": ""{MessagesController.chartUrl}"",
                            ""altText"":""{Userresponse}"",
                            ""size"": ""Stretch"",

					}},				
				],
			
			}}
		}},
	
	]
}}")
                    });
                    await context.PostAsync(message);
                    context.Wait(MessageReceivedAsync);





                    //-----------------------------------------------------------------------
//                       message.Attachments.Add(new Attachment()
//                    {
//                        ContentType = "application/vnd.microsoft.card.adaptive",
//                        Content = JObject.Parse($@"
//                 {{
//                ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
//              ""type"": ""AdaptiveCard"",
//              ""version"": ""1.0"",
//              ""body"": [
//                   {{
//                            ""type"": ""TextBlock"",
//                            ""text"": ""TOTAL GROWTH for in 2017 by VIEW""
//                        }},
//                        {{
//                            ""type"": ""Image"",
//                            ""url"": ""{MessagesController.chartUrl}"",
//                            ""altText"":""Not Loaded"",
//                            ""size"": ""Stretch"",
//                            ""horizontalAlignment"": ""right""
//                                }},
//            ],

//}}")
//                    });





                    //--------------------------------------------------------------------
                    //Random r = new Random();
                    //var CardAttachment = LuisData.CreateAdaptiveCardAttachment(this._cards[r.Next(this._cards.Length)]);
                    //var reply = context.MakeMessage();
                    //reply.Attachments = new List<Attachment>() { CardAttachment };
                    //await context.PostAsync(reply,CancellationToken.None);
                    //-------------------------------------------------------------------------------


                    //                    IMessageActivity messageActivity = context.MakeMessage();
                    //                    messageActivity.Attachments.Add(new Attachment()
                    //                    {
                    //                        ContentType = "application /vnd.microsoft.card.adaptive",
                    //                        Content = JObject.Parse(@"
                    //                      {

                    //                ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                    //              ""type"": ""AdaptiveCard"",
                    //              ""version"": ""1.0"",
                    //              ""body"": [
                    //                     {
                    //                      ""type"": ""Column"",
                    //                      ""width"": ""auto"",
                    //                        ""items"": [
                    //                          {
                    //                            ""type"": ""TextBlock"",
                    //                            ""text"": ""This Is The Bar Graph""
                    //                        },
                    //                        {
                    //                            ""type"": ""Image"",
                    //                            ""url"":""data: image/png; base64,iVBORw0KGgoAAAANSUhEUgAAAlgAAAD6CAYAAAB9LTkQAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAABWWSURBVHhe7d3bceNYlgVQlQ9pQJmi + aNBNGNMoC8yoj7qVyaMBxyCD5FUQrgbWRDyJs5aESe6RFGgtEmdu4Oq7n45AgCwKAULAGBhChYAwMIULACAhSlYAAALU7AAABamYAEALEzBAgBYmIIFALAwBQsAYGEKFgDAwhQsAICFKVgAAAtTsAAAFqZgAQAsTMECAFiYggUAsDAF6zd5fX09vry8 / DQ / fvw4 / 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"",
                    //                            ""spacing"": ""none""
                    //                        }
                    //      ]
                    //    }
                    //]
                    //                }")
                    //                    });

                    //                    await context.PostAsync(messageActivity);
                    //                    context.Wait(MessageReceivedAsync);


                    //------------------------------------------------------------------------------------

                    //AdaptiveCard adaptivecard = new AdaptiveCard()
                    //{
                    //    Body = new List<CardElement>()
                    //    {
                    //        new Container()
                    //{
                    //   // speak = " < s > hello!</ s >< s > are you looking for a flight or a hotel ?</ s > ",
                    //    Items =  new List<CardElement>()
                    //    {
                    //        new ColumnSet()
                    //        {
                    //            Columns = new List<Column>()
                    //            {
                    //                new Column()
                    //                {
                    //                    Size = ColumnSize.Auto,
                    //                    Items = new List<CardElement>()
                    //                    {
                    //                        new Image()
                    //                        {
                    //                            Url = MessagesController.chartUrl,
                    //                            Size = ImageSize.Stretch,
                    //                            style = imagestyle.normal
                    //                        }
                    //                    }
                    //                },
                    //             }
                    //         }
                    //    }
                    //}
                    //    }
                    //};

                    //attachment attachment = new attachment()
                    //{
                    //    contenttype = adaptivecard.contenttype,
                    //    content = adaptivecard
                    //};

                    //var reply = context.makemessage();
                    //reply.attachments.add(attachment);
                    //await context.postasync(reply, cancellationtoken.none);





                    //--------------------------------------------------------------------------------------------------------


                    //string position = "right";
                    //var replymessage = context.MakeMessage();

                    //HeroCard plCard = new HeroCard()
                    //{
                    //    Images = new List<CardImage> { new CardImage("<img style="+"width:100%; max-height:100%;"+"src="+MessagesController.chartUrl+" />") }
                    //};
                    //replymessage.Text = string.Empty;
                    //replymessage.AttachmentLayout = AttachmentLayoutTypes.List;
                    //replymessage.Attachments = new List<Microsoft.Bot.Connector.Attachment>();
                    //Microsoft.Bot.Connector.Attachment plAttachment = plCard.ToAttachment();
                    //replymessage.Attachments.Add(plAttachment);
                    //await context.PostAsync(replymessage);
                    //return replyActivity;
                }


                //var card = new AdaptiveCard();
                //card.Body.Add(new Image()
                //{
                //    Url = MessagesController.chartUrl,
                //    Type = "Image",
                //    AltText = "sorry NotLoaded"

                //});
                //Attachment attachment = new Attachment()
                //{
                //    ContentType = AdaptiveCard.ContentType,
                //    Content = card
                //};
                //replymessage.Attachments.Add(attachment);
                //    await context.PostAsync(replymessage);


                //----------------------------------------------------------------------------------
                //var replymessage = context.MakeMessage();
                //var herocard = new HeroCard
                //{
                //    Images=new List<CardImage> { new CardImage("<img src=" + MessagesController.chartUrl + " alt=" + "Image Not Loaded" + " width=" + "500" + " height=" + "600" + ">") }
                //    //Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Canada", value: "i want to book a meeting room In Canada"), new CardAction(ActionTypes.ImBack, "UK", value: "i want to book a meeting room In UK") }
                //};
                //replymessage.Attachments.Add(herocard.ToAttachment());
                //Logging.ConversationLogg(context.Activity.From.Name, activity.Text, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                //await context.PostAsync(replymessage);

                //-----------------------------------------------------

                if (rootobject.entities != null && rootobject.entities.Length != 0)
                {
                    if (rootobject.topScoringIntent.intent.Equals("RealEstateMeetingRoomReservationBook", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities[0].entity.Equals("Canada", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        Entity = rootobject.entities[0].entity;
                        BotResponseMessage = PostResponse.GetResponseFromBotForDynamicQuestions(Intent, Entity);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                        //await context.PostAsync(MessagesController.Conversationid);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("RealEstateMeetingRoomReservationBook", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities[0].entity.Equals("UK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        Entity = rootobject.entities[0].entity;
                        BotResponseMessage = PostResponse.GetResponseFromBotForDynamicQuestions(Intent, Entity);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("ItTicketRaising", StringComparison.InvariantCultureIgnoreCase) && (rootobject.entities[0].type.Equals("Category", StringComparison.InvariantCultureIgnoreCase) || rootobject.entities[0].type.Equals("Subcategory", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        ActivityValue = Userresponse;
                        context.Call(new GenerateItTicket(), AfterChoosencategory);
                    }
                    else
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        BotResponseMessage = PostResponse.GetResponseFromBotForStaticQuestions(Intent);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }
                }
                else
                {
                    if (rootobject.topScoringIntent.intent.Equals("RealEstateMeetingRoomReservationBook", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        var replymessage = context.MakeMessage();
                        var herocard = new ThumbnailCard
                        {
                            Text = "Please Book the rooms in Below Locations Only",
                            Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Canada", value: "i want to book a meeting room In Canada"), new CardAction(ActionTypes.ImBack, "UK", value: "i want to book a meeting room In UK") }
                        };
                        replymessage.Attachments.Add(herocard.ToAttachment());
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(replymessage);
                    }

                    else if (rootobject.topScoringIntent.intent.Equals("Register", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        context.Call(new QuestionsDialog(), AfterChoosencategory);

                        //  questions = PostResponse.Questions(rootobject.topScoringIntent.intent);








                    }
                    else if (rootobject.topScoringIntent.intent.Equals("TicketsCount", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {

                        string SpecificUser = context.Activity.From.Name;
                        Tickets = PostResponse.TicketsLists(SpecificUser);
                        if (Tickets.Count != 0)
                        {
                            for (int i = 1; i <= Tickets.Count; i++)
                            {
                                TicketsGenerated += "<b>Ticket " + i + "</b> :- " + "  category is - " + Tickets[i - 1].IssueCategory + " ,Issue facing With - " + Tickets[i - 1].IssuefacingWith + " ,Description by you - " + Tickets[i - 1].IssueDescription + " ,  Time " + Tickets[i - 1].GeneratingTime + "<br/>";
                            }
                            var ticketsdisplaycard = context.MakeMessage();
                            var Herocard = new HeroCard
                            {
                                Text = TicketsGenerated
                            };
                            ticketsdisplaycard.Attachments.Add(Herocard.ToAttachment());
                            Tickets.Clear();
                            TicketsGenerated = string.Empty;
                            await context.PostAsync(ticketsdisplaycard);
                        }
                        else
                        {
                            await context.PostAsync(" Sorry " + SpecificUser + ", You did not Raised Any Tickets Yet");
                        }


                    }
                    else if (rootobject.topScoringIntent.intent.Equals("RealEstateReceptionRegister", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        Entity = "";
                        BotResponseMessage = PostResponse.GetResponseFromBotForDynamicQuestions(Intent, Entity);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("RealEstateShuttleBusSchedule", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        Entity = "";
                        BotResponseMessage = PostResponse.GetResponseFromBotForDynamicQuestions(Intent, Entity);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("TravelBookITBook", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        Entity = "";
                        BotResponseMessage = PostResponse.GetResponseFromBotForDynamicQuestions(Intent, Entity);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("ItTicketRaising", StringComparison.InvariantCultureIgnoreCase) && rootobject.entities.Length == 0)
                    {
                        ActivityValue = Userresponse;
                        context.Call(new GenerateItTicket(), AfterChoosencategory);
                    }
                    else if (rootobject.topScoringIntent.intent.Equals("VisitorBadgeRequest", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ActivityValue = Userresponse;
                        context.Call(new VisitorBadgeDialog(), AfterChoosencategory);
                    }
                    else
                    {
                        Intent = rootobject.topScoringIntent.intent;
                        BotResponseMessage = PostResponse.GetResponseFromBotForStaticQuestions(Intent);
                        //BotResponseMessage = BotResponseMessage.Replace("$", System.Environment.NewLine);
                        BotResponseMessage = BotResponseMessage.Replace("#", context.Activity.From.Name);
                        Logging.ConversationLogg(context.Activity.From.Name, activity.Text, BotResponseMessage, DateTime.Now, context.Activity.ChannelId);
                        await context.PostAsync(BotResponseMessage);
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                await context.PostAsync(ex.Message);
            }
        }

        /// <summary>
        /// This Method Is The Final Method That Is Called After The End Process Of The Guidence which were used
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task AfterChoosencategory(IDialogContext context, IAwaitable<object> result)
        {
            try
            {

                var message = "Any Other Help??";
                await context.PostAsync(message);
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
    }
}