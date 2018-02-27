using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIPoolManager
{


    //private static Transform m_poolParent;
    //public static Transform PoolParent
    //{
    //    get
    //    {
    //        if (m_poolParent == null)
    //        {
    //            GameObject instancePool = new GameObject("ObjectPool");
    //            m_poolParent = instancePool.transform;
    //            if (Application.isPlaying)
    //                GameObject.DontDestroyOnLoad(m_poolParent);
    //        }

    //        return m_poolParent;
    //    }
    //}



    //#region 旧版本对象池

    //static Dictionary<string, List<GameObject>> s_objectPool = new Dictionary<string, List<GameObject>>();

    ///// <summary>
    ///// 加载一个对象并把它实例化
    ///// </summary>
    ///// <param name="gameObjectName">对象名</param>
    ///// <param name="parent">对象的父节点,可空</param>
    ///// <returns></returns>
    //public static GameObject CreateGameObject(string gameObjectName, GameObject parent = null)
    //{
    //    GameObject goTmp = ResourcesManager.Load<GameObject>(gameObjectName);

    //    if (goTmp == null)
    //    {
    //        throw new Exception("CreateGameObject error dont find :" + gameObjectName);
    //    }

    //    return CreateGameObject(goTmp, parent);
    //}

    //public static GameObject CreateGameObject(GameObject prefab, GameObject parent = null)
    //{
    //    if (prefab == null)
    //    {
    //        throw new Exception("CreateGameObject error : l_prefab  is null");
    //    }

    //    GameObject instanceTmp = GameObject.Instantiate(prefab);
    //    instanceTmp.name = prefab.name;

    //    if (parent != null)
    //    {
    //        instanceTmp.transform.SetParent(parent.transform);
    //    }
    //    //l_instanceTmp.transform.localScale = Vector3.one;
    //    return instanceTmp;
    //}


    //public static bool IsExist(string objectName)
    //{
    //    if (objectName == null)
    //    {
    //        Debug.LogError("GameObjectManager objectName is null!");
    //        return false;
    //    }

    //    if (s_objectPool.ContainsKey(objectName) && s_objectPool[objectName].Count > 0)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    ///// <summary>
    ///// 从对象池取出一个对象，如果没有，则直接创建它
    ///// </summary>
    ///// <param name="name">对象名</param>
    ///// <param name="parent">要创建到的父节点</param>
    ///// <returns>返回这个对象</returns>
    //public static GameObject CreateGameObjectByPool(string name, GameObject parent = null, bool isSetActive = true)
    //{
    //    if (IsExist(name))
    //    {
    //        GameObject go = s_objectPool[name][0];
    //        s_objectPool[name].RemoveAt(0);

    //        if (isSetActive)
    //            go.SetActive(true);

    //        if (parent == null)
    //        {
    //            go.transform.SetParent(null);
    //        }
    //        else
    //        {
    //            go.transform.SetParent(parent.transform);
    //        }

    //        //go.transform.localScale = Vector3.one;

    //        return go;
    //    }
    //    else
    //    {
    //        return CreateGameObject(name, parent);
    //    }
    //}

    //public static GameObject CreateGameObjectByPool(GameObject prefab, GameObject parent = null, bool isSetActive = true)
    //{
    //    if (IsExist(prefab.name))
    //    {
    //        GameObject go = s_objectPool[prefab.name][0];
    //        s_objectPool[prefab.name].RemoveAt(0);

    //        if (isSetActive)
    //            go.SetActive(true);

    //        if (parent == null)
    //        {
    //            go.transform.SetParent(null);
    //        }
    //        else
    //        {
    //            go.transform.SetParent(parent.transform);
    //        }
    //        return go;
    //    }
    //    else
    //    {
    //        return CreateGameObject(prefab, parent);
    //    }
    //}

    ///// <summary>
    ///// 将一个对象放入对象池
    ///// </summary>
    ///// <param name="obj">目标对象</param>
    //public static void DestroyGameObjectByPool(GameObject obj, bool isSetActive = true)
    //{
    //    string key = obj.name.Replace("(Clone)", "");

    //    if (s_objectPool.ContainsKey(key) == false)
    //    {
    //        s_objectPool.Add(key, new List<GameObject>());
    //    }

    //    if (s_objectPool[key].Contains(obj))
    //    {
    //        throw new Exception("DestroyGameObjectByPool:-> Repeat Destroy GameObject !" + obj);
    //    }

    //    s_objectPool[key].Add(obj);

    //    if (isSetActive)
    //        obj.SetActive(false);
    //    else
    //    {
    //        obj.transform.position = Vector3.zero;
    //    }

    //    obj.name = key;
    //    obj.transform.SetParent(PoolParent);
    //}

    //public static void DestroyGameObjectByPool(GameObject go, float time)
    //{
    //    //Timer.DelayCallBack(time, (object[] obj) =>
    //    //{
    //    //    if (go != null)//应对调用过CleanPool()
    //    //        GameObject.DestroyGameObjectByPool(go);
    //    //});
    //}

    ///// <summary>
    ///// 清空对象池
    ///// </summary>
    //public static void CleanPool()
    //{
    //    foreach (string name in s_objectPool.Keys)
    //    {
    //        if (s_objectPool.ContainsKey(name))
    //        {
    //            List<GameObject> l_objList = s_objectPool[name];

    //            for (int i = 0; i < l_objList.Count; i++)
    //            {
    //                GameObject.Destroy(l_objList[i]);
    //            }

    //            l_objList.Clear();

    //        }
    //    }

    //    s_objectPool.Clear();
    //}

    ///// <summary>
    ///// 清除掉某一个对象的所有对象池缓存
    ///// </summary>
    //public static void CleanPoolByName(string name)
    //{
    //    Debug.Log("CleanPool :" + name);

    //    if (s_objectPool.ContainsKey(name))
    //    {
    //        List<GameObject> l_objList = s_objectPool[name];

    //        for (int i = 0; i < l_objList.Count; i++)
    //        {
    //            GameObject.Destroy(l_objList[i]);
    //        }

    //        l_objList.Clear();

    //        s_objectPool.Remove(name);
    //    }
    //}

    //#endregion
}
