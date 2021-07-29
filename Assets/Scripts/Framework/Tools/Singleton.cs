using System;
using System.Reflection;

/// <summary>
/// C#泛型单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class
{
    protected static T instance;
    private static object initLock = new object();
    public static T GetInstance()
    {
        if (instance == null)
        {
            CreateInstance();
        }
        return instance;
    }
    /// <summary>
    /// 清除单例
    /// </summary>
    public static void Clear()
    {
        instance = null;
    }
    private static void CreateInstance()
    {
        lock (initLock)
        {
            if (instance == null)
            {
                Type t = typeof(T);
                ConstructorInfo[] ctors = t.GetConstructors();
                if (ctors.Length > 0)
                {
                    throw new InvalidOperationException(t.Name + " has other ctor");
                }
                instance = (T)Activator.CreateInstance(t, true);
            }
        }
    }
}