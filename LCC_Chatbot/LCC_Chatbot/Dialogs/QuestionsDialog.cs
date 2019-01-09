using DALRepository;
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
    public class QuestionsDialog : IDialog<object>
    {
        public static List<QuestionsList> questions = new List<QuestionsList>();
        public static Dictionary<string, string> SecQuestions = new Dictionary<string, string>();
        public Task StartAsync(IDialogContext context)
        {
            questions = PostResponse.Questions(RootDialog.Intent);
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[0].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            context.PostAsync(replymessage);
            PromptDialog.Text(context, secondquestion,"answer above question");
            //context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;

        }

        private async Task secondquestion(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[0].Questions, activity.ToString());
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[1].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, ThirdQuestion, "answer above question");
        }



        private async Task ThirdQuestion(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[1].Questions, activity.ToString());
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[2].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, FourthQuestion, "answer above question");
        }



        private async Task FourthQuestion(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[2].Questions, activity.ToString());
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[3].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, FifthQuestion, "answer above question");
        }



        private async Task FifthQuestion(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[3].Questions, activity.ToString());
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[4].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, SixthQuestion, "answer above question");
        }

        private async Task SixthQuestion(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[4].Questions, activity.ToString());
            var replymessage = context.MakeMessage();
            var herocard = new ThumbnailCard
            {
                Subtitle = "Please answer the below question",
                Text = questions[5].Questions
            };
            replymessage.Attachments.Add(herocard.ToAttachment());
            await context.PostAsync(replymessage);

            PromptDialog.Text(context, Last, "answer above question");
        }

        private async Task Last(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;

            SecQuestions.Add(questions[5].Questions, activity.ToString());
            string QuestionandAnswers="";
            string val;
            foreach(string value in SecQuestions.Keys)
            {
                SecQuestions.TryGetValue(value, out val);
                QuestionandAnswers += value + "  -  " + val+"<br/>";
            }
           await context.PostAsync(QuestionandAnswers);
            context.Done(result);
           
        }
    }
}
    