using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ThisOtherThing.UI.Shapes;
using UnityEngine.InputSystem;


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

        public override void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer = null, int buttonIndex = -1, int useCaseIndex = -1, Transform parent = null)
        {

            //base.SetUpButton(customGeneralMenuDataContainer, buttonIndex, useCaseIndex);

            Debug.Log("Generating Button Module");

            GameObject newButton;

            newButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuButtonPrefab, parent);


            generalMenuModuleObject = newButton.GetComponent<GeneralMenuModuleObject_Button>();
            generalMenuModuleObject.data = this as CustomGeneralMenuModule_Button;

            generalMenuModuleObject.SetUp();

        }

        public override void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            base.Update();

        }
    }

}