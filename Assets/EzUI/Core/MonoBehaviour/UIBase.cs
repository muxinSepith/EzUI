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


        #region 注册事件

        protected void RegisterBtnClick(Button btn, Action<GameObject> action)
        {
            btn.onClick.AddListener(delegate ()
            {
                action(btn.gameObject);
            });
        }

        #endregion

    }


}