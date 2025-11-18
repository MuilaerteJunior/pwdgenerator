using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class ExceptionTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Generate_NullConfig_ThrowsArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => Core.PwdGenerator.Generate(null));

        }

        [Test]
        public void Generate_LengthLessOrEqualZero_ThrowsNotSupportedException()
        {
            // Arrange
            var config = new ConfigModel
            {
                Length = 0,
                UppercaseCount = 0,
                SpecialCharsCount = 0,
                NumbersCount = 0
            };

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => Core.PwdGenerator.Generate(config));
        }

        [Test]
        public void Generate_UppercaseCountGreaterThanLength_ThrowsNotSupportedException()
        {
            // Arrange
            var config = new ConfigModel
            {
                Length = 3,
                UppercaseCount = 4, // > Length
                SpecialCharsCount = 0,
                NumbersCount = 0
            };

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => Core.PwdGenerator.Generate(config));
        }

        [Test]
        public void Generate_SpecialCharsCountGreaterThanLength_ThrowsNotSupportedException()
        {
            // Arrange
            var config = new ConfigModel
            {
                Length = 2,
                UppercaseCount = 0,
                SpecialCharsCount = 5, // > Length
                NumbersCount = 0
            };

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => Core.PwdGenerator.Generate(config));
        }

        [Test]
        public void Generate_NumbersCountGreaterThanLength_ThrowsNotSupportedException()
        {
            // Arrange
            var config = new ConfigModel
            {
                Length = 1,
                UppercaseCount = 0,
                SpecialCharsCount = 0,
                NumbersCount = 2 // > Length
            };

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => Core.PwdGenerator.Generate(config));
        }

        [Test]
        public void Generate_SumOfOptionsGreaterThanLength_ThrowsNotSupportedExceptionWithExpectedMessage()
        {
            // Arrange
            var config = new ConfigModel
            {
                Length = 5,
                UppercaseCount = 2,
                SpecialCharsCount = 2,
                NumbersCount = 2 // sum = 6 > Length
            };

            // Act & Assert
            var ex = Assert.Throws<NotSupportedException>(() => Core.PwdGenerator.Generate(config));
            Assert.That("Options defined are higher than password length!".Equals(ex.Message));
        }

    }
}
