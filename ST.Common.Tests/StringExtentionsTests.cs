using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ST.Common.Tests
{
    [TestClass]
    public class StringExtentionsTests
    {
        [TestMethod]
        public void StringExtention_Truncate_TruncatedString()
        {
            // Arrange
            var testString = "Apple";
            var expectedResult = "App";

            // Act
            var result = testString.Truncate(3);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_OverTruncate_SameString()
        {
            // Arrange
            var testString = "Apple";
            var expectedResult = "Apple";

            // Act
            var result = testString.Truncate(10);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_TruncateUsingZeroValue_SameString()
        {
            // Arrange
            var testString = "Apple";
            var expectedResult = "Apple";

            // Act
            var result = testString.Truncate(0);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_TruncateUsingNegativeValue_SameString()
        {
            // Arrange
            var testString = "Apple";
            var expectedResult = "Apple";

            // Act
            var result = testString.Truncate(-3);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_TruncateEmptyString_EmptyString()
        {
            // Arrange
            var testString = "";
            var expectedResult = "";

            // Act
            var result = testString.Truncate(3);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_NormalizeWhitespace_StringWithoutWhiteSpaces()
        {
            // Arrange
            var testString = "This  is a  test      sentence!";
            var expectedResult = "This is a test sentence!";

            // Act
            var result = testString.NormalizeWhitespace();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void StringExtention_NormalizeWhitespaceUsingEmptyString_EmptyString()
        {
            // Arrange
            var testString = "";
            var expectedResult = "";

            // Act
            var result = testString.NormalizeWhitespace();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
