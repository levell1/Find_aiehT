using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object 
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(path);
        if (prefab == null) return null;
        
        return Object.Instantiate(prefab, parent);
    }

}
