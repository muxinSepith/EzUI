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

        
    }

}
