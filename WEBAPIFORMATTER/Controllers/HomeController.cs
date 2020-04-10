using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MessagePack;
using RestSharp;

namespace WEBAPIFORMATTER.Controllers
{
    public class HomeController : ApiController
    {
        public void Get()
        {
            var response = new Response_Base<List<test>>();
            response.data = Enumerable.Range(1, 10).Select(x => new test() { MyProperty = x }).ToList();
            var rr= MessagePack.MessagePackSerializer.Serialize(response);
            var client = new RestClient("http://127.0.0.1:8080/api/home/get1");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/x-msgpack");
            request.AddHeader("Accept", "application/x-msgpack");
            request.AddHeader("Cookie", "ASP.NET_SessionId=6BF4446F1EABC2E1E222A707");
            request.AddParameter("application/x-msgpack", rr, ParameterType.RequestBody);
            IRestResponse resp = client.Execute(request);
            Console.WriteLine(resp.Content);
        }

        public Response_Base<List<test>> Put(Response_Base<List<test>> req) {
            return req;
        }
    }

    [MessagePackObject]
    public class Response_Base<T>
    {
        [Key(0)]
        public bool isSuccess { get; set; }
        [Key(1)]
        public string message { get; set; }
        [Key(2)]
        public T data { get; set; }
    }
    [MessagePackObject]
   public class test
    {
        [Key(0)]
        public object MyProperty { get; set; }
    }
}
