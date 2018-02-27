using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using System.Text.RegularExpressions;
using EzUI;

//[RequireComponent(typeof(UIStackManager))]
//[RequireComponent(typeof(UILayerManager))]
//[RequireComponent(typeof(UIAnimManager))]
public class UIManager : MonoBehaviour
{

    public static GameObject m_UIManagerGo;
    public static Camera m_UIcamera;                    //UICamera

    public static UILayerManager m_UILayerManager;      //UI层级管理器
    public static UIAnimManager m_UIAnimManager;        //UI动画管理器
    public static UIStackManager m_UIStackManager;      //UI栈管理器


    public static Dictionary<string, List<UIWindowBase>> m_UIs = new Dictionary<string, List<UIWindowBase>>();      //打开的UI
    public static Dictionary<string, List<UIWindowBase>> m_hideUIs = new Dictionary<string, List<UIWindowBase>>();  //隐藏的UI



    #region 初始化

    public static void Init()
    {
        GameObject instance = GameObject.Find("UIManager");

        if (instance == null)
        {
            instance = ResourcesManager.Load<GameObject>("UIManager");
        }

        m_UIManagerGo = instance;

        m_UIcamera = instance.GetComponentInChildren<Camera>();

        m_UILayerManager = instance.GetComponent<UILayerManager>();
        m_UIAnimManager = instance.GetComponent<UIAnimManager>();
        m_UIStackManager = instance.GetComponent<UIStackManager>();

        DontDestroyOnLoad(instance);
    }

    #endregion



    //#region UI的打开与关闭方法

    /// <summary>
    /// 创建UI,如果不打开则存放在Hide列表中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateUIWindow<T>() where T : UIWindowBase
    {
        return (T)CreateUIWindow(typeof(T).Name);
    }

