using DataContainers.Runtime;

namespace DataContainers.Tests.Editor
{
    public class TestItem : IId
    {
        public int Value { get; set; }
        public ulong Id { get; set; }
    }
}