using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace ServiceStack.WebHost.Endpoints.Tests.Mocks
{
	public class HttpResponseMock
		: IHttpResponse
	{
		public HttpResponseMock()
		{
			this.Headers = new NameValueCollection();
			this.OutputStream = new MemoryStream();
            _output = new StreamWriter(this.OutputStream);
			this.TextWritten = new StringBuilder();
		}

		public string GetOutputStreamAsString()
		{
			this.OutputStream.Seek(0, SeekOrigin.Begin);
			using (var reader = new StreamReader(this.OutputStream))
			{
				return reader.ReadToEnd();
			}
		}

		public byte[] GetOutputStreamAsBytes()
		{
			var ms = (MemoryStream)this.OutputStream;
			return ms.ToArray();
		}

		public StringBuilder TextWritten
		{
			get;
			set;
		}

		public int StatusCode { get; set; }

		private string statusDescription = string.Empty;
		public string StatusDescription
		{
			get
			{
				return statusDescription;
			}
			set
			{
				statusDescription = value;
			}
		}

		public string ContentType
		{
			get;
			set;
		}

		public NameValueCollection Headers
		{
			get;
			private set;
		}

		public void AddHeader(string name, string value)
		{
			this.Headers.Add(name, value);
		}

		public void Redirect(string url)
		{
			this.Headers.Add(HttpHeaders.Location, url.MapServerPath());
		}

		public Stream OutputStream
		{
			get;
			private set;
		}

		public void Write(string text)
		{
			this.Output.Write(text);
		}

		public void Close()
		{
			this.IsClosed = true;
		}

		public bool IsClosed
		{
			get;
			private set;
		}

        private TextWriter _output;
        public TextWriter Output
        {
            get { return _output; }
        }
    }
}