using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public int maxObject = 30;
    List<GameObject> Pool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pool = new List<GameObject>();

        for (int i = 0; i < maxObject; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            Pool.Add(obj);
        }

    }

    public GameObject Get()
    {

        foreach (GameObject obj in Pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

}
