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
    public class CustomGeneralMenuModule_Toggle : CustomGeneralMenuModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        new public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.toggle;

        //[BoxGroup("Toggle"), ReadOnly]
        //public CustomGeneralMenuToggleObject customGeneralMenuToggleObj;

        [BoxGroup("Toggle")]
        public Sprite toggleActiveSprite;

        [BoxGroup("Toggle")]
        public Sprite toggleDeactiveSprite;

        [BoxGroup("Toggle"), ReadOnly]
        public bool toggleIsActive;

        [BoxGroup("Toggle")]
        public List<Function> onSetActiveFunctions = new List<Function>();
        [BoxGroup("Toggle")]
        public List<Function> onSetDeactiveFunctions = new List<Function>();

        public override void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer, int buttonIndex = -1, int useCaseIndex = -1, Transform parent = null)
        {

            base.SetUpButton(customGeneralMenuDataContainer, buttonIndex, useCaseIndex, parent);

        }

        public override void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            base.Update();

        }
    }

}