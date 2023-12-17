namespace NetTilt.Tests;
[FeatureDescription(@"All tests")]
[Label("Sqlite")]

public partial class RealSqlite
{
    public override void StartDatabase()
    {
        ConstructServiceProvider();
    }
    
    public override void StopDatabase()
    {
        
    }
    
}
