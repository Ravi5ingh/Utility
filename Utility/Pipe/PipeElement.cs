namespace Utility.Pipe
{
    public class PipeElement<T>
    {
        public T Value { get; set; }

        public PipeElement<T> LeftNeighbor { get; set; }

        public PipeElement<T> RightNeighbor { get; set; }

        public PipeElement(T value)
        {
            Value = value;
        }

    }
}