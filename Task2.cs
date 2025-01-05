public static class EnumerableExtensions
{
    private static void EnsureUnique<T>(T[] source, IEqualityComparer<T> comparer)
    {
        for (int i = 0; i < source.Length; i++)
        {
            for (int j = i + 1; j < source.Length; j++)
            {
                if (comparer.Equals(source[i], source[j]))
                {
                    throw new ArgumentException("Элементы входного перечисления должны быть уникальными.");
                }
            }
        }
    }

    //Сочетания с повторениями
    public static T[][] CombinationsWithRepetition<T>(this T[] source, int k, IEqualityComparer<T> comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        EnsureUnique(source, comparer);

        return GenerateCombinationsWithRepetition(source, k);
    }

    private static T[][] GenerateCombinationsWithRepetition<T>(T[] items, int k)
    {
        if (k == 0)
        {
            return new T[][] { new T[0] };
        }

        var results = new System.Collections.ArrayList();

        for (int i = 0; i < items.Length; i++)
        {
            var subResults = GenerateCombinationsWithRepetition(items[i..], k - 1);
            foreach (var sub in subResults)
            {
                var combination = new T[sub.Length + 1];
                combination[0] = items[i];
                Array.Copy(sub, 0, combination, 1, sub.Length);
                results.Add(combination);
            }
        }

        return (T[][])results.ToArray(typeof(T[]));
    }

    //Сочетания без повторений
    public static T[][] CombinationsWithoutRepetition<T>(this T[] source, int k, IEqualityComparer<T> comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        EnsureUnique(source, comparer);

        return GenerateCombinationsWithoutRepetition(source, k);
    }

    private static T[][] GenerateCombinationsWithoutRepetition<T>(T[] items, int k)
    {
        if (k == 0)
        {
            return new T[][] { new T[0] };
        }

        var results = new System.Collections.ArrayList();

        for (int i = 0; i < items.Length; i++)
        {
            var subResults = GenerateCombinationsWithoutRepetition(items[(i + 1)..], k - 1);
            foreach (var sub in subResults)
            {
                var combination = new T[sub.Length + 1];
                combination[0] = items[i];
                Array.Copy(sub, 0, combination, 1, sub.Length);
                results.Add(combination);
            }
        }

        return (T[][])results.ToArray(typeof(T[]));
    }

    //Подмножества
    public static T[][] Subsets<T>(this T[] source, IEqualityComparer<T> comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        EnsureUnique(source, comparer);

        return GenerateSubsets(source);
    }

    private static T[][] GenerateSubsets<T>(T[] items)
    {
        var results = new System.Collections.ArrayList { new T[0] };

        for (int i = 0; i < items.Length; i++)
        {
            var subsets = GenerateSubsets(items[(i + 1)..]);
            foreach (var subset in subsets)
            {
                var newSubset = new T[subset.Length + 1];
                newSubset[0] = items[i];
                Array.Copy(subset, 0, newSubset, 1, subset.Length);
                results.Add(newSubset);
            }
        }

        return (T[][])results.ToArray(typeof(T[]));
    }

    //Перестановки
    public static T[][] Permutations<T>(this T[] source, IEqualityComparer<T> comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        EnsureUnique(source, comparer);

        return GeneratePermutations(source);
    }

    private static T[][] GeneratePermutations<T>(T[] items)
    {
        if (items.Length == 0)
        {
            return new T[][] { new T[0] };
        }

        var results = new System.Collections.ArrayList();

        for (int i = 0; i < items.Length; i++)
        {
            var current = items[i];
            var remaining = new T[items.Length - 1];
            Array.Copy(items, 0, remaining, 0, i);
            Array.Copy(items, i + 1, remaining, i, items.Length - i - 1);

            var subPermutations = GeneratePermutations(remaining);
            foreach (var sub in subPermutations)
            {
                var permutation = new T[sub.Length + 1];
                permutation[0] = current;
                Array.Copy(sub, 0, permutation, 1, sub.Length);
                results.Add(permutation);
            }
        }

        return (T[][])results.ToArray(typeof(T[]));
    }
}

// Демонстрация работы
public class Program2
{
    public static void Main2()
    {
        try
        {
            var input = new int[] { 1, 2, 3 };

            Console.WriteLine("Сочетания с повторениями (k=2):");
            foreach (var combination in input.CombinationsWithRepetition(2))
            {
                Console.WriteLine($"[{string.Join(", ", combination)}]");
            }

            Console.WriteLine("\nСочетания без повторений (k=2):");
            foreach (var combination in input.CombinationsWithoutRepetition(2))
            {
                Console.WriteLine($"[{string.Join(", ", combination)}]");
            }

            Console.WriteLine("\nПодмножества:");
            foreach (var subset in input.Subsets())
            {
                Console.WriteLine($"[{string.Join(", ", subset)}]");
            }

            Console.WriteLine("\nПерестановки:");
            foreach (var permutation in input.Permutations())
            {
                Console.WriteLine($"[{string.Join(", ", permutation)}]");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
