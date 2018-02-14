using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EzUI
{

    public class UISetting
    {
        private static UISetting _instance;
        public static UISetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UISetting();
                }
                return _instance;
            }
        }





        public static void AddUI()
        {

        }

        public static void RemoveUI()
        {

        }


    }

}