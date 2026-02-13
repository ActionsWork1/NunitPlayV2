//global using FluentAssertions;
global using NUnit;
using NUnit.Framework;
////running parallel fixtures
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]
[assembly: Timeout(120_000)]