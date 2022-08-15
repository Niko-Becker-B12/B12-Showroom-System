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
        public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.dropdown;

        //[BoxGroup("Dropdown"), ReadOnly]
        //public CustomGeneralMenuDropdownObject customGeneralMenuDropdownObj;

        [BoxGroup("Dropdown")]
        public Sprite dropdownSprite;

        [BoxGroup("Dropdown")]
        public Sprite dropdownChildSprite;

        [BoxGroup("Dropdown")]
        public List<Function> dropdownFunctions = new List<Function>();

        public override void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer, int buttonIndex = -1, int useCaseIndex = -1)
        {

            base.SetUpButton(customGeneralMenuDataContainer, buttonIndex, useCaseIndex);

        }

        public override void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            base.Update();

        }
    }

}