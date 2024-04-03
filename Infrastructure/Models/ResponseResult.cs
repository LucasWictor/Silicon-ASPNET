namespace Infrastructure.Models;

public enum StatusCodes
{
    Ok = 200,
    Error = 400,
    Notfound = 404,
    Exists = 409
}

public class ResponseResult
{
    public StatusCodes StatusCode { get; set; }
    public string ? Message { get; set; }
    public object? ContentResult { get; set; }
}