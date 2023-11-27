using System.Collections;

namespace Application.InternalModels;

public record DateRange : IEnumerable<DateTime>
{
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }

    public IEnumerator<DateTime> GetEnumerator()
    {
        return new DateRangeEnumerator(StartDate, EndDate);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class DateRangeEnumerator : IEnumerator<DateTime>
{
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;
    private readonly TimeSpan _step;

    public DateRangeEnumerator(DateTime startDate, DateTime endDate, TimeSpan step)
    {
        _startDate = startDate;
        _endDate = endDate;
        _step = step;
        Current = startDate;
    }

    public DateRangeEnumerator(DateTime startDate, DateTime endDate) : this(startDate, endDate, TimeSpan.FromDays(1))
    {
        
    }

    public bool MoveNext()
    {
        if (Current >= _endDate) return false;
        Current = Current.Add(_step);
        return true;

    }

    public void Reset()
    {
        Current = _startDate;
    }

    object IEnumerator.Current => Current;

    public DateTime Current { get; private set; }

    public void Dispose()
    {
    }
}