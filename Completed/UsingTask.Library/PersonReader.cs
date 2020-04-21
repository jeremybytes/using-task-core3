using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UsingTask.Shared;

namespace UsingTask.Library
{
    public class PersonReader
    {
        HttpClient client = new HttpClient();

        public PersonReader()
        {
            client.BaseAddress = new Uri("http://localhost:9874");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Person>> GetAsync(
            CancellationToken cancelToken = new CancellationToken())
        {
            await Task.Delay(3000, cancelToken);

            //throw new NotImplementedException("GetAsync is not implemented.");

            cancelToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await client.GetAsync("people", cancelToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Person>>(stringResult);
            }
            return new List<Person>();
        }
    }
}