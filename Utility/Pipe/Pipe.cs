using System;

namespace Utility.Pipe
{
    public class Pipe<T>
    {
        private PipeElement<T> leftElement;

        private PipeElement<T> rightElement;

        public void AddFromLeft(T value)
        {
            var newElement = new PipeElement<T>(value);
            
            if (IsPipeEmpty())
            {
                leftElement = newElement;
                rightElement = newElement;
                return;
            }

            leftElement.LeftNeighbor = newElement;
            newElement.RightNeighbor = leftElement;
            leftElement = newElement;
        }

        public void AddFromRight(T value)
        {
            var newElement = new PipeElement<T>(value);
            if (IsPipeEmpty())
            {
                leftElement = newElement;
                rightElement = newElement;
            }
            
            rightElement.RightNeighbor = newElement;
            newElement.LeftNeighbor = rightElement;
            rightElement = newElement;
        }

        public T TakeFromLeft()
        {
            if (TryTakeEdgeCase(out var returnValue))
            {
                return returnValue;
            }

            var retVal = leftElement.Value;
            leftElement = leftElement.RightNeighbor;

            return retVal;
        }

        public T TakeFromRight()
        {
            if(TryTakeEdgeCase(out var returnValue))
            {
                return returnValue;
            }

            var retVal = rightElement.Value;
            rightElement = rightElement.LeftNeighbor;

            return retVal;
        }

        private bool TryTakeEdgeCase(out T returnValue)
        {
            if (IsPipeEmpty())
            {
                throw new InvalidOperationException("Cannot take anymore. Pipe is empty");
            }
            if (leftElement == rightElement)
            {
                returnValue = leftElement.Value;
                leftElement = null;
                rightElement = null;
                return true;
            }

            returnValue = default(T);
            return false;
        }

        private bool IsPipeEmpty()
        {
            return leftElement == null && rightElement == null;
        }
    }
}