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
    public class CustomGeneralMenuModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        public CustomGeneralMenuButtonType customGeneralMenuModuleType = CustomGeneralMenuButtonType.button;

        public string tooltipText;

        public bool isIndexed = false;

        public virtual void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer = null, int buttonIndex = -1, int useCaseIndex = -1)
        {

            tooltipText = customGeneralMenuDataContainer.tooltipText;

        }

        public virtual void Update(CustomGeneralMenuModule customGeneralMenuDataContainer = null)
        {

            tooltipText = customGeneralMenuDataContainer.tooltipText;

        }
    }

    public enum CustomGeneralMenuButtonType
    {

        button,
        toggle,
        slider,
        dropdown

    }

}