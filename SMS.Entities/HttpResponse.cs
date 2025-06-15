
using System.Net;

namespace SMS.Entities
{
    public class HttpResponse<T>
    {
        public T Result { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
