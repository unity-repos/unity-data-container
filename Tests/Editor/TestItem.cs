using DataContainers.Runtime;

namespace DataContainers.Tests.Editor
{
    public class TestItem : IId, IRemove
    {
        public int Value { get; set; }
        public ulong Id { get; set; }
        public bool Remove(ulong id)
        {
            return false;
        }
    }
}