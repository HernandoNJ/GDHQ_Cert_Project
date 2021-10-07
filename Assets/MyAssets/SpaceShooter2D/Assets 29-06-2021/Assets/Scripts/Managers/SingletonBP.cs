using UnityEngine;
using static UnityEngine.Debug;

// Singleton Blue Print
public abstract class SingletonBP<T> : MonoBehaviour where T : SingletonBP<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) LogError($"{typeof(T).ToString()} is null");
            return instance;
        }
    }

    private void Awake()
    {
        instance = (T)this;
        Init();
    }

    protected virtual void Init() => Log($"{typeof(T).ToString()} initialized");
}
