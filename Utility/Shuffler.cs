using System;

namespace Utility
{
    /// <summary>
    /// Implementation of a lazy shuffling non-recurring algorithm
    /// </summary>
    /// <remarks>
    /// This takes in a from and to value, then lazily iterates through the range of values in random order with non-recurring values
    /// Time Complexity = O(n)
    /// Space Complexity = O(n)
    /// </remarks>
    public class Shuffler
    {
        #region Interface

        /// <summary>
        /// Checks to see if object has iterated through all ints or not
        /// </summary>
        public bool HasNext => maxIndex > -1;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="from">(Inclusive)</param>
        /// <param name="to">(Inclusive)</param>
        /// <param name="random">a random object</param>
        public Shuffler(int from, int to, Random random)
        {
            if (to < from)
            {
                throw new ArgumentException("from needs to be less than to");
            }
            gaps = new int[to - from + 1, 2];
            maxIndex = 0;
            SetGapAt(maxIndex, from, to);
            rand = random;
        }

        /// <summary>
        /// Get the next random non-recurring value within the provided range
        /// </summary>
        /// <remarks>
        /// The algorithm works by storing a set of gaps which represent ranges of numbers yet untouched.
        /// Eg. if the current range is (1->10), and 5 is selected as the return value, then there will be two
        /// new ranges ie (1->4) and (6->10) and (1->10) will be removed
        /// </remarks>
        /// <returns></returns>
        public int Next()
        {
            if (!HasNext)
            {
                throw new InvalidOperationException("Cannot call Next now. This object is dead. Must create a new one");
            }

            var gapInd = rand.Next(0, maxIndex + 1);

            var from = gaps[gapInd, 0];
            var to = gaps[gapInd, 1];

            int retVal;
            if (from == to)
            {
                retVal = from;
                SetGapAt(gapInd, gaps[maxIndex, 0], gaps[maxIndex, 1]);
                maxIndex--;
            }
            else
            {
                retVal = rand.Next(from, to + 1);
                var incMarker = false;
                if (retVal > from)
                {
                    SetGapAt(gapInd, from, retVal - 1);
                    incMarker = true;
                }
                if (retVal < to)
                {
                    SetGapAt(incMarker ? ++maxIndex : gapInd, retVal + 1, to);
                }
            }

            return retVal;
        }

        #endregion

        #region Private

        /// <summary>
        /// Randomizer
        /// </summary>
        private Random rand;

        /// <summary>
        /// Everything after this is ignored
        /// </summary>
        private int maxIndex;

        /// <summary>
        /// The cache of gaps
        /// </summary>
        private int[,] gaps;

        /// <summary>
        /// Set a gap at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void SetGapAt(int index, int from, int to)
        {
            gaps[index, 0] = from;
            gaps[index, 1] = to;
        }

        #endregion
    }
}