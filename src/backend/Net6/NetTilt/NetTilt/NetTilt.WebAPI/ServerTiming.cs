using System.Diagnostics;

namespace NetTilt.WebAPI;

public class ServerTiming : IMiddleware
{
    private readonly IServerTiming serverTiming;

    public ServerTiming(IServerTiming serverTiming)
    {
        this.serverTiming = serverTiming;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        this.serverTiming.AddMetric((decimal)0.001, "startRequest");
        await next(context);               
    }
}
