namespace AFSMath.Sequences
{
    internal static class QuickSortHelper
    {
        public static void QuickSortInternal<T>(List<T> _values, int left, int right) where T : IComparable<T>
        {
            if (left >= right)
            {
                return;
            }

            int partition = PartitionInternal(_values, left, right);

            QuickSortInternal(_values, left, partition - 1);
            QuickSortInternal(_values, partition + 1, right);
        }

        public static int PartitionInternal<T>(List<T> _values, int left, int right) where T :IComparable<T>
        {
            T partition = _values[right];

            // stack items smaller than partition from left to right
            int swapIndex = left;
            for (int i = left; i < right; i++)
            {
                T item = _values[i];
                if (item.CompareTo(partition) <= 0)
                {
                    _values[i] = _values[swapIndex];
                    _values[swapIndex] = item;

                    swapIndex++;
                }
            }

            // put the partition after all the smaller items
            _values[right] = _values[swapIndex];
            _values[swapIndex] = partition;

            return right;
        }
    }
}
