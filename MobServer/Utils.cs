using System;

/// <summary>
/// Singleton Helper Class
/// </summary>
public class Singleton<T> where T : class, new()
{
    public static T Instance { get; } = new T();

    protected Singleton()
    {
    }
}


public static class Clock
{
    public static long TickMS() => DateTime.Now.Ticks / 10000;
}