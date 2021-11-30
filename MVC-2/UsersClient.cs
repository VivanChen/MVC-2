using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC_2
{
    public class UsersClient
    {
        private HttpClient client;

        public UsersClient()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(200);
        }


        public async Task<string> BatchPostAsync(string url, object data)
        {
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(url, byteContent).ConfigureAwait(false);

                var Result = await response.Content.ReadAsStringAsync();

                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
