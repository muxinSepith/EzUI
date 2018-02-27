using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EzUI
{

    public class UILayerManager : MonoBehaviour
    {
        public Transform m_GameUILayerParent;
        public Transform m_FixedLayerParent;
        public Transform m_NormalLayerParent;
        public Transform m_TopbarLayerParent;
        public Transform m_PopUpLayerParent;

        public List<UIWindowBase> normalUIList = new List<UIWindowBase>();

        public void Awake()
        {
            if (m_GameUILayerParent == null)
            {
                Debug.LogError("UILayerManager :GameUILayerParent is null!");
            }

            if (m_FixedLayerParent == null)
            {
                Debug.LogError("UILayerManager :FixedLayerParent is null!");
            }

            if (m_NormalLayerParent == null)
            {
                Debug.LogError("UILayerManager :NormalLayerParent is null!");
            }

            if (m_TopbarLayerParent == null)
            {
                Debug.LogError("UILayerManager :TopbarLayerParent is null!");
            }

            if (m_PopUpLayerParent == null)
            {
                Debug.LogError("UILayerManager :popUpLayerParent is null!");
            }
        }

        public void SetLayer(UIWindowBase ui)
        {
            RectTransform rt = ui.GetComponent<RectTransform>();
            switch (ui.m_UIType.UIFormType)
            {
                case UIFormType.GameUI: ui.transform.SetParent(m_GameUILayerParent); break;
                case UIFormType.Fixed: ui.transform.SetParent(m_FixedLayerParent); break;
                case UIFormType.Normal:
                    ui.transform.SetParent(m_NormalLayerParent);
                    normalUIList.Add(ui);
                    break;
                case UIFormType.TopBar: ui.transform.SetParent(m_TopbarLayerParent); break;
                case UIFormType.PopUp: ui.transform.SetParent(m_PopUpLayerParent); break;
            }

            rt.localScale = Vector3.one;
            rt.sizeDelta = Vector2.zero;

            if (ui.m_UIType.UIFormType != UIFormType.GameUI)
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector3.one;

                rt.sizeDelta = Vector2.zero;
                rt.anchoredPosition = Vector3.zero;
                rt.SetAsLastSibling();
            }
        }

        public void RemoveUI(UIWindowBase ui)
        {
            switch (ui.m_UIType.UIFormType)
            {
                case UIFormType.GameUI: break;
                case UIFormType.Fixed: break;
                case UIFormType.Normal:
                    normalUIList.Remove(ui);
                    break;
                case UIFormType.TopBar: break;
                case UIFormType.PopUp: break;
            }
        }
    }

}