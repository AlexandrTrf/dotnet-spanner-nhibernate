using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Gax;
using Google.Cloud.Spanner.Admin.Database.V1;
using Google.Cloud.Spanner.Common.V1;
using Google.Cloud.Spanner.NHibernate.Samples.SampleModel;
using NHibernate;
using NHibernate.Multi;

namespace Google.Cloud.Spanner.NHibernate.Samples.Snippets
{
    public class BatchSample
    {
        public static async Task Run(SampleConfiguration configuration)
        {
            using var session = configuration.SessionFactory.OpenSession();

            string[] ids = { "131", "141", "151", "161", "171" };
            var queries = session.CreateQueryBatch()
                .Add<Singer>(
                    session.CreateQuery("from Singers s where s.Id in :Ids")
                        .SetParameterList("Ids", ids)
                        .SetFirstResult(3))
                .Add<long>(
                    session.CreateQuery("select count(*) from Singers s where s.Id in :Ids")
                        .SetParameterList("Ids", ids));
            queries.Execute();

        }
    }
}