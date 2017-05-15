using BenchmarkDotNet.Attributes;
using Mighty;
using System.Linq;

namespace Dapper.Tests.Performance
{
    public class MightyBenchmarks : BenchmarkBase
    {
		private MightyORM _modelDynamic;
		private MightyORM<Post> _modelGeneric;

		[Setup]
        public void Setup()
        {
            BaseSetup();
			_modelDynamic = new MightyORM(ConnectionString + ";ProviderName=System.Data.SqlClient");
			_modelGeneric = new MightyORM<Post>(ConnectionString + ";ProviderName=System.Data.SqlClient");
		}

		[Benchmark(Description = "Query (dynamic)")]
		public dynamic QueryDynamic()
		{
			Step();
			var result = _modelDynamic.Query("select * from Posts where Id = @0", _connection, i).First();
			return result;
		}

		[Benchmark(Description = "Query (generic)")]
		public Post QueryGeneric()
		{
			Step();
			var result = _modelGeneric.Query("select * from Posts where Id = @0", _connection, i).First();
			return result;
		}
	}
}