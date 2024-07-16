using System;
using System.Collections.Generic;

public abstract class Activity
{
    protected DateTime _date;
    protected int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {_GetType()} ({_minutes} min) - Distance {GetDistance():F1} {_GetUnit()}, Speed {GetSpeed():F1} {_GetSpeedUnit()}, Pace: {GetPace():F2} min per {_GetUnit()}";
    }

    protected abstract string _GetType();
    protected abstract string _GetUnit();
    protected abstract string _GetSpeedUnit();
}

public class RunningActivity : Activity
{
    private double _distance;

    public RunningActivity(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / _minutes) * 60;
    }

    public override double GetPace()
    {
        return _minutes / _distance;
    }

    protected override string _GetType()
    {
        return "Running";
    }

    protected override string _GetUnit()
    {
        return "miles";
    }

    protected override string _GetSpeedUnit()
    {
        return "mph";
    }
}

public class CyclingActivity : Activity
{
    private double _speed;

    public CyclingActivity(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed / 60) * _minutes;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }

    protected override string _GetType()
    {
        return "Cycling";
    }

    protected override string _GetUnit()
    {
        return "miles";
    }

    protected override string _GetSpeedUnit()
    {
        return "mph";
    }
}

public class SwimmingActivity : Activity
{
    private int _laps;

    public SwimmingActivity(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return (_laps * 50) / 1000;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / _minutes) * 60;
    }

    public override double GetPace()
    {
        return _minutes / GetDistance();
    }

    protected override string _GetType()
    {
        return "Swimming";
    }

    protected override string _GetUnit()
    {
        return "km";
    }

    protected override string _GetSpeedUnit()
    {
        return "kph";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new RunningActivity(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new CyclingActivity(new DateTime(2022, 11, 3), 30, 6.0));
        activities.Add(new SwimmingActivity(new DateTime(2022, 11, 3), 30, 20));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}