using Infrastructure.Models;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    //ok
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message ="Succeeded",
            StatusCode = StatusCodes.Ok 
        };
    }
    //custom message
    public static ResponseResult Ok(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Succeeded",
            StatusCode = StatusCodes.Ok 
        };
    }
    //object + custom message
    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message ?? "Succeeded",
            StatusCode = StatusCodes.Ok 
        };
    }
    
    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Failed",
            StatusCode = StatusCodes.Error 
        };
    }
    
    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Already exists",
            StatusCode = StatusCodes.Exists 
        };
    }
    
    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not found",
            StatusCode = StatusCodes.Notfound 
        };
    }
}