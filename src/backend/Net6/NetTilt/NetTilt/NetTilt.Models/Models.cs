using System.Diagnostics;

namespace NetTilt.Models
{
    public partial class Models
    {
        static ActivitySource activitySource = new ActivitySource("TILT_SOURCE.Models");
        public static IDisposable? StartMethod(string nameClass, string nameMethod)
        {
            var cur = Activity.Current;
            var cnt = cur?.Context ?? default(ActivityContext);            
            var act= activitySource.StartActivity($"{nameClass}_{nameMethod}",ActivityKind.Internal,cnt);
            act?.SetTag("Andrei", "test11");
            //Activity.Current = act;
            //act?.SetTag("peer.service", "aaa");
            //act?.SetTag("net.peer.name", "bbb");
            //act?.SetTag("net.host.name", "ccc");
            //act?.SetTag("service.name", "ddd");
            //act?.SetTag("service.namespace", "eee");
            //act?.SetTag("service.instance.id", "fff");
            //act= act?.AddEvent(new ActivityEvent("ggg"+nameMethod));

            //act = act?.AddEvent(new ActivityEvent("aaaaaaaaaa"));
            return act;
            //return null;
        }
        
    }
    
}