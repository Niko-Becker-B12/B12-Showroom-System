using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using ThisOtherThing.UI;
using TMPro;
using ThisOtherThing.UI.Shapes;
using UnityEngine.Events;


namespace Showroom.UI
{

    [System.Serializable]
    public class UIContainerBlock_Button
    {

        public string buttonText;

        public string sspPosNumber;

        public List<Function> buttonOnClickFunctions = new List<Function>();
        public List<Function> buttonOnEnterFunctions = new List<Function>();
        public List<Function> buttonOnExitFunctions = new List<Function>();
        public List<Function> buttonOnResetFunctions = new List<Function>();

    }

}