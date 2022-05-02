using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTilt.Tests
{
    partial class TestAuth : FeatureFixture
    {
        ServiceProvider? serviceProvider;
        void SetupTilt(DateTime? oneTILTInTheDB)
        {
            var searchUrl = new Mock<ISearchDataTILT_URL>();
            var insert = new Mock<I_InsertDataApplicationDBContext>();
            insert.Setup(x => x.InsertTILT_Note(It.IsAny<TILT_Note_Table>())).Returns(Task.FromResult(new TILT_Note_Table()));




            var searchNote = new Mock<ISearchDataTILT_Note>();
            TILT_Note[] retArray;
            if (oneTILTInTheDB.HasValue)
            {
                retArray = new TILT_Note[1];
                retArray[0] = new TILT_Note();
                retArray[0].ForDate = oneTILTInTheDB.Value;
            }
            else
            {
                retArray = Array.Empty<TILT_Note>();
            }

            searchNote.Setup(it => it.TILT_NoteFind_AsyncEnumerable(It.IsAny<SearchTILT_Note?>())).Returns(retArray.ToAsyncEnumerable());

            var auth = new Mock<IAuthUrl>();
            auth.Setup(x => x.MainUrlId(null)).Returns(1);
            //var q = new SearchDataTILT_URL(null);

            serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IMyTilts, MyTilts>()
            .AddScoped<ISearchDataTILT_URL>(sp => searchUrl.Object)
            .AddScoped<I_InsertDataApplicationDBContext>(sp => insert.Object)
            .AddScoped<ISearchDataTILT_Note>(sp => searchNote.Object)
            .AddScoped<IAuthUrl>(sp => auth.Object)
            //.AddSingleton<IBarService, BarService>()
            .BuildServiceProvider();

        }
    }
}
