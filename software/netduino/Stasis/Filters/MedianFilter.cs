using System;
using Microsoft.SPOT;
using System.Collections;

namespace Stasis.Software.Netduino
{
    public class MedianFilter
    {
        /// <summary>
        /// Gets the window size for the median filter
        /// </summary>
        public int Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the median value.
        /// </summary>
        public double Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of values currently in the filter
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Values queue
        /// </summary>
        private Queue Values;

        /// <summary>
        /// Sorted values array
        /// </summary>
        private double[] SortedValues;

        /// <summary>
        /// Index of the median value
        /// </summary>
        private int ValueIndex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">Window size</param>
        public MedianFilter(int size)
        {
            this.Size = size;
            this.Values = new Queue();
            this.ValueIndex = FindMedianIndex();
        }

        /// <summary>
        /// Adds a value to the filter
        /// </summary>
        /// <param name="value">Newest value</param>
        public void AddValue(double value)
        {
            double[] temporaryValues = new double[this.Count];

            if (this.Count == this.Size)
            {
                this.Values.Dequeue();
                this.Count--;
            }

            this.Values.Enqueue(value);
            this.Count++;

            this.Values.CopyTo(temporaryValues, 0);
            this.SortedValues = QuickSort(temporaryValues, 0, Count - 1);

            this.Value = SortedValues[this.ValueIndex];
        }

        /// <summary>
        /// Find the center index of an array of size Count
        /// </summary>
        /// <param name=""></param>
        private int FindMedianIndex()
        {
            int index = (int)(this.Count / 2);

            if (this.Count % 2 == 0)
            {
                index -= 1;
            }

            return index;
        }

        /// <summary>
        /// Performs quick sort
        /// </summary>
        /// <param name="value"></param>
        private double[] QuickSort(double[] values, int start, int end)
        {
            if (start < end)
            {
                int pivot = Partition(values, start, end);
                values = QuickSort(values, start, pivot);
                values = QuickSort(values, pivot + 1, end);
            }
            return values;
        }

        /// <summary>
        /// Calculates paritions for the quick sort
        /// </summary>
        /// <param name="value"></param>
        private int Partition(double[] values, int left, int right)
        {
            double pivot = values[left];
            int start = left - 1;
            int end = right + 1;
            double temp = 0;
            while (true)
            {
                do
                {
                    end--;
                } while (values[end] > pivot);
                do
                {
                    start++;
                } while (values[start] < pivot);
                if (start < end)
                {
                    temp = values[start];
                    values[start] = values[end];
                    values[end] = temp;
                }
                else return end;
            }
        } 
    }
}
