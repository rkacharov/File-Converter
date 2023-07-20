using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileConverter.Api.UnitTests
{
    internal class BaseTestFixture
    {
        protected static IServiceProvider ServiceProvider { get; }
        protected static ILoggerFactory LoggerFactory { get; }

        static BaseTestFixture()
        {
            ServiceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddDebug())
                .BuildServiceProvider();

            LoggerFactory = ServiceProvider.GetService<ILoggerFactory>() ?? 
                            throw new InvalidOperationException($"{nameof(ILoggerFactory)} is not registered in the DI container.");
        }
    }
}