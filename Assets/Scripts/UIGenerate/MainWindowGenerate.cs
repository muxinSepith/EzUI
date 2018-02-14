using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EzUI;

/// <summary>
/// 该类是自动生成的，请不要修改
/// </summary>
public abstract class MainWindowGenerate : UIWindowBase
{
    protected MainWindowView view;

    public MainWindowGenerate()
    {
        view = new MainWindowView(m_uiRoot.transform);

        RegisterBtns();
    }

    protected void RegisterBtns()
    {
        RegisterBtnClick(view.btn_setting, BtnSetting_OnBtnSettingClick);
    }

    protected abstract void BtnSetting_OnBtnSettingClick(GameObject go);

}

public class MainWindowView
{
    public Button btn_setting;

    public MainWindowView(Transform transform)
    {
        btn_setting = transform.Find("BtnSetting").GetComponent<Button>();
    }
}
