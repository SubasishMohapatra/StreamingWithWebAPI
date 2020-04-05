using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace DCX.DPE.Service.Cache.Controller
{
    /// <summary>
    /// The AkvController class.
    /// </summary>
    //[Route("Cache")]
    public class CacheController : ApiController
    {
        /// <summary>
        /// The Get method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Cache/About")]
        public string About()
        {
            return "This is a Cache web api";
        }

        [HttpGet]
        [Route("Cache/GetData")]
        public HttpResponseMessage PushStreamContent()
        {
            var response = Request.CreateResponse();

            response.Content =
                new PushStreamContent((stream, content, context) =>
                {
                    foreach (var num in Enumerable.Range(0, 100000))
                    {

                        //var serializer = new JsonSerializer();
                        using (var writer = new StreamWriter(stream))
                        {
                            try
                            {
                                //serializer.Serialize(writer, num.ToString());
                                writer.WriteLineAsync(num.ToString());
                                writer.FlushAsync();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                });

            return response;
        }

        [HttpGet]
        [Route("Cache/GetComplexObjects")]
        public HttpResponseMessage GetComplexObjects()
        {
            var response = Request.CreateResponse();

            response.Content =
                new PushStreamContent(async (stream, content, context) =>
                {
                    foreach (var num in Enumerable.Range(1, 10000))
                    {
                        var complexObject = new ComplexObject()
                        {
                            Name = $"Name{num}",
                            Age = num % 100,
                            Gender = (Gender)(num % 2)
                        };
                        var jsonObject = JsonConvert.SerializeObject(complexObject);
                        var serializer = new JsonSerializer();
                        using (var writer = new StreamWriter(stream))
                        {
                            try
                            {
                                serializer.Serialize(writer, complexObject);
                                await writer.WriteLineAsync();
                                await writer.FlushAsync();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                });

            return response;
        }
    }
}