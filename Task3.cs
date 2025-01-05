public enum SortOrder
{
    Ascending,
    Descending
}

public enum SortAlgorithm
{
    Insertion, //Сортировка вставками
    Selection, //Сортировка выбором
    Heap, //Пирамидальная сортировка
    Quick, //Быстрая сортировка
    Merge //Сортировка слиянием
}

public static class ArraySortExtensions
{
    //Сортировка с внутренним правилом сравнения
    public static void Sort<T>(this T[] array, SortOrder order, SortAlgorithm algorithm)
        where T : IComparable<T>
    {
        Sort(array, order, algorithm, (x, y) => x.CompareTo(y));
    }

    //Сортировка с IComparer<T>
    public static void Sort<T>(this T[] array, SortOrder order, SortAlgorithm algorithm, IComparer<T> comparer)
    {
        Sort(array, order, algorithm, comparer.Compare);
    }

    //Сортировка с Comparer<T>
    public static void Sort<T>(this T[] array, SortOrder order, SortAlgorithm algorithm, Comparer<T> comparer)
    {
        Sort(array, order, algorithm, comparer.Compare);
    }

    //Сортировка с делегатом Comparison<T>
    public static void Sort<T>(this T[] array, SortOrder order, SortAlgorithm algorithm, Comparison<T> comparison)
    {
        if (order == SortOrder.Descending)
        {
            var originalComparison = comparison;
            comparison = (x, y) => originalComparison(y, x);
        }

        switch (algorithm)
        {
            case SortAlgorithm.Insertion:
                InsertionSort(array, comparison);
                break;
            case SortAlgorithm.Selection:
                SelectionSort(array, comparison);
                break;
            case SortAlgorithm.Heap:
                HeapSort(array, comparison);
                break;
            case SortAlgorithm.Quick:
                QuickSort(array, 0, array.Length - 1, comparison);
                break;
            case SortAlgorithm.Merge:
                var sortedArray = MergeSort(array, comparison);
                Array.Copy(sortedArray, array, sortedArray.Length);
                break;
            default:
                throw new ArgumentException("Неизвестный алгоритм сортировки");
        }
    }

    // Реализация алгоритмов сортировки

    private static void InsertionSort<T>(T[] array, Comparison<T> comparison)
    {
        for (int i = 1; i < array.Length; i++)
        {
            T key = array[i];
            int j = i - 1;
            while (j >= 0 && comparison(array[j], key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    private static void SelectionSort<T>(T[] array, Comparison<T> comparison)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (comparison(array[j], array[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
    }

    private static void HeapSort<T>(T[] array, Comparison<T> comparison)
    {
        void Heapify(int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && comparison(array[left], array[largest]) > 0)
            {
                largest = left;
            }

            if (right < n && comparison(array[right], array[largest]) > 0)
            {
                largest = right;
            }

            if (largest != i)
            {
                (array[i], array[largest]) = (array[largest], array[i]);
                Heapify(n, largest);
            }
        }

        for (int i = array.Length / 2 - 1; i >= 0; i--)
        {
            Heapify(array.Length, i);
        }

        for (int i = array.Length - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            Heapify(i, 0);
        }
    }

    private static void QuickSort<T>(T[] array, int low, int high, Comparison<T> comparison)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high, comparison);
            QuickSort(array, low, pivotIndex - 1, comparison);
            QuickSort(array, pivotIndex + 1, high, comparison);
        }
    }

    private static int Partition<T>(T[] array, int low, int high, Comparison<T> comparison)
    {
        T pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (comparison(array[j], pivot) < 0)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }

    private static T[] MergeSort<T>(T[] array, Comparison<T> comparison)
    {
        if (array.Length <= 1) return array;

        int mid = array.Length / 2;
        T[] left = MergeSort(array[..mid], comparison);
        T[] right = MergeSort(array[mid..], comparison);

        return Merge(left, right, comparison);
    }

    private static T[] Merge<T>(T[] left, T[] right, Comparison<T> comparison)
    {
        T[] result = new T[left.Length + right.Length];
        int i = 0, j = 0, k = 0;

        while (i < left.Length && j < right.Length)
        {
            if (comparison(left[i], right[j]) <= 0)
            {
                result[k++] = left[i++];
            }
            else
            {
                result[k++] = right[j++];
            }
        }

        while (i < left.Length) result[k++] = left[i++];
        while (j < right.Length) result[k++] = right[j++];

        return result;
    }
}

class Program3
{
    public static void Main3()
    {
        int[] originalArray = { 5, 3, 8, 1, 2 };
        
       
        Console.WriteLine("Оригинальный массив: " + string.Join(", ", originalArray));

        //Сортировка вставками
        TestSortingAlgorithm(originalArray, SortAlgorithm.Insertion);

        //Сортировка выбором
        TestSortingAlgorithm(originalArray, SortAlgorithm.Selection);

        //Пирамидальная сортировка
        TestSortingAlgorithm(originalArray, SortAlgorithm.Heap);

        //Быстрая сортировка
        TestSortingAlgorithm(originalArray, SortAlgorithm.Quick);

        //Сортировка слиянием
        TestSortingAlgorithm(originalArray, SortAlgorithm.Merge);
    }

    static void TestSortingAlgorithm(int[] originalArray, SortAlgorithm algorithm)
    {
        Console.WriteLine($"\n=== {algorithm} Sort ===");

        // Копируем массив, чтобы сортировки не влияли друг на друга
        int[] arrayAscending = (int[])originalArray.Clone();
        int[] arrayDescending = (int[])originalArray.Clone();
        
        
        arrayAscending.Sort(SortOrder.Ascending, algorithm);
        Console.WriteLine($"По возрастанию: {string.Join(", ", arrayAscending)}");
        
        
        arrayDescending.Sort(SortOrder.Descending, algorithm);
        Console.WriteLine($"По убыванию: {string.Join(", ", arrayDescending)}");
    }
}
