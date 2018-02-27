using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EzUI
{

    public class UIStackManager : MonoBehaviour
    {
        List<UIWindowBase> m_normalStack = new List<UIWindowBase>();
        List<UIWindowBase> m_fixedStack = new List<UIWindowBase>();
        List<UIWindowBase> m_popupStack = new List<UIWindowBase>();
        List<UIWindowBase> m_topBarStack = new List<UIWindowBase>();

        public void OnUIOpen(UIWindowBase ui)
        {
            switch (ui.m_UIType.UIFormType)
            {
                case UIFormType.Fixed: m_fixedStack.Add(ui); break;
                case UIFormType.Normal: m_normalStack.Add(ui); break;
                case UIFormType.PopUp: m_popupStack.Add(ui); break;
                case UIFormType.TopBar: m_topBarStack.Add(ui); break;
            }
        }

        public void OnUIClose(UIWindowBase ui)
        {
            switch (ui.m_UIType.UIFormType)
            {
                case UIFormType.Fixed: m_fixedStack.Remove(ui); break;
                case UIFormType.Normal: m_normalStack.Remove(ui); break;
                case UIFormType.PopUp: m_popupStack.Remove(ui); break;
                case UIFormType.TopBar: m_topBarStack.Remove(ui); break;
            }
        }

        public void CloseLastUIWindow(UIFormType uiType = UIFormType.Normal)
        {
            UIWindowBase ui = GetLastUI(uiType);

            if (ui != null)
            {
                UIManager.CloseUIWindow(ui);
            }
        }

        public UIWindowBase GetLastUI(UIFormType uiType)
        {
            switch (uiType)
            {
                case UIFormType.Fixed:
                    if (m_fixedStack.Count > 0)
                        return m_fixedStack[m_fixedStack.Count - 1];
                    else
                        return null;
                case UIFormType.Normal:
                    if (m_normalStack.Count > 0)
                        return m_normalStack[m_normalStack.Count - 1];
                    else
                        return null;
                case UIFormType.PopUp:
                    if (m_popupStack.Count > 0)
                        return m_popupStack[m_popupStack.Count - 1];
                    else
                        return null;
                case UIFormType.TopBar:
                    if (m_topBarStack.Count > 0)
                        return m_topBarStack[m_topBarStack.Count - 1];
                    else
                        return null;
            }

            throw new System.Exception("CloseLastUIWindow does not support GameUI");
        }
    }
}
