using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        public class TestPostRequestModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int UserId { get; set; }
            public string Body { get; set; }
        }
        public class TestPostResponseMdel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int UserId { get; set; }
            public string Body { get; set; }
            public override string ToString()
            {
                return $"{Id}\n{Title}\n{Body}\n";
            }
        }
        public class DateTimeRespooseModel
        {
            //[JsonProperty("$id")]
            public string Id { get; set; }
            public string currentDateTime { get; set; }
            public string utcOffset { get; set; }
            public bool isDayLightSavingsTime { get; set; }
            public string dayOfTheWeek { get; set; }
            public string timeZoneName { get; set; }
            public long currentFileTime { get; set; }
            public string ordinalDate { get; set; }
            public object serviceResponse { get; set; }
            public override string ToString()
            {
                return $"{currentDateTime}\n{dayOfTheWeek}\n{ordinalDate}\n";
            }
        }
        static void Main(string[] args)
        {
            DateTimeRespooseModel model = new();
            TestPostResponseMdel responseModel = new();
            TestPostRequestModel postModel = new();
            var apiService = new BaseApiService.BaseApiService();
            string response = null;
            Task.Run(async () =>
            {
                HttpResponseMessage responseMessage = await apiService.ExecuteGetAsync("http://worldclockapi.com/api/json/est/now");
                response = await responseMessage.Content.ReadAsStringAsync();
            }).Wait();
            Console.WriteLine("\n---------------------------\nGET:\nreturn: HttpResponseMessage\n-------------------------- ");
            Console.WriteLine(response);

            Task.Run(async () =>
            {
                model =
                await apiService.GetAsync<DateTimeRespooseModel>("http://worldclockapi.com/api/json/est/now");

            }).Wait();
            Console.WriteLine("\n---------------------------\nGET:\nreturn: Desirialize model\n-------------------------- ");
            Console.WriteLine(model);

            Console.WriteLine("\n***************\ntest post reuest\n*******************\n");

            Task.Run(async () =>
            {
                responseModel =
                await apiService.PostAsync<TestPostResponseMdel>("http://jsonplaceholder.typicode.com/users", new TestPostRequestModel { Id = 1 });

            }).Wait();
            Console.WriteLine("\n---------------------------\nPOST:\nreturn: Desirialize model\n-------------------------- ");
            Console.WriteLine(responseModel);

            Task.Run(async () =>
            {
                HttpResponseMessage responseMessage = await apiService.ExecutePostAsync("http://jsonplaceholder.typicode.com/posts", new TestPostRequestModel { Id = 1, Body = "Lorem....." });
                response = await responseMessage.Content.ReadAsStringAsync();
            }).Wait();
            Console.WriteLine("\n---------------------------\nPOST:\nreturn: HttpResponseMessage\n-------------------------- ");
            Console.WriteLine(response);



            Console.Read();

        }
    }
}
