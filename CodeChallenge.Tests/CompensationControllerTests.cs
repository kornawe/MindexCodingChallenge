
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = Guid.NewGuid().ToString(),
                EffectiveDate = DateTime.Now,
                Salary = 100000.00,
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        }

        /// <summary>
        ///     Compensation is not poulated by a db at startup. With no data, 
        ///     no compensation should be found.
        /// </summary>
        [TestMethod]
        public void GetCompensationById_Returns_NotFound()
        {
            // Arrange
            var compensationId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensationId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            // Post a new compensation before so that is can be queried during
            // the execution portion
            var compensation = new Compensation()
            {
                EmployeeId = Guid.NewGuid().ToString(),
                EffectiveDate = DateTime.Now,
                Salary = 100000.00,
            };

            var requestContent = new JsonSerialization().ToJson(compensation);
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            // Post funcitonality should be covered in another test, the result
            // needs to be collected so that the task finishes execution.
            var postResponse = postRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var foundCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(foundCompensation.EmployeeId);
            Assert.AreEqual(compensation.EffectiveDate, foundCompensation.EffectiveDate);
            Assert.AreEqual(compensation.Salary, foundCompensation.Salary);
        }

    }
}
