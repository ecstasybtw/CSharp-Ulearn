using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace StructBenchmarking;
public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();                   
        GC.WaitForPendingFinalizers();  
		task.Run();
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < repetitionCount; i++)
            task.Run();
        stopwatch.Stop();

        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
	}
}

public class StringConstructorTask : ITask
    {
        public void Run()
        {
            var result = new string('a', 10000);
        }
    }

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var builder = new System.Text.StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                builder.Append('a');
            }
            var result = builder.ToString();
        }
    }

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        var constructorTask = new StringConstructorTask();
        var builderTask = new StringBuilderTask();

        var repetitionCount = 1000; 
        var constructorDuration = benchmark.MeasureDurationInMs(constructorTask, repetitionCount);
        var builderDuration = benchmark.MeasureDurationInMs(builderTask, repetitionCount);

        Assert.Less(constructorDuration, builderDuration);
    }
}