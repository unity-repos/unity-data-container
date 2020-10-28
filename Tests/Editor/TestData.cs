using DataContainers.Runtime;
using NUnit.Framework;

namespace DataContainers.Tests.Editor
{
    public class TestData
    {
        [Test]
        public void IncrementIds()
        {
            var container = new DataContainer<TestItem>();
            int length = 10;
            var a = new TestItem[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = new TestItem();
            }

            foreach (var item in a)
            {
                container.Add(item);
            }

            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(i + 1, a[i].Id);
            }
        }

        [Test]
        public void UpdateValue()
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

        [Test]
        public void ConserveIds()
        {
            var container = new DataContainer<TestItem>(true);

            var item1 = new TestItem {Id = 0};
            var item2 = new TestItem {Id = 1};
            var item3 = new TestItem {Id = 2};

            container.Add(new[] {item1, item2, item3});

            Assert.AreEqual(3, item1.Id);
            Assert.AreEqual(1, item2.Id);
            Assert.AreEqual(2, item3.Id);
        }
    }
}