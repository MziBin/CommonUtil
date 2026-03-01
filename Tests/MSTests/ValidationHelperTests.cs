using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonUtil.Tests.MSTests
{
    [TestClass]
    public class ValidationHelperTests
    {
        #region 字符串验证测试

        [TestMethod]
        public void IsEmpty_ShouldReturnTrueForNull()
        {
            // Act
            bool result = ValidationHelper.IsEmpty(null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmpty_ShouldReturnTrueForEmptyString()
        {
            // Act
            bool result = ValidationHelper.IsEmpty(string.Empty);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmpty_ShouldReturnFalseForNonEmptyString()
        {
            // Act
            bool result = ValidationHelper.IsEmpty("test");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsBlank_ShouldReturnTrueForWhitespace()
        {
            // Act
            bool result = ValidationHelper.IsBlank("   ");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLengthInRange_ShouldReturnTrueForValidLength()
        {
            // Act
            bool result = ValidationHelper.IsLengthInRange("test", 2, 5);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region 格式验证测试

        [TestMethod]
        public void IsValidEmail_ShouldReturnTrueForValidEmail()
        {
            // Act
            bool result = ValidationHelper.IsValidEmail("test@example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidEmail_ShouldReturnFalseForInvalidEmail()
        {
            // Act
            bool result = ValidationHelper.IsValidEmail("test@example");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidUrl_ShouldReturnTrueForValidUrl()
        {
            // Act
            bool result = ValidationHelper.IsValidUrl("https://www.example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPhone_ShouldReturnTrueForValidPhone()
        {
            // Act
            bool result = ValidationHelper.IsValidPhone("13812345678");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidIdCard_ShouldReturnTrueForValidIdCard()
        {
            // Act
            bool result = ValidationHelper.IsValidIdCard("513021200009220896");

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region 数字验证测试

        [TestMethod]
        public void IsValidNumber_ShouldReturnTrueForValidNumber()
        {
            // Act
            bool result = ValidationHelper.IsValidNumber("123.45");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidInteger_ShouldReturnTrueForValidInteger()
        {
            // Act
            bool result = ValidationHelper.IsValidInteger("-123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPositiveNumber_ShouldReturnTrueForValidPositiveNumber()
        {
            // Act
            bool result = ValidationHelper.IsValidPositiveNumber("123.45");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNumberInRange_ShouldReturnTrueForNumberInRange()
        {
            // Act
            bool result = ValidationHelper.IsNumberInRange(10, 5, 15);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region 日期时间验证测试

        [TestMethod]
        public void IsValidDateTime_ShouldReturnTrueForValidDateTime()
        {
            // Act
            bool result = ValidationHelper.IsValidDateTime("2023-01-01 12:00:00");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDateInRange_ShouldReturnTrueForDateInRange()
        {
            // Arrange
            DateTime date = new DateTime(2023, 6, 15);
            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 12, 31);

            // Act
            bool result = ValidationHelper.IsDateInRange(date, startDate, endDate);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region 密码强度验证测试

        [TestMethod]
        public void IsWeakPassword_ShouldReturnTrueForShortPassword()
        {
            // Act
            bool result = ValidationHelper.IsWeakPassword("123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsMediumPassword_ShouldReturnTrueForMediumPassword()
        {
            // Act
            bool result = ValidationHelper.IsMediumPassword("Test123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsStrongPassword_ShouldReturnTrueForStrongPassword()
        {
            // Act
            bool result = ValidationHelper.IsStrongPassword("Test@123456789");

            // Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}