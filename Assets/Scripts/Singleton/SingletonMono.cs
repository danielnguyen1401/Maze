using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _singleton;

    public static bool IsInstanceValid()
    {
        return _singleton != null;
    }

    private void Reset()
    {
        gameObject.name = typeof(T).Name;
    }

    public static T Instance
    {
        get
        {
            if (_singleton != null) return _singleton;
            _singleton = (T) FindObjectOfType(typeof(T));
            if (_singleton != null) return _singleton;
            var obj = new GameObject {name = typeof(T).Name};
            _singleton = obj.AddComponent<T>();

            return _singleton;
        }
    }
}