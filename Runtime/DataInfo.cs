namespace DataContainers.Runtime
{
    public struct DataInfo<T> where T : IId, IRemove
    {
        public DataContainer<T> Container;
        public T Data;
    }
}