    public static UIWindowBase CreateUIWindow(string UIName)
    {
        GameObject UItmp = ResourcesManager.Load<GameObject>(UIName);
        //UIPoolManager.CreateGameObject(UIName, m_UIManagerGo);

        UIWindowBase UIbase = UItmp.GetComponent<UIWindowBase>();

        UISystemEvent.Dispatch(UIbase, UIEvent.OnInit);  //派发OnInit事件
        try
        {
            UIbase.Init(GetUIID(UIName));
        }
        catch (Exception e)
        {
            Debug.LogError("OnInit Exception: " + e.ToString());
        }

        AddHideUI(UIbase);

        m_UILayerManager.SetLayer(UIbase);      //设置层级

        return UIbase;
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="UIName">UI名</param>
    /// <param name="callback">动画播放完毕回调</param>
    /// <param name="objs">回调传参</param>`
    /// <returns>返回打开的UI</returns>
    public static UIWindowBase OpenUIWindow(string UIName, UICallBack callback = null, params object[] objs)
    {
        UIWindowBase UIbase = GetHideUI(UIName);

        if (UIbase == null)
        {
            UIbase = CreateUIWindow(UIName);
        }

        RemoveHideUI(UIbase);
        AddUI(UIbase);

        UISystemEvent.Dispatch(UIbase, UIEvent.OnOpen);  //派发OnOpen事件
        try
        {
            UIbase.OnUIOpen();
        }
        catch (Exception e)
        {
            Debug.LogError(UIName + " OnOpen Exception: " + e.ToString());
        }

        m_UIStackManager.OnUIOpen(UIbase);
        m_UILayerManager.SetLayer(UIbase);      //设置层级
        m_UIAnimManager.StartEnterAnim(UIbase, callback, objs); //播放动画
        return UIbase;
    }
    public static T OpenUIWindow<T>() where T : UIWindowBase
    {
        return (T)OpenUIWindow(typeof(T).Name);
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="UI">目标UI</param>
    /// <param name="isPlayAnim">是否播放关闭动画</param>
    /// <param name="callback">动画播放完毕回调</param>
    /// <param name="objs">回调传参</param>
    public static void CloseUIWindow(UIWindowBase UI, bool isPlayAnim = true, UICallBack callback = null, params object[] objs)
    {
        RemoveUI(UI);        //移除UI引用
        UI.RemoveAllListener();
        s_UILayerManager.RemoveUI(UI);

        if (isPlayAnim)
        {
            //动画播放完毕删除UI
            if (callback != null)
            {
                callback += CloseUIWindowCallBack;
            }
            else
            {
                callback = CloseUIWindowCallBack;
            }

            s_UIAnimManager.StartExitAnim(UI, callback, objs);
        }
        else
        {
            CloseUIWindowCallBack(UI, objs);
        }
    }
    //static void CloseUIWindowCallBack(UIWindowBase UI, params object[] objs)
    //{
    //    UISystemEvent.Dispatch(UI, UIEvent.OnClose);  //派发OnClose事件
    //    try
    //    {
    //        UI.OnClose();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(UI.UIName + " OnClose Exception: " + e.ToString());
    //    }

    //    s_UIStackManager.OnUIClose(UI);
    //    AddHideUI(UI);
    //}
    //public static void CloseUIWindow(string UIname, bool isPlayAnim = true, UICallBack callback = null, params object[] objs)
    //{
    //    UIWindowBase ui = GetUI(UIname);

    //    if (ui == null)
    //    {
    //        Debug.LogError("CloseUIWindow Error UI ->" + UIname + "<-  not Exist!");
    //    }
    //    else
    //    {
    //        CloseUIWindow(GetUI(UIname), isPlayAnim, callback, objs);
    //    }
    //}

    //public static void CloseUIWindow<T>(bool isPlayAnim = true, UICallBack callback = null, params object[] objs) where T : UIWindowBase
    //{
    //    CloseUIWindow(typeof(T).Name, isPlayAnim, callback, objs);
    //}

    //public static UIWindowBase ShowUI(string UIname)
    //{
    //    UIWindowBase ui = GetUI(UIname);
    //    return ShowUI(ui);
    //}

    //public static UIWindowBase ShowUI(UIWindowBase ui)
    //{
    //    UISystemEvent.Dispatch(ui, UIEvent.OnShow);  //派发OnShow事件
    //    try
    //    {
    //        ui.Show();
    //        ui.OnShow();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(ui.UIName + " OnShow Exception: " + e.ToString());
    //    }

    //    return ui;
    //}

    //public static UIWindowBase HideUI(string UIname)
    //{
    //    UIWindowBase ui = GetUI(UIname);
    //    return HideUI(ui);
    //}

    //public static UIWindowBase HideUI(UIWindowBase ui)
    //{
    //    UISystemEvent.Dispatch(ui, UIEvent.OnHide);  //派发OnHide事件

    //    try
    //    {
    //        ui.Hide();
    //        ui.OnHide();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(ui.UIName + " OnShow Exception: " + e.ToString());
    //    }

    //    return ui;
    //}

    //public static void HideOtherUI(string UIName)
    //{
    //    List<string> keys = new List<string>(s_UIs.Keys);
    //    for (int i = 0; i < keys.Count; i++)
    //    {
    //        List<UIWindowBase> list = s_UIs[keys[i]];
    //        for (int j = 0; j < list.Count; j++)
    //        {
    //            if (list[j].UIName != UIName)
    //            {
    //                HideUI(list[j]);
    //            }
    //        }
    //    }
    //}

    //public static void ShowOtherUI(string UIName)
    //{
    //    List<string> keys = new List<string>(s_UIs.Keys);
    //    for (int i = 0; i < keys.Count; i++)
    //    {
    //        List<UIWindowBase> list = s_UIs[keys[i]];
    //        for (int j = 0; j < list.Count; j++)
    //        {
    //            if (list[j].UIName != UIName)
    //            {
    //                ShowUI(list[j]);
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 移除全部UI
    ///// </summary>
    //public static void CloseAllUI(bool isPlayerAnim = false)
    //{
    //    List<string> keys = new List<string>(s_UIs.Keys);
    //    for (int i = 0; i < keys.Count; i++)
    //    {
    //        List<UIWindowBase> list = s_UIs[keys[i]];
    //        for (int j = 0; j < list.Count; j++)
    //        {
    //            CloseUIWindow(list[i], isPlayerAnim);
    //        }
    //    }
    //}

    //public static void CloseLastUI(UIType uiType = UIType.Normal)
    //{
    //    s_UIStackManager.CloseLastUIWindow(uiType);
    //}

    //#endregion



    //    #region UI的打开与关闭 异步方法

    //    public static void OpenUIAsync<T>( UICallBack callback, params object[] objs) where T : UIWindowBase
    //    {
    //        string UIName = typeof(T).Name;
    //        OpenUIAsync(UIName,callback,objs);
    //    }

    //    public static void OpenUIAsync(string UIName , UICallBack callback, params object[] objs)
    //    {
    //        ResourceManager.LoadAsync(UIName, (loadState,resObject) =>
    //         {
    //             if(loadState.isDone)
    //             {
    //                 OpenUIWindow(UIName, callback, objs);
    //             }
    //         });
    //    }

    //#endregion

    //    #region UI内存管理

    //    public static void DestroyUI(UIWindowBase UI)
    //    {
    //        Debug.Log("UIManager DestroyUI " + UI.name);

    //        if (GetIsExitsHide(UI))
    //        {
    //            RemoveHideUI(UI);
    //        }
    //        else if(GetIsExits(UI))
    //        {
    //            RemoveUI(UI);   
    //        }

    //        UISystemEvent.Dispatch(UI, UIEvent.OnDestroy);  //派发OnDestroy事件
    //        try
    //        {
    //            UI.DestroyUI();
    //        }
    //        catch(Exception e)
    //        {
    //            Debug.LogError("OnDestroy :" + e.ToString());
    //        }
    //        Destroy(UI.gameObject);
    //    }

    //    public static void DestroyAllUI()
    //    {
    //        DestroyAllActiveUI();
    //        DestroyAllHideUI();
    //    }

    //#endregion

    //    #region 打开UI列表的管理

    //    /// <summary>
    //    /// 删除所有打开的UI
    //    /// </summary>
    //    public static void DestroyAllActiveUI()
    //    {
    //        foreach (List<UIWindowBase> uis in s_UIs.Values)
    //        {
    //            for (int i = 0; i < uis.Count; i++)
    //            {
    //                UISystemEvent.Dispatch(uis[i], UIEvent.OnDestroy);  //派发OnDestroy事件
    //                try
    //                {
    //                    uis[i].DestroyUI();
    //                }
    //                catch (Exception e)
    //                {
    //                    Debug.LogError("OnDestroy :" + e.ToString());
    //                }
    //                Destroy(uis[i].gameObject);
    //            }
    //        }

    //        s_UIs.Clear();
    //    }

    //    public static T GetUI<T>() where T : UIWindowBase
    //    {
    //        return (T)GetUI(typeof(T).Name);
    //    }
    //    public static UIWindowBase GetUI(string UIname)
    //    {
    //        if (!s_UIs.ContainsKey(UIname))
    //        {
    //            //Debug.Log("!ContainsKey " + l_UIname);
    //            return null;
    //        }
    //        else
    //        {
    //            if (s_UIs[UIname].Count == 0)
    //            {
    //                //Debug.Log("s_UIs[UIname].Count == 0");
    //                return null;
    //            }
    //            else
    //            {
    //                //默认返回最后创建的那一个
    //                return s_UIs[UIname][s_UIs[UIname].Count - 1];
    //            }
    //        }
    //    }

    //    public static UIBase GetUIBaseByEventKey(string eventKey)
    //    {
    //        string UIkey = eventKey.Split('.')[0];
    //        string[] keyArray = UIkey.Split('_');

    //        string uiEventKey = "";

    //        UIBase uiTmp = null;
    //        for (int i = 0; i < keyArray.Length; i++)
    //        {
    //            if(i == 0)
    //            {
    //                uiEventKey = keyArray[0];
    //                uiTmp = GetUIWindowByEventKey(uiEventKey);
    //            }
    //            else
    //            {
    //                uiEventKey += "_" + keyArray[i];
    //                uiTmp = uiTmp.GetItemByKey(uiEventKey);
    //            }

    //            Debug.Log("uiEventKey " + uiEventKey);
    //        }

    //        return uiTmp;
    //    }

    //    static Regex uiKey = new Regex(@"(\S+)\d+");
    //    static UIWindowBase GetUIWindowByEventKey(string eventKey)
    //    {
    //        string UIname = uiKey.Match(eventKey).Groups[1].Value;

    //        if (!s_UIs.ContainsKey(UIname))
    //        {
    //            throw new Exception("UIManager: GetUIWindowByEventKey error dont find UI name: ->" + eventKey + "<-  " + UIname);
    //        }

    //        List<UIWindowBase> list = s_UIs[UIname];
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            if(list[i].UIEventKey == eventKey)
    //            {
    //                return list[i];
    //            }
    //        }

    //        throw new Exception("UIManager: GetUIWindowByEventKey error dont find UI name: ->" + eventKey + "<-  " + UIname);
    //    }

    static bool GetIsExits(UIWindowBase UI)
    {
        if (!m_UIs.ContainsKey(UI.name))
        {
            return false;
        }
        else
        {
            return m_UIs[UI.name].Contains(UI);
        }
    }

    static void AddUI(UIWindowBase UI)
    {
        if (!m_UIs.ContainsKey(UI.name))
        {
            m_UIs.Add(UI.name, new List<UIWindowBase>());
        }

        m_UIs[UI.name].Add(UI);

        UI.Show();
    }

    static void RemoveUI(UIWindowBase UI)
    {
        if (UI == null)
        {
            throw new Exception("UIManager: RemoveUI error l_UI is null: !");
        }

        if (!m_UIs.ContainsKey(UI.name))
        {
            throw new Exception("UIManager: RemoveUI error dont find UI name: ->" + UI.name + "<-  " + UI);
        }

        if (!m_UIs[UI.name].Contains(UI))
        {
            throw new Exception("UIManager: RemoveUI error dont find UI: ->" + UI.name + "<-  " + UI);
        }
        else
        {
            m_UIs[UI.name].Remove(UI);
        }
    }

    static int GetUIID(string UIname)
    {
        if (!m_UIs.ContainsKey(UIname))
        {
            return 0;
        }
        else
        {
            int id = m_UIs[UIname].Count;

            for (int i = 0; i < m_UIs[UIname].Count; i++)
            {
                if (m_UIs[UIname][i].UIID == id)
                {
                    id++;
                    i = 0;
                }
            }

            return id;
        }
    }

    //    public static int GetNormalUICount()
    //    {
    //        return s_UILayerManager.normalUIList.Count;
    //    }

    //#endregion

    //    #region 隐藏UI列表的管理

    //    /// <summary>
    //    /// 删除所有隐藏的UI
    //    /// </summary>
    //    public static void DestroyAllHideUI()
    //    {
    //        foreach (List<UIWindowBase> uis in s_hideUIs.Values)
    //        {
    //            for (int i = 0; i < uis.Count; i++)
    //            {
    //                UISystemEvent.Dispatch(uis[i], UIEvent.OnDestroy);  //派发OnDestroy事件
    //                try
    //                {
    //                    uis[i].DestroyUI();
    //                }
    //                catch (Exception e)
    //                {
    //                    Debug.LogError("OnDestroy :" + e.ToString());
    //                }
    //                Destroy(uis[i].gameObject);
    //            }
    //        }

    //        s_hideUIs.Clear();
    //    }

    /// <summary>
    /// 获取一个隐藏的UI,如果有多个同名UI，则返回最后创建的那一个
    /// </summary>
    /// <param name="UIname">UI名</param>
    /// <returns></returns>
    public static UIWindowBase GetHideUI(string UIname)
    {
        if (!m_hideUIs.ContainsKey(UIname))
        {
            return null;
        }
        else
        {
            if (m_hideUIs[UIname].Count == 0)
            {
                return null;
            }
            else
            {
                UIWindowBase ui = m_hideUIs[UIname][m_hideUIs[UIname].Count - 1];
                //默认返回最后创建的那一个
                return ui;
            }
        }
    }

    static bool GetIsExitsHide(UIWindowBase UI)
    {
        if (!m_hideUIs.ContainsKey(UI.name))
        {
            return false;
        }
        else
        {
            return m_hideUIs[UI.name].Contains(UI);
        }
    }

    static void AddHideUI(UIWindowBase UI)
    {
        if (!m_hideUIs.ContainsKey(UI.name))
        {
            m_hideUIs.Add(UI.name, new List<UIWindowBase>());
        }

        m_hideUIs[UI.name].Add(UI);

        UI.Hide();
    }


    static void RemoveHideUI(UIWindowBase UI)
    {
        if (UI == null)
        {
            throw new Exception("UIManager: RemoveUI error l_UI is null: !");
        }

        if (!m_hideUIs.ContainsKey(UI.name))
        {
            throw new Exception("UIManager: RemoveUI error dont find: " + UI.name + "  " + UI);
        }

        if (!m_hideUIs[UI.name].Contains(UI))
        {
            throw new Exception("UIManager: RemoveUI error dont find: " + UI.name + "  " + UI);
        }
        else
        {
            m_hideUIs[UI.name].Remove(UI);
        }
    }

    //#endregion
}



#region UI事件 代理 枚举

/// <summary>
/// UI回调
/// </summary>
/// <param name="objs"></param>
public delegate void UICallBack(UIWindowBase UI, params object[] objs);
public delegate void UIAnimCallBack(UIWindowBase UIbase, UICallBack callBack, params object[] objs);

public enum UIType
{
    GameUI,

    Fixed,
    Normal,
    TopBar,
    PopUp
}

public enum UIEvent
{
    OnOpen,
    OnClose,
    OnHide,
    OnShow,

    OnInit,
    OnDestroy,

    OnRefresh,

    OnStartEnterAnim,
    OnCompleteEnterAnim,

    OnStartExitAnim,
    OnCompleteExitAnim,
}

#endregion
