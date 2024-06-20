namespace EmployeeClient.Infrastructure
{
    public interface IHttpClientService
    {
        
        HttpResponseMessage GetHttpResponseMessage<TService>(string url, HttpRequest? httpRequest = null)
         where TService : class;

        HttpResponseMessage GetHttpResponseMessage<TService>(string url, object id, HttpRequest? httpRequest = null)
        where TService : class;

       
        HttpResponseMessage PostHttpResponseMessage<TService>(string url, TService entityDto, HttpRequest? httpRequest = null)
        where TService : class;

       
        HttpResponseMessage PostHttpResponseMessage<TService>(string url, HttpRequest? httpRequest = null)
        where TService : class;

        
        HttpResponseMessage PutHttpResponseMessage<TService>(string url, TService entityDto, HttpRequest? httpRequest = null)
         where TService : class;


        T ExecuteApiRequest<T>(string url, HttpMethod method, HttpRequest httpRequest, object? parameters = null, int? TimeOutInSecond = 60)
        where T : class;
    }
}
