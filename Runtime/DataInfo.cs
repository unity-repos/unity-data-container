namespace DataContainers.Runtime
{
    public struct DataInfo<T> where T : IId
    {
        public DataContainer<T> Container;
        public T Data;
    }
}