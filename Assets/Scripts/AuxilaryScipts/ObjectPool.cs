using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{

    #region Variables
    static Dictionary<Object, List<GameObject>> objectPool= new Dictionary<Object, List<GameObject>>();
    #endregion
    #region Monobehaviour callBacks
   
    #endregion
    #region Functions
    public   static GameObject Instantiate(Object prefab, Transform parent)
    {
        if (objectPool.ContainsKey(prefab))
        {

            for (int i = objectPool[prefab].Count - 1; i >= 0; i--)
            {
                if (objectPool[prefab][i] != null)
                {
                    if (!objectPool[prefab][i].activeInHierarchy)
                    {
                        objectPool[prefab][i].transform.SetParent(parent);
                        objectPool[prefab][i].transform.localPosition = Vector3.zero;
                        objectPool[prefab][i].transform.localRotation = Quaternion.identity;
                        objectPool[prefab][i].SetActive(true);
                        return objectPool[prefab][i];
                    }
                }
                else
                {
                    objectPool[prefab].RemoveAt(i);
                }
            }
            GameObject go =GameObject.Instantiate((GameObject)prefab, parent);
            objectPool[prefab].Add(go);
            return go;
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            GameObject go = GameObject.Instantiate((GameObject)prefab, parent);
            list.Add(go);
            objectPool.Add(prefab, list);
            return go;
        }
    }
    public  static GameObject Instantiate(Object prefab, Vector3 position, Quaternion rotation)
    {
        if (objectPool.ContainsKey(prefab))
        {

            for (int i = objectPool[prefab].Count - 1; i >= 0; i--)
            {
                if (objectPool[prefab][i] != null)
                {
                    if (!objectPool[prefab][i].activeInHierarchy)
                    {
                        objectPool[prefab][i].transform.SetParent(null);
                        objectPool[prefab][i].transform.position = position;
                        objectPool[prefab][i].transform.rotation = rotation;
                        objectPool[prefab][i].SetActive(true);
                        return objectPool[prefab][i];
                    }
                }
                else
                {
                    objectPool[prefab].RemoveAt(i);
                }
            }
            GameObject go = GameObject.Instantiate((GameObject)prefab, position, rotation);
            objectPool[prefab].Add(go);
            return go;
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            GameObject go = GameObject.Instantiate((GameObject)prefab, position, rotation);
            list.Add(go);
            objectPool.Add(prefab, list);
            return go;
        }
    }

    public static void preWarmPool(Object prefab, int instanceCount)
    {
        if (objectPool.ContainsKey(prefab))
        {
            if (objectPool[prefab].Capacity > instanceCount)
                return;
            else
            {
                objectPool[prefab].Capacity = instanceCount;
                for (int i = objectPool[prefab].Count - 1; i >= 0; i++)
                {
                    if (objectPool[prefab][i] == null)
                    {
                        objectPool[prefab].RemoveAt(i);
                    }
                }
                for (int i = objectPool[prefab].Count - 1; i < instanceCount; i++)
                {
                    GameObject go = GameObject.Instantiate((GameObject)prefab,null);
                    go.SetActive(false);
                    objectPool[prefab].Add(go);
                }
            }
        }
        else
        {
            List<GameObject> objs = new List<GameObject>(instanceCount);
            for (int i = 0; i < instanceCount; i++)
            {
                GameObject go = GameObject.Instantiate((GameObject)prefab,null);
                go.SetActive(false);
                objs.Add(go);
            }
            objectPool.Add(prefab,objs);
        }
    }

    public void ClearPool()
    {
        objectPool.Clear();
    }
    #endregion
}
