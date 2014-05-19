using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace JoshCodes.Web.Http.Formatting
{
    public  class MultipartMediaTypeFormatter : System.Net.Http.Formatting.MediaTypeFormatter
    {
        private class MultipartMediaTypeMapping : MediaTypeMapping
        {
            public MultipartMediaTypeMapping()
                : base("multipart/mixed")
            {

            }

            public override double TryMatchMediaType(HttpRequestMessage request)
            {
                var topMatch = 0.0;
                foreach (var accept in request.Headers.Accept)
                {
                    if(accept.MediaType.StartsWith("multipart"))
                    {
                        if (accept.Quality.HasValue)
                            return accept.Quality.Value;
                        return 0.99;
                    }
                }
                return topMatch;
            }
        }

        public MultipartMediaTypeFormatter()
        {
            this.MediaTypeMappings.Add(new MultipartMediaTypeMapping());
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/mixed"));
        }

        public override bool CanReadType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public override bool CanWriteType(Type type)
        {
            var canWrite = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            return canWrite;
        }

        public override void SetDefaultContentHeaders(Type type, System.Net.Http.Headers.HttpContentHeaders headers, System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var boundry = Guid.NewGuid().ToString();
            var multipartFormDataContent = new MultipartContent("mixed", boundry);

            //content. .StatusCode = (int)System.Net.HttpStatusCode.OK;
            var contentType = new MediaTypeHeaderValue("multipart/mixed"); // String.Format("multipart/mixed; boundary={0}", boundry));
            var boundaryParameter = new NameValueHeaderValue("boundary", boundry);
            contentType.Parameters.Add(boundaryParameter);
            contentType.Parameters.Add(new System.Net.Http.Headers.NameValueHeaderValue("revision", "0.1"));
            content.Headers.ContentType = contentType;
            //content.Headers.Add("Accept", "multipart/mixed");
            //content.Headers.Add("MIME-Version", "1.0");

            var entities = (IEnumerable<object>)value;
            foreach (var entity in entities)
            {
                var entityContent = new ObjectContent<object>(entity, new JsonMediaTypeFormatter());
                entityContent.Headers.LastModified = DateTime.UtcNow; // Add("Last-Modified", entity.LastModified.ToUniversalTime().ToString("R"));
                entityContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-" + entity.GetType().Name);
                multipartFormDataContent.Add(entityContent);
            }
            return multipartFormDataContent.CopyToAsync(writeStream);
        }
    }
}
