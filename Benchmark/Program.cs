using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmark;

public class Benchmark
{
    private class FooBar
    {
        public string Foo { get; init; } = string.Empty;
        public string Bar { get; init; } = string.Empty;
        public DateTime Baz { get; init; }
    }

    private const string Chars = "123";
    private const int CountCharsInString = 32;
    private const int SampleSize = 30;
    private const int NumberSamples = 1000;
    private const int TimeDelta = 10;

    private readonly List<List<FooBar>> _randomSets;

    public Benchmark()
    {
        var random = new Random();
        _randomSets = new List<List<FooBar>>(NumberSamples);

        for (int sampleCounter = 0; sampleCounter < NumberSamples; ++sampleCounter)
        {
            _randomSets.Add(new List<FooBar>(SampleSize));
            for (int objectCounter = 0; objectCounter < SampleSize; objectCounter++)
            {
                string fooChar = Chars[random.Next(Chars.Length)].ToString();
                string barChar = Chars[random.Next(Chars.Length)].ToString();
                var baz = DateTime.Now.AddHours(random.Next(TimeDelta));

                _randomSets[sampleCounter].Add(new FooBar()
                {
                    Foo = new StringBuilder().Insert(0, fooChar, CountCharsInString).ToString(),
                    Bar = new StringBuilder().Insert(0, barChar, CountCharsInString).ToString(),
                    Baz = baz
                });
            }
        }
    }

    [Benchmark(Description = "Linq")]
    public void BenchLinq()
    {
        LinkedList<LinkedList<FooBar>> result = new LinkedList<LinkedList<FooBar>>();

        foreach (var sampleSet in _randomSets)
        {
            result.AddLast(
                new LinkedList<FooBar>(
                    sampleSet.DistinctByWithMax(f => (f.Foo, f.Bar), f => f.Baz)));
        }
    }
    
    [Benchmark(Description = "Sort")]
    public void BenchSort()
    {
        LinkedList<LinkedList<FooBar>> result = new LinkedList<LinkedList<FooBar>>();

        foreach (var sampleSet in _randomSets)
        {
            result.AddLast(
                new LinkedList<FooBar>(
                    sampleSet.DistinctByWithMaxSorted(f => (f.Foo, f.Bar), f => f.Baz)));
        }
    }

    [Benchmark(Description = "Hash")]
    public void BenchHash()
    {
        LinkedList<LinkedList<FooBar>> result = new LinkedList<LinkedList<FooBar>>();

        foreach (var sampleSet in _randomSets)
        {
            result.AddLast(
                new LinkedList<FooBar>(
                    sampleSet.DistinctByWithMaxHash(f => (f.Foo, f.Bar), f => f.Baz)));
        }
    }
}

internal static class Program
{
    private static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchmark>();
    }
}