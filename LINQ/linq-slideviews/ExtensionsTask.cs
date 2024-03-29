﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
    /// <summary>
    /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
    /// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
    /// </summary>
    /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
    public static double Median(this IEnumerable<double> items)
    {
        var sortedItems = items.OrderBy(x => x).ToList();
        if (sortedItems.Count == 0)
            throw new InvalidOperationException();
        if (sortedItems.Count % 2 != 0)
            return sortedItems[sortedItems.Count / 2];
        else
            return (sortedItems[sortedItems.Count / 2 - 1] + sortedItems[sortedItems.Count / 2]) / 2;

    }

    /// <returns>
    /// Возвращает последовательность, состоящую из пар соседних элементов.
    /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
    /// </returns>
    public static IEnumerable<(T First, T second)> Bigrams<T>(this IEnumerable<T> items)
    {
        T firstElement = default(T);
        bool flag = true;
        foreach (var item in items)
            if (flag)
            {
                firstElement = item;
                flag = false;
            }
            else
            {
                yield return (firstElement, item);
                firstElement = item;
            }
    }
}