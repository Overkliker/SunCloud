using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SunCloud.Model;

namespace SunCloud.ViewModel.Helpers
{
    internal class Serializer
    {
        private static string url = "https://api.weatherapi.com/v1/forecast.json?key=9335a795c4fe42d3bcc72042232106&lang=ru&q=";

        public static Root Get(string city)
        {
            try
            {
                //Подключение клиента
                HttpClient client = new HttpClient();
                //Подгрузка стринг контента
                HttpResponseMessage message = client.GetAsync(url + $"{city}&aqi=no").Result;
                //Получение сообщения ответа сервера
                message.EnsureSuccessStatusCode();
                string result = message.Content.ReadAsStringAsync().Result;
                //Десиреализация строки данных
                Root all = JsonConvert.DeserializeObject<Root>(result);
                //
                return all;
            }
            catch (Exception e)
            {
                Root empt = new Root();
                return empt;
            }
        }
    }
}
