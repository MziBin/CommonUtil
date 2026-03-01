using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace CommonUtil.Tests.MSTests
{
    [TestClass]
    public class CryptoHelperTests
    {
        private readonly string _testString = "Hello, World!";
        private readonly string _testFilePath;
        private readonly string _aesKey = "1234567890123456";
        private readonly string _aesIv = "1234567890123456";

        public CryptoHelperTests()
        {
            // 创建测试文件
            _testFilePath = Path.Combine(Path.GetTempPath(), "test_crypto.txt");
            File.WriteAllText(_testFilePath, _testString, Encoding.UTF8);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // 删除测试文件
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        #region MD5测试

        [TestMethod]
        public void Md5_ShouldReturnCorrectHash()
        {
            // Arrange
            string expected = "6cd3556deb0da54bca060b4c39479839";

            // Act
            string result = CryptoHelper.Md5(_testString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Md5_ShouldReturnCorrectHashForEmptyString()
        {
            // Arrange
            string expected = "d41d8cd98f00b204e9800998ecf8427e";

            // Act
            string result = CryptoHelper.Md5(string.Empty);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Md5File_ShouldReturnCorrectHash()
        {
            // Arrange
            string expected = CryptoHelper.Md5(_testString);

            // Act
            string result = CryptoHelper.Md5File(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region SHA测试

        [TestMethod]
        public void Sha1_ShouldReturnCorrectHash()
        {
            // Arrange
            string expected = "0a0a9f2a6772942557ab5355d76af442f8f65e01";

            // Act
            string result = CryptoHelper.Sha1(_testString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sha256_ShouldReturnCorrectHash()
        {
            // Arrange
            string expected = "dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f";

            // Act
            string result = CryptoHelper.Sha256(_testString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sha512_ShouldReturnCorrectHash()
        {
            // Arrange
            string expected = "374d794a95cdcfd8b35993185fef9ba368f160d8daf432d08ba9f1ed1e5abe6cc69291e0fa2fe0006a52570ef18c19def4e617c33ce52ef0a6e5fbe318cb03879";

            // Act
            string result = CryptoHelper.Sha512(_testString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region AES测试

        [TestMethod]
        public void AesEncrypt_ShouldReturnEncryptedString()
        {
            // Act
            string encrypted = CryptoHelper.AesEncrypt(_testString, _aesKey, _aesIv);

            // Assert
            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(_testString, encrypted);
        }

        [TestMethod]
        public void AesDecrypt_ShouldReturnOriginalString()
        {
            // Arrange
            string encrypted = CryptoHelper.AesEncrypt(_testString, _aesKey, _aesIv);

            // Act
            string decrypted = CryptoHelper.AesDecrypt(encrypted, _aesKey, _aesIv);

            // Assert
            Assert.AreEqual(_testString, decrypted);
        }

        [TestMethod]
        public void AesEncryptDecrypt_ShouldHandleEmptyString()
        {
            // Arrange
            string emptyString = string.Empty;

            // Act
            string encrypted = CryptoHelper.AesEncrypt(emptyString, _aesKey, _aesIv);
            string decrypted = CryptoHelper.AesDecrypt(encrypted, _aesKey, _aesIv);

            // Assert
            Assert.AreEqual(emptyString, decrypted);
        }

        #endregion

        #region Base64测试

        [TestMethod]
        public void Base64Encode_ShouldReturnCorrectBase64String()
        {
            // Arrange
            string expected = "SGVsbG8sIFdvcmxkIQ==";

            // Act
            string result = CryptoHelper.Base64Encode(_testString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Base64Decode_ShouldReturnOriginalString()
        {
            // Arrange
            string base64String = "SGVsbG8sIFdvcmxkIQ==";

            // Act
            string result = CryptoHelper.Base64Decode(base64String);

            // Assert
            Assert.AreEqual(_testString, result);
        }

        [TestMethod]
        public void Base64EncodeFile_ShouldReturnCorrectBase64String()
        {
            // Arrange
            string expected = CryptoHelper.Base64Encode(_testString);

            // Act
            string result = CryptoHelper.Base64EncodeFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Base64DecodeToFile_ShouldCreateCorrectFile()
        {
            // Arrange
            string base64String = CryptoHelper.Base64Encode(_testString);
            string outputFilePath = Path.Combine(Path.GetTempPath(), "test_decoded.txt");

            try
            {
                // Act
                CryptoHelper.Base64DecodeToFile(base64String, outputFilePath);

                // Assert
                Assert.IsTrue(File.Exists(outputFilePath));
                string content = File.ReadAllText(outputFilePath, Encoding.UTF8);
                Assert.AreEqual(_testString, content);
            }
            finally
            {
                // Cleanup
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
            }
        }

        #endregion
    }
}