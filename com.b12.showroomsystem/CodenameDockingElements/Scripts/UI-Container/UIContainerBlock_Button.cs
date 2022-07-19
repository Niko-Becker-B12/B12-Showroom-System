using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Showroom.UI
{

    [System.Serializable]
    public class UIContainerBlock_Button
    {

        public string buttonText;

        public List<Function> buttonOnClickFunctions = new List<Function>();
        public List<Function> buttonOnEnterFunctions = new List<Function>();
        public List<Function> buttonOnExitFunctions = new List<Function>();
        public List<Function> buttonOnResetFunctions = new List<Function>();

    }

}