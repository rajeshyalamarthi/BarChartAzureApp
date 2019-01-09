using DALRepository;
using LCC_Chatbot.Attachments;
using LCC_Chatbot.LuisResponse;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LCC_Chatbot.Dialogs
{
    [Serializable]
    public class GenerateItTicket : IDialog<object>
    {
        public static string IssueCategory = string.Empty;
        public static string IssueFacing = string.Empty;
        public static string IssueDescriptionNote = string.Empty;
        //    Rootobject rootobject = new Rootobject();

        /// <summary>
        /// In This Method We are going To Display Rich card For The User To Select issue category
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task StartAsync(IDialogContext context)
        {
            try
            {
                var replymessage = context.MakeMessage();
                var herocard = new HeroCard()
                {
                    Text = "Please Choose issue category",
                    Images = new List<CardImage> { new CardImage(MessagesController.chartUrl) }
                   // Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Software", value: "Software Issue"), new CardAction(ActionTypes.ImBack, "Hardware", value: "HardWare Issue") }
                };
                replymessage.AttachmentLayout = AttachmentLayoutTypes.List;
                replymessage.Attachments = new List<Microsoft.Bot.Connector.Attachment>();
                Microsoft.Bot.Connector.Attachment plAttachment = herocard.ToAttachment();
                replymessage.Attachments.Add(plAttachment);
                Logging.ConversationLogg(context.Activity.From.Name, RootDialog.ActivityValue, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                context.PostAsync(replymessage);
                PromptDialog.Text(context, MessageReceivedAsync, "Choose Issue");
                //context.Wait(MessageReceivedAsync);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
        //This methods Performs The Actions Based On The Issue Selected
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                IssueCategory = activity.ToString();

                Rootobject rootobject = new Rootobject();

                rootobject = await LuisData.ReturnLuisData(IssueCategory);

                if (IssueCategory.Equals("Software Issue", StringComparison.InvariantCultureIgnoreCase))
                {
                    var replymessage = context.MakeMessage();
                    var herocard = new ThumbnailCard
                    {
                        Subtitle = "What type of Software are you issue facing with?",
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Email", value: "EmailIssue"), new CardAction(ActionTypes.ImBack, "Antivirus", value: "AntiVirusIssue"), new CardAction(ActionTypes.ImBack, "Others", value: "OtherIssues") }
                    };
                    replymessage.Attachments.Add(herocard.ToAttachment());
                    Logging.ConversationLogg(context.Activity.From.Name, IssueCategory, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                    await context.PostAsync(replymessage);
                }
                else if (IssueCategory.Equals("HardWare Issue", StringComparison.InvariantCultureIgnoreCase))
                {
                    var replymessage = context.MakeMessage();
                    var herocard = new ThumbnailCard
                    {
                        Subtitle = "What type of HardWare are you issue facing with?",
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Laptop", value: "LaptopIssue"), new CardAction(ActionTypes.ImBack, "Printer", value: "PrinterIssue"), new CardAction(ActionTypes.ImBack, "Others", value: "OtherIssues") }
                    };
                    replymessage.Attachments.Add(herocard.ToAttachment());
                    Logging.ConversationLogg(context.Activity.From.Name, IssueCategory, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                    await context.PostAsync(replymessage);
                    //context.Wait(MessageReceivedAsync);
                }
                else if (rootobject.entities[0].type.Equals("QuitFlow", StringComparison.InvariantCultureIgnoreCase))
                {
                    await context.PostAsync("ok");
                    context.Done(result);
                }
                //PromptDialog is used Inorder to Move To AfterSelectingIssue method
                PromptDialog.Text(
             context: context,
              resume: AfterSelectingIssue,
              prompt: "Please Choose Your Issue Mentioned Above"
              );
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
        // method to get the selected issues and check whether the entity type is Subcategory or  not 
        private async Task AfterSelectingIssue(IDialogContext context, IAwaitable<object> result)
        {

            try
            {
                var activity = await result;
                IssueFacing = activity.ToString();
                Rootobject rootobject = new Rootobject();

                rootobject = await LuisData.ReturnLuisData(IssueFacing);

                if (rootobject.entities != null && rootobject.entities.Length != 0)
                {

                    if (rootobject.entities[0].type.Equals("QuitFlow", StringComparison.InvariantCultureIgnoreCase))
                    {
                        await context.PostAsync("ok");
                        context.Done(result);
                    }
                    else if (rootobject.entities != null && rootobject.entities[0].type == "Subcategory")
                    {
                        //context.Wait(AfterSelectingIssue);
                        PromptDialog.Text(
                         context: context,
                         resume: IssueDescription,
                          prompt: "Please describe your issue in few words"
                    );
                    }
                }
                else
                {
                    PromptDialog.Text(context, AfterSelectingIssue, "Please Select in The Issues Provided Mentioned above the list");
                   // await context.PostAsync("Please Select in The Issues Provided Mentioned above the list");
                   //context.Wait(AfterSelectingIssue);
                }

            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
        //To Display An Rich card Wheather To raise The Ticket Or Not to
        private async Task IssueDescription(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                IssueDescriptionNote = activity.ToString();
                Rootobject rootobject = new Rootobject();

                rootobject = await LuisData.ReturnLuisData(IssueDescriptionNote);
                if (rootobject.entities != null && rootobject.entities.Length != 0)
                {
                    if (rootobject.entities[0].type.Equals("QuitFlow", StringComparison.InvariantCultureIgnoreCase))
                    {
                        await context.PostAsync("ok");
                        context.Done(result);
                    }
                }
                else
                {
                    var replymessage = context.MakeMessage();
                    var herocard = new ThumbnailCard
                    {
                        Text = "Shall we Raise The Ticket Yes Or No",
                        Subtitle = "Ticket To Be Raised For Issue Category :" + IssueCategory + " , Issue With :" + IssueFacing + ",Issue Description :" + IssueDescriptionNote + "\n\n",
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Yes", value: "Yes"), new CardAction(ActionTypes.ImBack, "No", value: "No") }
                    };
                    replymessage.Attachments.Add(herocard.ToAttachment());
                    await context.PostAsync(replymessage);
                    PromptDialog.Text(context, TicketGenerationResponse, "Click Yes Or No");
                }

            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
        //To display the Final Response 
        private async Task TicketGenerationResponse(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                string activityresult = activity.ToString().ToLower();
                Rootobject rootobject = new Rootobject();
                rootobject = await LuisData.ReturnLuisData(activity.ToString());
                if (rootobject.entities != null && rootobject.entities.Length != 0)
                {
                    if (rootobject.entities[0].type.Equals("QuitFlow", StringComparison.InvariantCultureIgnoreCase))
                    {
                        await context.PostAsync("ok");
                        context.Done(result);
                    }
                }
                else if (activity.ToString().Equals("yes",StringComparison.InvariantCultureIgnoreCase))
                {
                    Logging.ItTicketLogging(context.Activity.From.Name, IssueCategory, IssueFacing, IssueDescriptionNote, DateTime.Now, context.Activity.ChannelId);
                    IssueCategory = string.Empty;
                    IssueFacing = string.Empty;
                    IssueDescriptionNote = string.Empty;
                    await context.PostAsync("Yeah Your Token Will Be Raised,Thank You.");
                    context.Done(result);
                }
                else if (activity.ToString().Equals("no",StringComparison.InvariantCultureIgnoreCase))
                {
                    await context.PostAsync("Ok");
                    context.Done(result);
                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
    }
}