using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{

    /// <summary>
    /// 游戏内资源读取类型
    /// </summary>
    public static ResLoadLocation m_gameLoadType = ResLoadLocation.Resource; //默认从resourcePath中读取


    public static T Load<T>(string name) where T : UnityEngine.Object
    {
        //ResourcesConfig packData = ResourcesConfigManager.GetBundleConfig(name);

        //if (packData == null)
        //{
        //    throw new Exception("Load Exception not find " + name);
        //}

        if (m_gameLoadType == ResLoadLocation.Resource)
        {
            return Resources.Load<T>(name);
        }
        else
        {
            //return AssetsBundleManager.Load<T>(name);
            return null;
        }
    }

}



public enum ResLoadLocation
{
    Resource,
    Streaming,
    Persistent,
    Catch,
}
