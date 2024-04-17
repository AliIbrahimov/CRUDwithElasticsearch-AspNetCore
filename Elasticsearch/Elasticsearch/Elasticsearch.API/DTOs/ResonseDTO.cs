using System.Net;

namespace Elasticsearch.API.DTOs;

public class ResponseDTO<T>
{
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public HttpStatusCode code { get; set; }
    public static ResponseDTO<T> Success(T data, HttpStatusCode status)
    {
        return new ResponseDTO<T> { Data = data, code = status };
    }

    public static ResponseDTO<T> Fail(List<string> errors, HttpStatusCode statusCode) {
        return new ResponseDTO<T> { Errors = errors, code = statusCode };
    }
    public static ResponseDTO<T>  Fail(string error, HttpStatusCode statusCode) 
    {
        return new ResponseDTO<T> { Errors = new List<string> { error}, code = statusCode };
    }

}
