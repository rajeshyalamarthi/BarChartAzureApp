using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using FoodOrderMini.JsonResponse;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
namespace FoodOrderMini.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public static string NonvegBirresponse = string.Empty;
        public static string vegBirresponse = string.Empty;
        public static string NonVegBiryaniCount = string.Empty;
        public static string VegBiryaniCount = string.Empty;

        public static string RegularPizzaResponse = string.Empty;
        public static string MediumPizzaResponse = string.Empty;
        public static string RegularPizzaCount = string.Empty;
        public static string MediumPizzaCount = string.Empty;

        public static string SoftDrinksResponse = string.Empty;
        public static string ThickShakesResponse = string.Empty;
        public static string SoftDrinksCount = string.Empty;
        public static string ThickShakesCount = string.Empty;

        public static int ChickenBiryaniCost = 0;
        public static int VegBiryaniCost = 0;
        public static int RegularPizzaCost = 0;
        public static int MediumPizzaCost = 0;
        public static int SoftDrinksCost = 0;
        public static int ThickShakesCost = 0;
        public static int Finalcost = 0;
        public static string FinalOrderCount = "";
        public Task StartAsync(IDialogContext context)
        {
            try
            {
                context.Wait(MessageReceivedAsync);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                context.PostAsync(ex.Message);
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            Rootobject rootobject = new Rootobject();
            try
            {
                var activity = await result as Activity;
                var Userresponse = activity.Text;
                using (HttpClient client = new HttpClient())
                {
                    string RequestURI = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/7b90b679-aaef-458e-9b9d-33a03cf9fd70?subscription-key=12b0368d58924683b7b0907c56a89525&timezoneOffset=-360&q=" + Userresponse;
                    HttpResponseMessage msg = await client.GetAsync(RequestURI);

                    if (msg.IsSuccessStatusCode)
                    {
                        var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                        rootobject = JsonConvert.DeserializeObject<Rootobject>(JsonDataResponse);
                    }


                    if (rootobject.topScoringIntent.intent == "DisplayFood") //if activty is equal to DisplayFood Then MenuCarousel method is called
                    {
                        try
                        {
                            await context.PostAsync(DisplayFoodCarousel.MenuCarousel(activity, context));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "BiryaniOrder") //if activty is equal to BiryaniOrder Then ItemTypes method is called by passing all the parameters required for herocard
                    {
                        try
                        {
                            string Title = "BIRYANI";
                            string Subtitle = "Experience The Savage";
                            string Text = "Choose The Biryani";
                            string Image = "http://res.cloudinary.com/prayuta/image/upload/v1500964175/Chicken-Biryani-1_fudjk7.jpg";
                            string Button1 = "NonVegBiryani";
                            string Button2 = "VegBiryani";
                            await context.PostAsync(ItemtypesDialog.ItemTypes(activity, context, Title, Subtitle, Text, Image, Button1, Button2));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "PizzaOrder")//if activty is equal to PizzaOrder Then ItemTypes method is called by passing all the parameters required for herocard
                    {
                        try
                        {
                            string Title = "Pizza";
                            string Subtitle = "Experience The Savage";
                            string Text = "Choose The Pizza";
                            string Image = "https://images5.alphacoders.com/396/thumb-1920-396575.jpg";
                            string Button1 = "Regular";
                            string Button2 = "Medium";
                            await context.PostAsync(ItemtypesDialog.ItemTypes(activity, context, Title, Subtitle, Text, Image, Button1, Button2));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "BeveragesOrder")//if activty is equal to BeveragesOrder Then ItemTypes method is called by passing all the parameters required for herocard
                    {
                        {
                            try
                            {
                                string Title = "Beverages";
                                string Subtitle = "Experience The Savage";
                                string Text = "Feel The End";
                                string Image = "https://st.depositphotos.com/1044737/4943/i/950/depositphotos_49431317-stock-photo-colorful-soda-drinks-with-cola.jpg";
                                string Button1 = "SoftDrinks";
                                string Button2 = "ThickShakes";
                                await context.PostAsync(ItemtypesDialog.ItemTypes(activity, context, Title, Subtitle, Text, Image, Button1, Button2));
                            }
                            catch (Exception ex)
                            {
                                await context.PostAsync(ex.Message);
                               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                            }
                        }
                    }

                   
                    else if (rootobject.topScoringIntent.intent == "NonVegBiryani")//if activty is equal to NonVegBiryani Then Nonvegbiryanicount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(NonVegBiryaniDialog.Nonvegbiryanicount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                          // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "VegBiryani")//if activty is equal to VegBiryani Then Vegbiryanicount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(VegBiryaniDialog.Vegbiryanicount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent== "Regular")//if activty is equal to Regular Then RegularPizzaCount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(RegularPizza.RegularPizzaCount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "Medium")//if activty is equal to Medium Then MediumPizzaCount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(MediumPizzaDialog.MediumPizzaCount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "SoftDrinks")//if activty is equal to SoftDrinks Then SoftDrinksCount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(SoftDrinksDialog.SoftDrinksCount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "ThickShakes")//if activty is equal to ThickShakes Then ThickShakesCount method is called by passing all the parameters required
                    {
                        try
                        {
                            await context.PostAsync(ThickShakesDialog.ThickShakesCount(activity, context, Userresponse));
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "CheckOut")// if user wants to checkout then the Food Which He Ordered Will Be Displayed Along With The Cost
                    {
                        try
                        {
                            await context.PostAsync(CheckoutDialog.Checkout(activity, context));
                        }
                        catch (Exception ex)
                        {
                            //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.topScoringIntent.intent == "Confirm")// If User Confirm The Order Then The Message Of Food Is Getting Prepared will be displayed
                    {
                        try
                        {
                            var Confirmmessage = "Yeah Your Food Is Getting Prepared, We Well Deliver You..Thank You...Enjoy The Food";
                            //  ConversationLog.ConversationLogg(context.Activity.From.Name, activity.Text, Confirmmessage.ToString(), DateTime.Now, context.Activity.ChannelId);
                            await context.PostAsync(Confirmmessage);
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if ( rootobject.entities[0].type == "builtin.number" && NonvegBirresponse != null)//if NonvegBirresponse != null Then NonVegBiryaniOrder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int biryanicount = 0;
                            Int32.TryParse(activity.Text, out biryanicount);
                            if (biryanicount >= 0 && biryanicount <= 15)//if orderd biryani count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(NonVegBiryaniDialog.NonVegBiryaniOrder(activity, context, biryanicount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order The Biryani In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.entities[0].type == "builtin.number" && vegBirresponse != null)//if vegBirresponse != null Then VegBiryaniOrder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int biryanicount = 0;
                            Int32.TryParse(activity.Text, out biryanicount);
                            if (biryanicount >= 0 && biryanicount <= 15)//if orderd biryani count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(VegBiryaniDialog.VegBiryaniOrder(activity, context, biryanicount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order The Biryani In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.entities[0].type == "builtin.number" && RegularPizzaResponse != null)//if RegularPizzaResponse != null Then Regularpizzaorder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int pizzacount = 0;
                            Int32.TryParse(activity.Text, out pizzacount);
                            if (pizzacount >= 0 && pizzacount <= 15)//if orderd pizzacount count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(RegularPizza.Regularpizzaorder(activity, context, pizzacount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order The Pizza In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.entities[0].type == "builtin.number" && MediumPizzaResponse != null)//if MediumPizzaResponse != null Then MediumPizzaOrder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int pizzacount = 0;
                            Int32.TryParse(activity.Text, out pizzacount);
                            if (pizzacount >= 0 && pizzacount <= 15)//if orderd pizzacount count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(MediumPizzaDialog.MediumPizzaOrder(activity, context, pizzacount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order The Pizza In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                           // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }

                    else if (rootobject.entities[0].type == "builtin.number" && SoftDrinksResponse != null)//if SoftDrinksResponse != null Then SoftDrinksOrder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int drinkscount = 0;
                            Int32.TryParse(activity.Text, out drinkscount);
                            if (drinkscount >= 0 && drinkscount <= 15)//if orderd drinkscount count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(SoftDrinksDialog.SoftDrinksOrder(activity, context, drinkscount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order SoftDrinks In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                            //ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }
                    else if (rootobject.entities[0].type == "builtin.number" && ThickShakesResponse != null)//if ThickShakesResponse != null Then ThickShakesOrder method is called After Performing some validations by passing all the parameters required
                    {
                        try
                        {
                            int drinkscount = 0;
                            Int32.TryParse(activity.Text, out drinkscount);
                            if (drinkscount >= 0 && drinkscount <= 15)//if orderd drinkscount count is greater than or equal to zero and less than or equal to 15 then the method is called
                            {
                                await context.PostAsync(ThickShakesDialog.ThickShakesOrder(activity, context, drinkscount));
                            }
                            else
                            {
                                await context.PostAsync("Please Order ThickShakes In Range");
                            }
                        }
                        catch (Exception ex)
                        {
                            await context.PostAsync(ex.Message);
                          //  ErrorLog.errorloginfo(ex.Message, DateTime.Now);
                        }
                    }

                    else
                    {
                        await context.PostAsync("sorry iam unable to find your request");
                    }
                   
                    context.Wait(MessageReceivedAsync);
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
               // ErrorLog.errorloginfo(ex.Message, DateTime.Now);
            }

        }
    }
}