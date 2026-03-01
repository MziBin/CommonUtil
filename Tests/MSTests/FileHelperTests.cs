using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonUtil.Tests.MSTests
{
    [TestClass]
    public class FileHelperTests
    {
        private readonly string _testDirectoryPath;
        private readonly string _testFilePath;
        private readonly string _testContent = "Hello, FileHelper!";
        private readonly string _testContentAppend = " Appended content";

        public FileHelperTests()
        {
            // 创建测试目录和文件
            _testDirectoryPath = Path.Combine(Path.GetTempPath(), "TestFileHelper");
            _testFilePath = Path.Combine(_testDirectoryPath, "test.txt");
            FileHelper.CreateDirectory(_testDirectoryPath);
            FileHelper.WriteText(_testFilePath, _testContent);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // 删除测试目录和文件
            if (Directory.Exists(_testDirectoryPath))
            {
                Directory.Delete(_testDirectoryPath, true);
            }
        }

        #region 文件读取与写入测试

        [TestMethod]
        public void ReadText_ShouldReturnCorrectContent()
        {
            // Act
            string content = FileHelper.ReadText(_testFilePath);

            // Assert
            Assert.AreEqual(_testContent, content);
        }

        [TestMethod]
        public void WriteText_ShouldWriteCorrectContent()
        {
            // Arrange
            string newContent = "New content";
            string testFilePath = Path.Combine(_testDirectoryPath, "write_test.txt");

            // Act
            FileHelper.WriteText(testFilePath, newContent);
            string content = FileHelper.ReadText(testFilePath);

            // Assert
            Assert.AreEqual(newContent, content);
        }

        [TestMethod]
        public void AppendText_ShouldAppendCorrectContent()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDirectoryPath, "append_test.txt");
            FileHelper.WriteText(testFilePath, _testContent);

            // Act
            FileHelper.AppendText(testFilePath, _testContentAppend);
            string content = FileHelper.ReadText(testFilePath);

            // Assert
            Assert.AreEqual(_testContent + _testContentAppend, content);
        }

        [TestMethod]
        public void ReadAllLines_ShouldReturnCorrectLines()
        {
            // Arrange
            List<string> testLines = new List<string> { "Line 1", "Line 2", "Line 3" };
            string testFilePath = Path.Combine(_testDirectoryPath, "lines_test.txt");
            FileHelper.WriteAllLines(testFilePath, testLines);

            // Act
            List<string> lines = FileHelper.ReadAllLines(testFilePath);

            // Assert
            CollectionAssert.AreEqual(testLines, lines);
        }

        [TestMethod]
        public void WriteAllLines_ShouldWriteCorrectLines()
        {
            // Arrange
            List<string> testLines = new List<string> { "Line 1", "Line 2", "Line 3" };
            string testFilePath = Path.Combine(_testDirectoryPath, "write_lines_test.txt");

            // Act
            FileHelper.WriteAllLines(testFilePath, testLines);
            List<string> lines = FileHelper.ReadAllLines(testFilePath);

            // Assert
            CollectionAssert.AreEqual(testLines, lines);
        }

        #endregion

        #region 文件复制与移动测试

        [TestMethod]
        public void CopyFile_ShouldCopyFileCorrectly()
        {
            // Arrange
            string destFilePath = Path.Combine(_testDirectoryPath, "copy_test.txt");

            // Act
            FileHelper.CopyFile(_testFilePath, destFilePath);

            // Assert
            Assert.IsTrue(File.Exists(destFilePath));
            Assert.AreEqual(FileHelper.ReadText(_testFilePath), FileHelper.ReadText(destFilePath));
        }

        [TestMethod]
        public void MoveFile_ShouldMoveFileCorrectly()
        {
            // Arrange
            string sourceFilePath = Path.Combine(_testDirectoryPath, "move_source.txt");
            string destFilePath = Path.Combine(_testDirectoryPath, "move_dest.txt");
            FileHelper.WriteText(sourceFilePath, _testContent);

            // Act
            FileHelper.MoveFile(sourceFilePath, destFilePath);

            // Assert
            Assert.IsFalse(File.Exists(sourceFilePath));
            Assert.IsTrue(File.Exists(destFilePath));
            Assert.AreEqual(_testContent, FileHelper.ReadText(destFilePath));
        }

        [TestMethod]
        public void DeleteFile_ShouldDeleteFile()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDirectoryPath, "delete_test.txt");
            FileHelper.WriteText(testFilePath, _testContent);

            // Act
            FileHelper.DeleteFile(testFilePath);

            // Assert
            Assert.IsFalse(File.Exists(testFilePath));
        }

        #endregion

        #region 文件信息测试

        [TestMethod]
        public void FileExists_ShouldReturnTrueForExistingFile()
        {
            // Act
            bool exists = FileHelper.FileExists(_testFilePath);

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void FileExists_ShouldReturnFalseForNonExistingFile()
        {
            // Arrange
            string nonExistingFilePath = Path.Combine(_testDirectoryPath, "non_existing.txt");

            // Act
            bool exists = FileHelper.FileExists(nonExistingFilePath);

            // Assert
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void GetFileSize_ShouldReturnCorrectSize()
        {
            // Arrange
            long expectedSize = Encoding.UTF8.GetByteCount(_testContent);

            // Act
            long size = FileHelper.GetFileSize(_testFilePath);

            // Assert
            Assert.AreEqual(expectedSize, size);
        }

        [TestMethod]
        public void GetFileExtension_ShouldReturnCorrectExtension()
        {
            // Act
            string extension = FileHelper.GetFileExtension(_testFilePath);

            // Assert
            Assert.AreEqual("txt", extension);
        }

        [TestMethod]
        public void GetFileName_ShouldReturnCorrectFileName()
        {
            // Act
            string fileName = FileHelper.GetFileName(_testFilePath);

            // Assert
            Assert.AreEqual("test.txt", fileName);
        }

        [TestMethod]
        public void GetFileNameWithoutExtension_ShouldReturnCorrectFileName()
        {
            // Act
            string fileName = FileHelper.GetFileNameWithoutExtension(_testFilePath);

            // Assert
            Assert.AreEqual("test", fileName);
        }

        #endregion

        #region 目录操作测试

        [TestMethod]
        public void DirectoryExists_ShouldReturnTrueForExistingDirectory()
        {
            // Act
            bool exists = FileHelper.DirectoryExists(_testDirectoryPath);

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DirectoryExists_ShouldReturnFalseForNonExistingDirectory()
        {
            // Arrange
            string nonExistingDirPath = Path.Combine(_testDirectoryPath, "non_existing_dir");

            // Act
            bool exists = FileHelper.DirectoryExists(nonExistingDirPath);

            // Assert
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void CreateDirectory_ShouldCreateDirectory()
        {
            // Arrange
            string newDirPath = Path.Combine(_testDirectoryPath, "new_dir");

            // Act
            FileHelper.CreateDirectory(newDirPath);

            // Assert
            Assert.IsTrue(Directory.Exists(newDirPath));
        }

        [TestMethod]
        public void DeleteDirectory_ShouldDeleteDirectory()
        {
            // Arrange
            string dirToDelete = Path.Combine(_testDirectoryPath, "dir_to_delete");
            FileHelper.CreateDirectory(dirToDelete);
            string filePath = Path.Combine(dirToDelete, "test.txt");
            FileHelper.WriteText(filePath, _testContent);

            // Act
            FileHelper.DeleteDirectory(dirToDelete);

            // Assert
            Assert.IsFalse(Directory.Exists(dirToDelete));
        }

        [TestMethod]
        public void GetFiles_ShouldReturnCorrectFiles()
        {
            // Arrange
            string subDir = Path.Combine(_testDirectoryPath, "subdir");
            FileHelper.CreateDirectory(subDir);
            FileHelper.WriteText(Path.Combine(subDir, "file1.txt"), "content1");
            FileHelper.WriteText(Path.Combine(_testDirectoryPath, "file2.txt"), "content2");

            // Act
            List<string> files = FileHelper.GetFiles(_testDirectoryPath, "*.txt", SearchOption.AllDirectories);

            // Assert
            Assert.IsTrue(files.Count >= 2);
        }

        [TestMethod]
        public void GetDirectories_ShouldReturnCorrectDirectories()
        {
            // Arrange
            string subDir1 = Path.Combine(_testDirectoryPath, "subdir1");
            string subDir2 = Path.Combine(_testDirectoryPath, "subdir2");
            FileHelper.CreateDirectory(subDir1);
            FileHelper.CreateDirectory(subDir2);

            // Act
            List<string> directories = FileHelper.GetDirectories(_testDirectoryPath);

            // Assert
            Assert.IsTrue(directories.Count >= 2);
        }

        #endregion

        #region 路径操作测试

        [TestMethod]
        public void CombinePaths_ShouldCombineCorrectly()
        {
            // Arrange
            string expectedPath = Path.Combine(_testDirectoryPath, "subdir", "file.txt");

            // Act
            string combinedPath = FileHelper.CombinePaths(_testDirectoryPath, "subdir", "file.txt");

            // Assert
            Assert.AreEqual(expectedPath, combinedPath);
        }

        [TestMethod]
        public void GetAbsolutePath_ShouldReturnCorrectPath()
        {
            // Arrange
            string relativePath = "./test.txt";
            string expectedPath = Path.GetFullPath(relativePath);

            // Act
            string absolutePath = FileHelper.GetAbsolutePath(relativePath);

            // Assert
            Assert.AreEqual(expectedPath, absolutePath);
        }

        [TestMethod]
        public void GetTempDirectory_ShouldReturnTempPath()
        {
            // Arrange
            string expectedTempPath = Path.GetTempPath();

            // Act
            string tempPath = FileHelper.GetTempDirectory();

            // Assert
            Assert.AreEqual(expectedTempPath, tempPath);
        }

        [TestMethod]
        public void GetTempFilePath_ShouldReturnValidPath()
        {
            // Act
            string tempFilePath = FileHelper.GetTempFilePath(".txt");

            // Assert
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(tempFilePath.EndsWith(".txt"));

            // Cleanup
            File.Delete(tempFilePath);
        }

        #endregion
    }
}