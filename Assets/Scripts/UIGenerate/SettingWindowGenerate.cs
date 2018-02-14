using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzUI;
using UnityEngine.UI;

/// <summary>
/// 该类是自动生成的，请不要修改
/// </summary>
public abstract class SettingWindowGenerate : UIWindowBase
{
    protected SettingWindowView view;

    public SettingWindowGenerate()
    {
        view = new SettingWindowView(m_uiRoot.transform);

        RegisterBtns();
    }

    protected void RegisterBtns()
    {
        RegisterBtnClick(view.btn_back, BtnBack_OnBtnBackClick);
    }

    protected abstract void BtnBack_OnBtnBackClick(GameObject go);

}

public class SettingWindowView
{
    public Button btn_back;

    public SettingWindowView(Transform transform)
    {
        btn_back = transform.Find("BtnBack").GetComponent<Button>();
    }
}

