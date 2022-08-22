using Microsoft.Extensions.DependencyInjection;
using NetTilt.Logic;

namespace NetTilt.Tests;
public  abstract partial class RealDBTests: FeatureFixture
{
    public abstract void StartDatabase();

    public abstract void StopDatabase();
    
    [SetUp]
    public void Start()
    {
        StartDatabase();
    }
    [TearDown]
    public void Stop()
    {
        StopDatabase();
    }
}
