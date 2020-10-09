using DataContainer.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace DataContainer.Tests.Editor
{
    public class TestData
    {
        [Test]
        public void BasicOperationsClass()
        {
            var container = new DataContainer<TestItem>();

            var item1 = new TestItem {Value = 5};
            var item2 = new TestItem {Value = 6};

            Assert.AreEqual(1, container.Add(item1));
            Assert.AreEqual(1, container.Count());

            Assert.AreEqual(2, container.Add(new TestItem()));
            Assert.AreEqual(2, container.Count());

            container.Update(1, item2);
            container.TryGet(1, out var item);

            Assert.AreEqual(6, item.Value);

            container.Remove(1);
            Assert.AreEqual(1, container.Count());
        }
    }
}