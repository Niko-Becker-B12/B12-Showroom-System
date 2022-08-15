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

        [ReadOnly]
        public GeneralMenuModuleObject generalMenuModuleObject;

        public string tooltipText;

        public bool isIndexed = false;

        public virtual void SetUpButton(CustomGeneralMenuModule customGeneralMenuDataContainer = null, int buttonIndex = -1, int useCaseIndex = -1)
        {

            if (customGeneralMenuDataContainer == null)
            {

                Debug.Log("Data Container for General Menu Button is empty!");

                return;

            }

            Debug.Log("Generating Generic Module");

            tooltipText = customGeneralMenuDataContainer.tooltipText;

            GameObject newButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuButtonPrefab, CodenameDockingElements.Instance.generalMenuButtonParent);

            generalMenuModuleObject = newButton.GetComponent<GeneralMenuModuleObject>();
            generalMenuModuleObject.data = customGeneralMenuDataContainer;

            generalMenuModuleObject.SetUp();

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