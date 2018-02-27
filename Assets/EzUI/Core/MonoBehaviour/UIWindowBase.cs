using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EzUI
{

    public abstract class UIWindowBase : UIBase
    {

        /// <summary>
        /// 类型
        /// </summary>
        public UIType m_UIType;

        /// <summary>
        /// 背景
        /// </summary>
        public GameObject m_bgMask;

        /// <summary>
        /// 根节点
        /// </summary>
        public GameObject m_uiRoot;


        #region 重载方法

        public virtual void OnUIOpen()
        {

        }

        public virtual void OnUIClose()
        {

        }

        public virtual void OnUIHide()
        {

        }

        public virtual void OnUIShow()
        {

        }

        public virtual void OnRefresh()
        {

        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion





        #region 动画
        
        public virtual IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
        {
            //默认无动画
            l_animComplete(this, l_callBack, objs);

            yield break;
        }

        public virtual void OnCompleteEnterAnim()
        {
        }

        public virtual IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
        {
            //默认无动画
            l_animComplete(this, l_callBack, objs);

            yield break;
        }

        public virtual void OnCompleteExitAnim()
        {
        }

        #endregion




        
        #region 继承方法
        
        //刷新是主动调用
        public void Refresh(params object[] args)
        {
            UISystemEvent.Dispatch(this, UIEvent.OnRefresh);
            OnRefresh();
        }

        public void AddEventListener(Enum l_Event)
        {
            if (!m_EventNames.Contains(l_Event))
            {
                m_EventNames.Add(l_Event);
                GlobalEvent.AddEvent(l_Event, Refresh);
            }
        }

        public override void RemoveAllListener()
        {
            base.RemoveAllListener();

            for (int i = 0; i < m_EventNames.Count; i++)
            {
                GlobalEvent.RemoveEvent(m_EventNames[i], Refresh);
            }

            m_EventNames.Clear();
        }


        #endregion
    }

}
