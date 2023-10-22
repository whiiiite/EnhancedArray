namespace AFSMath.Sequences
{
    internal static class VectorThrowHelper
    {
        private const string
                TYPE_NOT_VALID_EXC_MSG = "Given generic type is not numeric " +
            "or custom numeric type. " +
            "Only standart numeric types allowed";

        private const string CAPACITY_EXC_MSG =
            "Capacity must be positive";

        private const string INIT_VALUES_EXC_MSG =
            "Init values is null";


        public static void ThrowIfVectorsLengthsNotEquals(int length1, int length2)
        {
            if(length1 != length2)
            {
                throw new ArgumentException("Vectors lengths must be equals");
            }
        }


        public static void ThrowIfGenericNotAllowed(bool isNotAllowed)
        {
            if (isNotAllowed)
            {
                throw new ArgumentException(TYPE_NOT_VALID_EXC_MSG);
            }
        }


        public static void ThrowIfCapacityNegative(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException(CAPACITY_EXC_MSG);
            }
        }


        public static void ThrowIfInitValuesNull(bool isNull)
        {
            if (isNull)
            {
                throw new ArgumentException(INIT_VALUES_EXC_MSG);
            }
        }
    }
}
