using FluentAssertions;

namespace SystemModelling;

public static class IntervalExtensions
{
    public static void PrintIntervals(this IEnumerable<Interval> intervals)
    {
        foreach (var interval in intervals)
        {
            Console.WriteLine($"{interval.Start} {interval.End} {interval.ElementsCount}");
        }
    }

    public static List<Interval> UniteSmallIntervals(this List<Interval> intervals, int minElementsCountInInterval = 5)
    {
        for (int i = 0; i < intervals.Count; i++)
        {
            if (intervals[i].ElementsCount < minElementsCountInInterval)
            {
                if (i == 0)
                {
                    UniteWithRight(i);
                }
                else if (i == intervals.Count - 1)
                {
                    UniteWithLeft(i);
                }
                else if (intervals[i - 1].ElementsCount < intervals[i + 1].ElementsCount)
                {
                    UniteWithLeft(i);
                }
                else
                {
                    UniteWithRight(i);
                }

                i -= 1;
            }
        }

        return intervals;

        void UniteWithRight(int i)
        {
            var unitedInterval = intervals[i].Unite(intervals[i + 1]);
            intervals.Insert(i, unitedInterval);
            intervals.RemoveAt(i + 1);
            intervals.RemoveAt(i + 1);
        }

        void UniteWithLeft(int i)
        {
            var unitedInterval = intervals[i].Unite(intervals[i - 1]);
            intervals.Insert(i, unitedInterval);
            intervals.RemoveAt(i + 1);
            intervals.RemoveAt(i - 1);
        }
    }

    public static Interval Unite(this Interval interval, Interval otherInterval)
    {
        // var internalsAdjusted = interval.Start == otherInterval.End || interval.End == otherInterval.Start;

        var newInterval = new Interval(Math.Min(interval.Start, otherInterval.Start),
            Math.Max(interval.End, otherInterval.End));
        foreach (var value in interval.Values)
        {
            newInterval.Values.Add(value);
        }

        foreach (var value in otherInterval.Values)
        {
            newInterval.Values.Add(value);
        }

        return newInterval;
    }

    public static List<Interval> SplitIntoIntervals(this double[] values, int intervalsCount)
    {
        var intervals = GenerateIntervals(values, intervalsCount);

        foreach (var value in values)
        {
            AddValueToCorrespondingInterval(intervals, value);
        }

        CalculateTotalCountInIntervals(intervals).Should().Be(values.Length);

        return intervals;
    }

    private static int CalculateTotalCountInIntervals(List<Interval> intervals)
    {
        return intervals.Sum(interval => interval.ElementsCount);
    }

    private static List<Interval> GenerateIntervals(double[] values, int intervalsCount)
    {
        double min = values.Min();
        double max = values.Max();
        double intervalLength = (max - min) / intervalsCount;

        List<Interval> intervals = new List<Interval>();

        for (int i = 0; i < intervalsCount; i++)
        {
            var newInterval = new Interval(min + i * intervalLength, min + (i + 1) * intervalLength);
            intervals.Add(newInterval);
        }

        return intervals;
    }

    private static void AddValueToCorrespondingInterval(List<Interval> intervals, double value)
    {
        for (int i = 0; i < intervals.Count; i++)
        {
            if (intervals[i].Start <= value && value < intervals[i].End)
            {
                intervals[i].Values.Add(value);
                return;
            }
        }

        intervals[^1].Values.Add(value);
    }
}