namespace NetTilt.WebAPI;

public class ServerTimingMid : IMiddleware
{
    private readonly IServerTiming serverTiming;

    public ServerTimingMid(IServerTiming serverTiming)
    {
        this.serverTiming = serverTiming;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        this.serverTiming.Metrics.Add(new ("startRequest",(decimal)0.001) );
        await next(context);               
    }
}
