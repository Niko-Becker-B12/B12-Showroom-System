using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Showroom.UI
{

    [System.Serializable]
    public class UIContainerModule_NumericList : UserInterfaceContainerModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        public UserInterfaceContainerModuleType userInterfaceContainerModuleType = UserInterfaceContainerModuleType.numericList;

        public List<UIContainerBlock_Button> containerModuleButtons = new List<UIContainerBlock_Button>();

        public override void Create(Transform moduleParent)
        {

            moduleParent.gameObject.AddComponent(typeof(VerticalLayoutGroup));

            //Set Layout Data
            VerticalLayoutGroup layoutGroup = moduleParent.GetComponent<VerticalLayoutGroup>();
            ContentSizeFitter contentSizeFitter = moduleParent.GetComponent<ContentSizeFitter>();

            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            //contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            layoutGroup.padding = CodenameDockingElements.Instance.baseUISkin.uiContainerPadding;


            for (int i = 0; i < containerModuleButtons.Count; i++)
            {

                GameObject newButton = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerButtonPrefab, moduleParent);

                TextMeshProUGUI newText = newButton.GetComponentInChildren<TextMeshProUGUI>();

                newText.text = string.Format("{0}. {1}", (i + 1), containerModuleButtons[i].buttonText);

                newButton.GetComponent<Button>().colors = CodenameDockingElements.Instance.baseUISkin.customUIContainerButtonColors;

            }

        }

    }

}