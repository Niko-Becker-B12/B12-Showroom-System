﻿using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ThisOtherThing.UI.Shapes;


namespace Showroom.UI
{

    [System.Serializable]
    public class CustomGeneralMenuModule_Slider : CustomGeneralMenuModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.slider;

        //[BoxGroup("Slider"), ReadOnly]
        //public CustomGeneralMenuSliderObject customGeneralMenuSliderObj;

        [BoxGroup("Slider")]
        public Vector2 sliderMinMaxValues;

        [BoxGroup("Slider")]
        public List<Function> sliderFunctions = new List<Function>();

        public override void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer = null, int buttonIndex = -1, int useCaseIndex = -1)
        {

            base.SetUpButton();

        }

        public override void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            base.Update();

        }
    }

}