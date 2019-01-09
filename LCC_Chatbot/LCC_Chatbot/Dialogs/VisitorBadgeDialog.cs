using DALRepository;
using LCC_Chatbot.Attachments;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LCC_Chatbot.Dialogs
{
    [Serializable]
    public class VisitorBadgeDialog : IDialog<object>
    {// this method displays the user data
        public string UserEnteredName = string.Empty;
        public string UserEnteredEmail = string.Empty;
        public Task StartAsync(IDialogContext context)
        {
            try
            {
                var replymessage = context.MakeMessage();
                var herocard = new ThumbnailCard
                {
                    Text = "Let Me Guide You Through the Process Confirm Your Name &email",
                    Title = "Name: " + context.Activity.From.Name,
                    Subtitle = "Mail :" + context.Activity.From.Name + "@gmail.com",
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Confirm", value: "Confirm"), new CardAction(ActionTypes.ImBack, "Reject", value: "Reject") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
                Logging.ConversationLogg(context.Activity.From.Name,RootDialog.ActivityValue, replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                context.PostAsync(replymessage);
                PromptDialog.Text(context, MessageReceivedAsync, "check your detials");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                if (activity.ToString().Equals("Confirm", StringComparison.InvariantCultureIgnoreCase))
                {
                    //context.Wait(MessageReceivedAsync);
                    PromptDialog.Text(context, Department, "Great! Just a few more steps. Please state your department");

                }
                else if (activity.ToString().Equals("Reject", StringComparison.InvariantCultureIgnoreCase))
                {

                    PromptDialog.Text(context, ValidEmail, "Please Enter Your Name");
                    //context.Wait(MessageReceivedAsync);
                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }
        private async Task ValidEmail(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                UserEnteredName = activity.ToString();
                //await context.PostAsync("");
                PromptDialog.Text(context, Reverify, "Enter Your Email");
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }

        private async Task Reverify(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var activity = await result;
                UserEnteredEmail = activity.ToString();
                var replymessage = context.MakeMessage();
                var herocard = new ThumbnailCard
                {
                    Text = "Let Me Guide You Through the Process Confirm Your Name &email",
                    Title = "Name: " + UserEnteredName,
                    Subtitle = "Mail :" + UserEnteredEmail,
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Confirm", value: "Confirm"), new CardAction(ActionTypes.ImBack, "Reject", value: "Reject") }
                };
                replymessage.Attachments.Add(herocard.ToAttachment());
                //Logging.ConversationLogg(context.Activity.From.Name, , replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                await context.PostAsync(replymessage);
                UserEnteredName = string.Empty;
                UserEnteredEmail = string.Empty;
                PromptDialog.Text(context, MessageReceivedAsync, "Reevaluate your details");
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
            }
        }

        private async Task Vailddetials(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            PromptDialog.Text(context, Department, "Great! Just a few more steps. Please state your department");
        }

        private async Task Department(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, ContactPerson, "Please enter the name of extension contact person");

        }
        private async Task ContactPerson(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, StartDate, "Please enter the  start date from when the badge is needed");
        }
        private async Task StartDate(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, EndDate, " How long do you need the badge for? Please enter on the end date");
        }
        private async Task EndDate(IDialogContext context, IAwaitable<object> result)
        {
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Text = "Thank you. The time period is noted. Would you like to raise request for parking ticket(s) as well?",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Yes", value: "Yes"), new CardAction(ActionTypes.ImBack, "No", value: "No") }
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            //Logging.ConversationLogg(context.Activity.From.Name, , replymessage.ToString(), DateTime.Now, context.Activity.ChannelId);
            await context.PostAsync(replymessage);
            PromptDialog.Text(context, ParkingTicket, "Click On Yes Or No");
        }
        private async Task ParkingTicket(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            string activityresponse = activity.ToString().ToLower();

            if (activityresponse.Equals("yes"))
            {
                PromptDialog.Text(context, ParkingTicketcount, "Ok. How many parking tickets would you like to have? Choose Number Like 1,2,3...");
            }
            else if (activityresponse.Equals("no"))
            {
                PromptDialog.Text(context, ParkingTicketcount, "If No press '0'");
            }
        }

        private async Task ParkingTicketcount(IDialogContext context, IAwaitable<object> result)
        {
            var parkingcount = await result;
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {   Subtitle= "Thank you. The number is noted["+parkingcount.ToString() +"]. Please note that the number of parking spaces are limited. It is possible that your parking request may not be granted. Apologies for the inconvenience.",
                Text = "Please Select Your Prefered Building For The Visit",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Building1", value: "Building1"), new CardAction(ActionTypes.ImBack, "Building2", value: "Building2"), new CardAction(ActionTypes.ImBack, "Building3", value: "Building3") }
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, BuildingDetials, "please Select your Building for the visit");
        }
        private async Task BuildingDetials(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, VistorName, " Almost Done! Please Enter The Name Of The Vistor");
        }
        private async Task VistorName(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, VisitorDetials, "Enter The Company Visitor From");
        }

        private async Task VisitorDetials(IDialogContext context, IAwaitable<object> result)
        {
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Text = "Would You Like To Add More Visitors",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.ImBack, "Yes", value: "Yes"), new CardAction(ActionTypes.ImBack, "No", value: "No") }
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);
            PromptDialog.Text(context, MoreVistors, "Click yes Or No");
        }
        private async Task MoreVistors(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            // string activityresponse = activity.ToString().ToLower();
            if (activity.ToString().ToLower().Equals("yes"))
            {
                PromptDialog.Text(context, VistorName, " Please Enter Other vistor name");
            }
            else if (activity.ToString().ToLower().Equals("no"))
            {
                PromptDialog.Text(context, EndFlow, "Details are noted. Please mention any comments/remarks");
            }
        }
        private async Task EndFlow(IDialogContext context, IAwaitable<object> result)
        {
            string Message = "Thank you. Your request has been raised.Your reference $ number is <ref. number >.You will receive an email on the $confirmation status.$Following are the guidelines for the visitors:$*A badge is only valid for one day. This means that your $visitor needs to report to the building's reception each $morning in order to receive a new badge if he/she is staying $multiple days.  Entry to the building is not available before $07:00 or after 19:00.  Visitor badges will only be valid $between those hours.$* In accordance with Netherlands law, a valid piece of $identification must be carried at all times(or at your place of work).$* Visitors will not be able to park in the garage of Alaska.In $the front of the building there are 10 parking places where $visitors can park, when full please use the facilities of $Projects and Technologies Rijswijk.$This request will be confirmed by the Reception";
            Message = Message.Replace("$", System.Environment.NewLine);

            await context.PostAsync(Message);

            context.Done(result);
        }

    }


}