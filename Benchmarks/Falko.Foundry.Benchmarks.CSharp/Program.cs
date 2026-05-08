using BenchmarkDotNet.Running;
using Falko.Benchmarks;

BenchmarkRunner.Run<CompileTypeElementBenchmark>();
BenchmarkRunner.Run<CacheTypeElementBenchmark>();
