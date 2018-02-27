using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EzUI
{

    public class UIType
    {
        public UIFormType UIFormType = UIFormType.Normal;
    }



    public enum UIFormType
    {
        GameUI,
        Normal,
        Fixed,
        PopUp,
        TopBar,
    }

    public enum UIFormShowMode
    {
        Normal,
        ReverseChange,
        HideOther,
    }

    public enum UIFormLucencyType
    {
        Lucency,
        Translucence,
        ImPenetrable,
        Pentrate,
    }
}