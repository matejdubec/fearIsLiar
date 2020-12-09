using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public abstract class CSingleton<T> : MonoBehaviour where T : CSingleton<T>
{
    private static T instance;

    // Singleton je vyinstancovany, ale jeste nemusel prebehnut init
    public static bool Instantiated { get; private set; }
    // Singleton ma prevovalny Init
    public static bool IsInitialized { get; private set; }
    public static bool Destroyed { get; private set; }
    public static bool IsNull => (instance == null);

    public static T Instance
    {
        get
        {
            if (!Instantiated && !Destroyed)
                CreateInstance();
            return instance;
        }
    }

    protected virtual void Init() { }

    public static void CreateInstance()
    {
        if (Destroyed || Instantiated) return;
        Type type = typeof(T);
        T[] objects = FindObjectsOfType<T>();
        if (objects.Length > 0)
        {
            if (objects.Length > 1)
            {
                Debug.LogWarning($"There is more than one instance of Singleton of type \"{type}\". Keeping the first one. Destroying the others.");
                for (int i = 1; i < objects.Length; i++) Destroy(objects[i].gameObject);
            }
            instance = objects[0];
            instance.gameObject.SetActive(true);
            instance.OnInstanceCreated();
            Instantiated = true;
            SetDestroyed(false);
            instance.Init();
            IsInitialized = true;
            return;
        }

        GameObject gameObject;
        CSingletonAttribute attribute = Attribute.GetCustomAttribute(type, typeof(CSingletonAttribute)) as CSingletonAttribute;

        SetDestroyed(false);

        if (string.IsNullOrEmpty(attribute?.Name))
        {
            gameObject = new GameObject();
        }
        else
        {
            if (attribute.BanedSceneIndexes != null && attribute.BanedSceneIndexes.Contains(SceneManager.GetActiveScene().buildIndex)
            || attribute.BanedSceneNames != null && attribute.BanedSceneNames.Contains(SceneManager.GetActiveScene().name))
            {
                Debug.LogError("You are trying to create singleton in the banned scene. Abandoning");
                return;
            }
            string prefabName = attribute.Name;
            GameObject prefab = Resources.Load<GameObject>(prefabName);
            if (!prefab)
            {
                Debug.LogErrorFormat("No prefab on path: {0}", prefabName);
                return;
            }
            gameObject = Instantiate(prefab);
            if (gameObject == null)
                Debug.LogError($"Could not find Prefab \"{prefabName}\" on Resources for Singleton of type \"{type}\".");
        }

        gameObject.name = $"{(attribute != null && attribute.Persistant ? "#(SP) " : "#(S) ")}{type.Name}";

        if (instance == null)
            instance = gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

        Instantiated = true;
        instance.OnInstanceCreated();

        if (attribute.Persistant)
            DontDestroyOnLoad(instance.gameObject);

        instance.Init();
        IsInitialized = true;
    }

    public static void SetDestroyed(bool pState)
    {
        Destroyed = pState;
    }

    private void Awake()
    {
        if (instance && this.gameObject.GetInstanceID() != instance.gameObject.GetInstanceID())
        {
            Debug.LogError($"Duplicate {typeof(T)} - deleting myself!");
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnInstanceCreated()
    {

    }

    protected virtual void OnDestroy()
    {
        Instantiated = false;
        instance = null;
    }

    protected virtual void OnApplicationQuit()
    {
        SetDestroyed(true);
        Instantiated = false;
        instance = null;
    }
}