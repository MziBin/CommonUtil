using CommonUtil.DeepCopy.Implement;
using CommonUtil.DeepCopy.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTests
{
    [TestClass]
    public class DeepCopyTests
    {
        [TestMethod]
        public void TextJsonDeepCopy_ReturnsNewInstanceWithSameValues()
        {
            IDeepCopy copier = new TextJsonDeepCopyImpl();
            var original = new TestObject { Id = 1, Name = "Test" };
            var copy = copier.DeepCopy(original);

            Assert.AreNotSame(original, copy);
            Assert.AreEqual(original.Id, copy.Id);
            Assert.AreEqual(original.Name, copy.Name);
        }

        [TestMethod]
        public void NewtonsoftDeepCopy_ReturnsNewInstanceWithSameValues()
        {
            IDeepCopy copier = new NewtonsoftDeepCopyImpl();
            var original = new TestObject { Id = 1, Name = "Test" };
            var copy = copier.DeepCopy(original);

            Assert.AreNotSame(original, copy);
            Assert.AreEqual(original.Id, copy.Id);
            Assert.AreEqual(original.Name, copy.Name);
        }

        [TestMethod]
        public void BinaryDeepCopy_ReturnsNewInstanceWithSameValues()
        {
            IDeepCopy copier = new BinaryDeepCopyImpl();
            var original = new TestObject { Id = 1, Name = "Test" };
            var copy = copier.DeepCopy(original);

            Assert.AreNotSame(original, copy);
            Assert.AreEqual(original.Id, copy.Id);
            Assert.AreEqual(original.Name, copy.Name);
        }

        [Serializable]
        public class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}