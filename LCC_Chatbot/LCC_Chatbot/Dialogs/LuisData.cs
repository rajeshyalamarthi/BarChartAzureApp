using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DALRepository;
using LCC_Chatbot.Attachments;
using LCC_Chatbot.LuisResponse;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
namespace LCC_Chatbot.Dialogs
{
    public static class LuisData
    {
        public static async Task<Rootobject> ReturnLuisData(string Userresponse)
        {
            try
            {
                Rootobject rootobject = new Rootobject();
                using (HttpClient client = new HttpClient())// inorder to get the luis response based on user utterance
                {
                    string RequestURI = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/1afb52f4-d585-458c-a587-2161dc87a9ae?subscription-key=d24548e8ffc9415ea2345bf400bb8a17&timezoneOffset=-360&q=" + Userresponse;
                    HttpResponseMessage msg = await client.GetAsync(RequestURI);

                    if (msg.IsSuccessStatusCode)
                    {
                        var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                        rootobject = JsonConvert.DeserializeObject<Rootobject>(JsonDataResponse);
                    }

                    return rootobject;
                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }


        public static Attachment CreateAdaptiveCardAttachment(string filePath)
        {
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }
    }
}