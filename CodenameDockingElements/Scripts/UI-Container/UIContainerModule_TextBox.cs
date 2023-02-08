using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Showroom.UI
{

    [System.Serializable]
    public class UIContainerModule_TextBox : UserInterfaceContainerModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        new public UserInterfaceContainerModuleType userInterfaceContainerModuleType = UserInterfaceContainerModuleType.textBox;

        //public List<UIContainerBlock_Text> containerModuleButtons = new List<UIContainerBlock_Text>();

        public UIContainerBlock_Text textBox;

        public override void Create(Transform moduleParent)
        {

            moduleParent.gameObject.AddComponent(typeof(VerticalLayoutGroup));

            //Set Layout Data
            VerticalLayoutGroup layoutGroup = moduleParent.GetComponent<VerticalLayoutGroup>();
            ContentSizeFitter contentSizeFitter = moduleParent.GetComponent<ContentSizeFitter>();

            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            //contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            layoutGroup.padding = CodenameDockingElements.Instance.baseUISkin.uiContainerPadding;


            GameObject newTextBoxHeadline = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerHeadlinePrefab, moduleParent);

            UIContainerBlock_Headline_Object uiContainerBlockHeadline = newTextBoxHeadline.GetComponent<UIContainerBlock_Headline_Object>();

            uiContainerBlockHeadline.data = textBox.headline;


            GameObject newTextBox = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerTextBoxPrefab, moduleParent);

            UIContainerBlock_Text_Object uiContainerBlockTextBox = newTextBoxHeadline.GetComponent<UIContainerBlock_Text_Object>();

            uiContainerBlockTextBox.data = textBox;

            uiContainerBlockTextBox.SetUpButton();
            uiContainerBlockHeadline.SetUpButton();


            //for (int i = 0; i < containerModuleButtons.Count; i++)
            //{
            //
            //    GameObject newButton = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerButtonPrefab, moduleParent);
            //
            //    UIContainerBlock_Button_Object uIContainerBlockButton = newButton.GetComponent<UIContainerBlock_Button_Object>();
            //
            //    uIContainerBlockButton.data = containerModuleButtons[i];
            //    uIContainerBlockButton.index = i;
            //
            //    uIContainerBlockButton.SetUpButton(string.Format("{0}<indent=5%>{1}", "•", containerModuleButtons[i].buttonText));
            //
            //}

        }

    }

}