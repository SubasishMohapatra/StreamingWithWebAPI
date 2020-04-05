//using Newtonsoft.Json;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace StreamingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Printing values:\n");
            _ = Task.Run(async () =>
              {
                  //await GetValues();
                  return await GetComplexObjects();
              }).ContinueWith((t) =>
              {
                  var results = t.Result;
                  Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
              });
            Console.ReadKey(true);
        }

        static async Task GetValues()
        {
            //var result = Enumerable.Empty<int>();
            //var serializer = new JsonSerializer();
            var client = new HttpClient();
            var header = new MediaTypeWithQualityHeaderValue("application/json");

            client.DefaultRequestHeaders.Accept.Add(header);

            try
            {
                // Note: port number might vary.
                using (var stream = await client.GetStreamAsync("http://localhost:7654/DPE/Cache/GetData"))
                using (var sr = new StreamReader(stream))
                //using (var jr = new JsonTextReader(sr))
                {
                    while (true)
                    {
                        if (int.TryParse(await sr.ReadLineAsync(), out int numData))
                        {
                            Console.WriteLine(numData);
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception x)
            {

            }
        }


        static async Task<List<ComplexObject>> GetComplexObjects()
        {
            var complexObjects = new List<ComplexObject>();
            var client = new HttpClient();
            var header = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(header);
            try
            {
                using (var stream = await client.GetStreamAsync("http://localhost:7654/DPE/Cache/GetComplexObjects"))
                using (var sr = new StreamReader(stream))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string jsonString = await sr.ReadLineAsync();
                            var complexObject = JsonConvert.DeserializeObject<ComplexObject>(jsonString);
                            if (complexObject != null)
                                complexObjects.Add(complexObject);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return complexObjects;
        }
    }
}
