using BenchmarkDotNet.Attributes;

namespace Cons.Benchmarks;

[MemoryDiagnoser(false)]
public class Some_bench
{
    [Benchmark]
    public void Test1()
    {
        
    }
}