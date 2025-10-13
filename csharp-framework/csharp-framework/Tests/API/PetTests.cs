using csharp_framework.Utils;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Tests.API
{
    public class PetTests
    {
        [Test, Category("Regression")]
        public void AddPet_ShouldReturn200()
        {
            var client = new RestClient(ConfigManager.Settings.ApiUrl);
            var request = new RestRequest("/pet", Method.Post);
            request.AddJsonBody(new { id = 12345, name = "Fluffy", status = "available" });

            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
    }
}
