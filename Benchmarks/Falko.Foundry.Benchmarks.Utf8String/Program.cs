using BenchmarkDotNet.Running;
using Falko.Benchmarks;

BenchmarkRunner.Run<CreateStringBenchmark>();
BenchmarkRunner.Run<ConvertStringBenchmark>();
BenchmarkRunner.Run<AppendStringBenchmark>();
BenchmarkRunner.Run<ForeachStringBenchmark>();
