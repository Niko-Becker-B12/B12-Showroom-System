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
    public class CustomGeneralMenuModule_Dropdown : CustomGeneralMenuModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        new public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.dropdown;

        //[BoxGroup("Dropdown"), ReadOnly]
        //public CustomGeneralMenuDropdownObject customGeneralMenuDropdownObj;

        [BoxGroup("Dropdown")]
        public Sprite dropdownSprite;

        [BoxGroup("Dropdown")]
        public Sprite dropdownChildSprite;

        [BoxGroup("Dropdown")]
        public List<Function> dropdownFunctions = new List<Function>();

        public override void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer, int buttonIndex = -1, int useCaseIndex = -1, Transform parent = null)
        {

            //base.SetUpButton(customGeneralMenuDataContainer, buttonIndex, useCaseIndex);

            Debug.Log("Generating Button Module");

            GameObject newButton;

            newButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuDropdownPrefab, parent);


            generalMenuModuleObject = newButton.GetComponent<GeneralMenuModuleObject_Dropdown>();
            generalMenuModuleObject.data = this as CustomGeneralMenuModule_Dropdown;

            generalMenuModuleObject.SetUp();

        }

        public override void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            base.Update();

        }
    }

}