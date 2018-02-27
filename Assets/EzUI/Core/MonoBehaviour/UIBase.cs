using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EzUI
{

    public abstract class UIBase : MonoBehaviour
    {
        public Canvas m_canvas;

        #region 重载方法

        //当UI第一次打开时调用OnInit方法，调用时机在OnOpen之前
        public virtual void OnUIInit()
        {

        }

        protected virtual void OnUIDestroy()
        {

        }

        #endregion

        #region 继承方法

        private int m_UIID = -1;
        public int UIID
        {
            get { return m_UIID; }
        }

        public string UIEventKey
        {
            get { return UIName + m_UIID; }
        }

        string m_UIName = null;
        public string UIName
        {
            get
            {
                if (m_UIName == null)
                {
                    m_UIName = name;
                }

                return m_UIName;
            }
            set
            {
                m_UIName = value;
            }
        }



        public void Init(int id)
        {
            m_UIID = id;
            m_canvas = GetComponent<Canvas>();
            m_UIName = null;

            OnUIInit();
        }

        public void DestroyUI()
        {
            OnUIDestroy();
        }

        #endregion


        #region 注册监听事件

        protected List<Enum> m_EventNames = new List<Enum>();
        protected List<EventHandRegisterInfo> m_EventListeners = new List<EventHandRegisterInfo>();

        protected void RegisterBtnClick(Button btn, Action<GameObject> action)
        {
            btn.onClick.AddListener(delegate ()
            {
                action(btn.gameObject);
            });
        }

        public virtual void RemoveAllListener()
        {
            for (int i = 0; i < m_EventListeners.Count; i++)
            {
                m_EventListeners[i].RemoveListener();
            }
            m_EventListeners.Clear();

            for (int i = 0; i < m_OnClickEvents.Count; i++)
            {
                m_OnClickEvents[i].RemoveListener(true);
            }
            m_OnClickEvents.Clear();

            for (int i = 0; i < m_LongPressEvents.Count; i++)
            {
                m_LongPressEvents[i].RemoveListener(true);
            }
            m_LongPressEvents.Clear();

            #region 拖动事件
            for (int i = 0; i < m_DragEvents.Count; i++)
            {
                m_DragEvents[i].RemoveListener(true);
            }
            m_DragEvents.Clear();

            for (int i = 0; i < m_BeginDragEvents.Count; i++)
            {
                m_BeginDragEvents[i].RemoveListener(true);
            }
            m_BeginDragEvents.Clear();

            for (int i = 0; i < m_EndDragEvents.Count; i++)
            {
                m_EndDragEvents[i].RemoveListener(true);
            }
            m_EndDragEvents.Clear();
            #endregion
        }

        #endregion

    }


}