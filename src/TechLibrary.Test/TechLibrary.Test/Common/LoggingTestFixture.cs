using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TechLibrary.Test.Common
{
    [ExcludeFromCodeCoverage]
    public class LoggingTestFixture
    {
        public static Mock<ILogger<T>> CreateMockLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();
            /*
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));
            mockLogger.Setup(logger => logger.LogWarning(It.IsAny<string>(), It.IsAny<object[]>()));
            mockLogger.Setup(logger => logger.LogError(It.IsAny<string>(), It.IsAny<object[]>()));
            */
            return mockLogger;
        }


    }
}
