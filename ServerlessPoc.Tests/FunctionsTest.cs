using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ServerlessPoc.Tests
{
    public class Tests
    {
        private Mock<IAsyncCollector<SignalRMessage>> _collectorMock;
        private readonly ILogger logger = TestFactory.CreateLogger();

        [SetUp]
        public void Setup()
        {
            _collectorMock = new Mock<IAsyncCollector<SignalRMessage>>();
        }

        [Test]
        public void Should_Return_BadRequest_Invalid_Message()
        {
            var request = TestFactory.CreateHttpRequest("name", "value");

            var result = ServerlessPoc.Function.ServerlessRealtime.SendMessage(request, _collectorMock.Object, logger).Result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void ShouldReturn_ValidRequest_SendMessage()
        {
            var data = JsonConvert.SerializeObject(Stub.GetMessageRequest());

            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data));

            var baseRequest = new DefaultHttpContext();
            baseRequest.Request.Body = stream;
            baseRequest.Request.ContentLength = stream.Length;

            var request = new DefaultHttpRequest(baseRequest);

            var result = ServerlessPoc.Function.ServerlessRealtime.SendMessage(request, _collectorMock.Object, logger).Result;

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void ShouldReturn_ValidRequest_Send_Vote()
        {
            var data = JsonConvert.SerializeObject(Stub.GetVoteRequest());

            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data));

            var baseRequest = new DefaultHttpContext();
            baseRequest.Request.Body = stream;
            baseRequest.Request.ContentLength = stream.Length;

            var request = new DefaultHttpRequest(baseRequest);

            var result = ServerlessPoc.Function.ServerlessRealtime.SendMessage(request, _collectorMock.Object, logger).Result;

            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}