using System.Collections;

namespace AFSMath.Sequences
{
    /// <summary>
    /// Extended dynamic array with more comfortable work over array
    /// <br/>
    /// Not thread safe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnhancedArray<T> : IEnumerable<T>  where T : struct, IComparable<T>
    {

        // I just made this class for my more comfortable work with arrays.
        // It can apply arithmetic operations over sequence.
        // And also between array and other array (but only if they equals lengths).

        // Actually is just nothing, but a wrapper over List<T>.
        // But much comfortable for work with data.

        // But it only for CTS(.net) numeric types. And no others.

        private List<T> _values;

        public int Capacity 
        {
            get { return _values.Count; }
            set
            {
                _values.Capacity = value;
            }
        }


        public int Length
        {
            get { return _values.Count; }
        }

        /// <summary>
        /// Initialize an empty array
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public EnhancedArray() 
        {
            if (!IsGenericAllowedType())
                VectorThrowHelper.ThrowIfGenericNotAllowed(true);

            _values = new List<T>();
        }


        /// <summary>
        /// Initialize array with initial capacity of elements
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentException"></exception>
        public EnhancedArray(int capacity) : this()
        {
            if (capacity < 0) VectorThrowHelper.ThrowIfCapacityNegative(capacity);

            _values.Capacity = capacity;
        }


        /// <summary>
        /// Initialize array by initial array
        /// </summary>
        /// <param name="initValues"></param>
        /// <exception cref="NullReferenceException"></exception>
        public EnhancedArray(T[] initValues) : this(initValues.Length)
        {
            if (initValues == null) VectorThrowHelper.ThrowIfInitValuesNull(true);

            Array.ForEach(initValues, (v) => { _values.Add(v); });
        }


        /// <summary>
        /// Substract all elements by specified value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Substracted array elements</returns>
        public static EnhancedArray<T> operator -(EnhancedArray<T> array, T value)
        {
            EnhancedArray<T> newVector = new EnhancedArray<T>(array.Capacity);

            foreach (T v in array)
            {
                dynamic newVal = SubstractValues(v, value);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }



        /// <summary>
        /// Add all elements by specified value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Added array elements</returns>
        public static EnhancedArray<T> operator +(EnhancedArray<T> array, T value)
        {
            EnhancedArray<T> newVector = new EnhancedArray<T>(array.Capacity);

            foreach (T @__v_1 in array)
            {
                dynamic newVal = AddValues(@__v_1, value);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Multiply all elements by specified value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Multiplied array elements</returns>
        public static EnhancedArray<T> operator *(EnhancedArray<T> array, T value)
        {
            EnhancedArray<T> newVector = new EnhancedArray<T>(array.Capacity);

            foreach (T @__v_1 in array)
            {
                dynamic newVal = MultiplyValues(@__v_1, value);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Devide all elements by specified value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Devided array elements</returns>
        public static EnhancedArray<T> operator /(EnhancedArray<T> array, T value)
        {
            EnhancedArray<T> newVector = new EnhancedArray<T>(array.Capacity);

            foreach (T @__v_1 in array)
            {
                dynamic newVal = DevideValues(@__v_1, value);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Substract all elements by specified other elements array
        /// <br>array2 substracts from array1</br>
        /// <br>lengths of 2 arrays must be equals</br>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Substracted array elements</returns>
        public static EnhancedArray<T> operator -(EnhancedArray<T> array1, EnhancedArray<T> array2)
        {
            VectorThrowHelper.ThrowIfVectorsLengthsNotEquals(array1.Length, array2.Length);

            EnhancedArray<T> newVector = new EnhancedArray<T>(array1.Capacity);

            for (int i = 0; i < array1.Length; i++)
            {
                dynamic newVal = SubstractValues(array1[i], array2[i]);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Add all elements by specified other elements array
        /// <br>array2 adds to array1</br>
        /// <br>lengths of 2 arrays must be equals</br>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Added array elements</returns>
        public static EnhancedArray<T> operator +(EnhancedArray<T> array1, EnhancedArray<T> array2)
        {
            VectorThrowHelper.ThrowIfVectorsLengthsNotEquals(array1.Length, array2.Length);

            EnhancedArray<T> newVector = new EnhancedArray<T>(array1.Capacity);

            for (int i = 0; i < array1.Length; i++)
            {
                dynamic newVal = AddValues(array1[i], array2[i]);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Multiply all elements by specified other elements array
        /// <br>lengths of 2 arrays must be equals</br>
        /// <br>array1 values multiplies to array2 values</br>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Multiplied array elements</returns>
        public static EnhancedArray<T> operator *(EnhancedArray<T> array1, EnhancedArray<T> array2)
        {
            VectorThrowHelper.ThrowIfVectorsLengthsNotEquals(array1.Length, array2.Length);

            EnhancedArray<T> newVector = new EnhancedArray<T>(array1.Capacity);

            for (int i = 0; i < array1.Length; i++)
            {
                dynamic newVal = MultiplyValues(array1[i], array2[i]);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Devide all elements by specified other elements array
        /// <br>lengths of 2 arrays must be equals</br>
        /// <br>array1 devides by values of array2</br>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Multiplied array elements</returns>
        public static EnhancedArray<T> operator /(EnhancedArray<T> array1, EnhancedArray<T> array2)
        {
            VectorThrowHelper.ThrowIfVectorsLengthsNotEquals(array1.Length, array2.Length);

            EnhancedArray<T> newVector = new EnhancedArray<T>(array1.Capacity);

            for (int i = 0; i < array1.Length; i++)
            {
                dynamic newVal = DevideValues(array1[i], array2[i]);
                newVector._values.Add((T)newVal);
            }

            return newVector;
        }


        /// <summary>
        /// Gets or sets element in array by specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }


        /// <summary>
        /// Merge current array instance with other instance.
        /// </summary>
        /// <param name="other"></param>
        public void MergeWithOther(EnhancedArray<T> other)
        {
            _values.AddRange(other._values);
        }


        /// <summary>
        /// Standart quick sort algorithm
        /// </summary>
        public void QuickSort()
        {
            QuickSortHelper.QuickSortInternal(_values, 0, Length - 1);
        }


        /// <summary>
        /// Can be useful if needs original sequence of <see cref="EnhancedArray{T}"/>
        /// <br>And for work about original List. As sort, extentions, linq operations. etc..</br>
        /// </summary>
        /// <returns><see cref="List{T}"/> that was wrapped in this class</returns>
        public List<T> GetWrappedSequence()
        {
            return _values;
        }


        /// <summary>
        /// Add an object to end of array
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            _values.Add(value); 
        }


        /// <summary>
        /// Remove specified object from array
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            _values.Remove(value);
        }


        /// <summary>
        /// Remove item by index from array
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }


        /// <summary>
        /// Check if given generic type is allowed. Like numeric type
        /// <br>Like int, double, short, float, etc...</br>
        /// </summary>
        /// <returns></returns>
        private bool IsGenericAllowedType()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Byte:     case TypeCode.SByte:
                case TypeCode.UInt16:   case TypeCode.UInt32:
                case TypeCode.UInt64:   case TypeCode.Int16:
                case TypeCode.Int32:    case TypeCode.Int64:
                case TypeCode.Decimal:  case TypeCode.Double:
                case TypeCode.Single:
                    return true;

                default:
                    return false;
            }
        }


        private static T AddValues(T value1, T value2)
        {
            dynamic val1Copy = value1;
            dynamic val2Copy = value2;

            unchecked
            {
                return val1Copy + val2Copy;
            }
        }


        private static T SubstractValues(T value1, T value2)
        {
            dynamic val1Copy = value1;
            dynamic val2Copy = value2;

            unchecked
            {
                return val1Copy - val2Copy;
            }
        }


        private static T MultiplyValues(T value1, T value2)
        {
            dynamic val1Copy = value1;
            dynamic val2Copy = value2;

            unchecked
            {
                return val1Copy * val2Copy;
            }
        }


        private static T DevideValues(T value1, T value2)
        {
            dynamic val1Copy = value1;
            dynamic val2Copy = value2;

            if (val2Copy == 0)
                return val1Copy;

            unchecked
            {
                return val1Copy / val2Copy;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_values).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_values).GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";
            foreach (T value in _values)
            {
                str += value.ToString() + ' ';
            }
            return str;
        }
    }
}
