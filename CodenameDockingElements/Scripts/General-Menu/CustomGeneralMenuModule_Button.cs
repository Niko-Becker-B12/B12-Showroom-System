using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ThisOtherThing.UI.Shapes;


namespace Showroom.UI
{

    [System.Serializable]
    public class CustomGeneralMenuModule_Button : CustomGeneralMenuModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.button;

        //[BoxGroup("Button"), ReadOnly]
        //public CustomGeneralMenuButtonObject customGeneralMenuButtonObj;

        [BoxGroup("Button")]
        public Sprite buttonSprite;

        [BoxGroup("Button")]
        public List<Function> buttonOnClickFunctions = new List<Function>();
        [BoxGroup("Button")]
        public List<Function> buttonOnEnterFunctions = new List<Function>();
        [BoxGroup("Button")]
        public List<Function> buttonOnExitFunctions = new List<Function>();

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