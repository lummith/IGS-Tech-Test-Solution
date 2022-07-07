using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QueryApp
{
    public class SendAgent
    {
        private string url = "";

        public SendAgent()
        {
            url = retriveUrl();
        }

        public async Task<string> sendAndReceive()
        {           
            var resString = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var response = await client.GetAsync(url);
                    resString = await response.Content.ReadAsStringAsync();
                }

                return resString;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An execption occured while attempting to send a request to the api: {e.Message}");
                return string.Empty;
            }


        }

        private string retriveUrl()
        {
            string urlFile = @"Files\URL.txt";
            if (File.Exists(urlFile))
            {
                var temp = File.ReadAllText(urlFile);
                if (!string.IsNullOrEmpty(temp))
                    return temp;
                else                
                    return Resources.URL;                  
            }                
            else
                return Resources.URL;
        }
    }
}
