using System.Diagnostics;

namespace NetTilt.Models
{
    public partial class Models
    {
        static ActivitySource activitySource = new ActivitySource("TILT_SOURCE.Models");
        public static IDisposable? StartMethod(string nameClass, string nameMethod)
        {
            return activitySource.StartActivity($"{nameClass}_{nameMethod}", ActivityKind.Client, default(ActivityContext));
            //return null;
        }
        
    }
}