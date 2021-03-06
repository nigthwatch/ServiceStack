using NUnit.Framework;
using ServiceStack.ServiceInterface.Testing;
using ServiceStack.WebHost.IntegrationTests.Services;

namespace ServiceStack.WebHost.IntegrationTests.Tests
{
	[TestFixture]
	public class RequestAndPathResolutionTests
		: TestBase
	{
		public RequestAndPathResolutionTests()
			: base(typeof(ReverseService).Assembly)
		{
		}

		protected override void Configure(Funq.Container container) { }

		[SetUp]
		public void OnBeforeTest()
		{
			base.OnBeforeEachTest();
		}

		[Test]
		public void Can_process_default_request()
		{
			var request = (EchoRequest)ExecutePath("/Xml/SyncReply/EchoRequest");
			Assert.That(request, Is.Not.Null);
		}

		[Test]
		public void Can_process_default_case_insensitive_request()
		{
			var request = (EchoRequest)ExecutePath("/xml/syncreply/echorequest");
			Assert.That(request, Is.Not.Null);
		}

		[Test]
		public void Can_process_default_request_with_queryString()
		{
			var request = (EchoRequest)ExecutePath("/Xml/SyncReply/EchoRequest?Id=1&String=Value");
			Assert.That(request, Is.Not.Null);
			Assert.That(request.Id, Is.EqualTo(1));
			Assert.That(request.String, Is.EqualTo("Value"));
		}
	}
}