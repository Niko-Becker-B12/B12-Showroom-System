using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Showroom.UI
{

    public class SidebarHeaderButtonObject : SidebarButtonObject
    {

        [FoldoutGroup("Header Only")]
        public List<SidebarButtonObject> subButtons = new List<SidebarButtonObject>();

        [ReadOnly]
        public SidebarHeadButton sidebarHeadButtonDataContainer;

        bool isActive = false;


        public override void SetUpButton()
        {

            base.SetUpButton();

            if(sidebarHeadButtonDataContainer.sidebarHeadButtonSubButtons.Count > 0)
                sidebarButtonChevron.gameObject.SetActive(true);

            this.gameObject.name = "SidebarButton_" + sidebarHeadButtonDataContainer.sidebarHeadButtonText + sidebarHeadButtonDataContainer.sidebarHeadButtonUseCaseIndex.ToString();

            if (sidebarHeadButtonDataContainer != null)
            {

                if (sidebarHeadButtonDataContainer.sidebarHeadButtonSprite != null)
                {

                    sidebarButtonIcon.gameObject.SetActive(true);
                    sidebarButtonIcon.sprite = sidebarHeadButtonDataContainer.sidebarHeadButtonSprite;

                }

                sidebarButtonText.text = sidebarHeadButtonDataContainer.sidebarHeadButtonText;

            }

            for (int i = 0; i < subButtons.Count; i++)
            {

                subButtons[i].sidebarButtonDataContainer = sidebarHeadButtonDataContainer.sidebarHeadButtonSubButtons[i];

                subButtons[i].sidebarButtonDataContainer.sidebarbuttonUseCaseIndex = sidebarHeadButtonDataContainer.sidebarHeadButtonUseCaseIndex;
                subButtons[i].sidebarButtonDataContainer.sidebarbuttonSiblingIndex = i;

                subButtons[i].SetUpButton();

                subButtons[i].gameObject.SetActive(false);

            }

        }

        public override void UpdateButton()
        {

            sidebarButtonText.text = sidebarHeadButtonDataContainer.sidebarHeadButtonText;
            sidebarButtonIcon.sprite = sidebarHeadButtonDataContainer.sidebarHeadButtonSprite;

            if (subButtons.Count != sidebarHeadButtonDataContainer.sidebarHeadButtonSubButtons.Count)
                Debug.Log("There's more either more Data Containers than Buttons, or more Buttons than Data Containers. Setting Up new Buttons!");


            for (int i = 0; i < subButtons.Count; i++)
            {

                subButtons[i].sidebarButtonDataContainer = sidebarHeadButtonDataContainer.sidebarHeadButtonSubButtons[i];

                subButtons[i].UpdateButton();

            }

        }

        public override void ButtonHighlight()
        {

            base.ButtonHighlight();

            sidebarButtonColors = CodenameDockingElements.Instance.baseUISkin.sidebarHeaderButtonColors;
            sidebarButtonTextColors = CodenameDockingElements.Instance.baseUISkin.sidebarHeaderButtonTextColors;
            sidebarButtonAdditionalColors = CodenameDockingElements.Instance.baseUISkin.sidebarHeaderButtonAdditionalColors;

            sidebarButton.colors = sidebarButtonColors;

        }

        public override void SidebarButtonObjectOnClick()
        {

            //base.SidebarButtonObjectOnClick();

            sidebarButtonText.color = sidebarButtonTextColors.pressedColor;

            sidebarButtonChevron.color = sidebarButtonTextColors.pressedColor;

            pixelLineTop.color = sidebarButtonAdditionalColors.pressedColor;
            pixelLineBottom.color = sidebarButtonAdditionalColors.pressedColor;

            EnableSubButtons();

            CodenameDockingElements.Instance.ToggleGeneralMenu(true);

        }

        public override void SidebarButtonObjectOnReset()
        {

            base.SidebarButtonObjectOnReset();

            DisableSubButtons();

        }

        public void EnableSubButtons()
        {

            isActive = false;

            ToggleSubButtons();

            SelectSubButton(-1);

            CodenameDockingElements.Instance.SelectSidebarButton(sidebarHeadButtonDataContainer.sidebarHeadButtonUseCaseIndex);

            sidebarButtonChevron.transform.DORotate(new Vector3(0f, 0f, -90f), .1f);

        }

        public void DisableSubButtons()
        {

            isActive = true;

            SelectSubButton(-1);

            ToggleSubButtons();

            sidebarButtonChevron.transform.DORotate(new Vector3(0f, 0f, 0f), .1f);

        }

        void ToggleSubButtons()
        {

            isActive = !isActive;

            for (int i = 0; i < subButtons.Count; i++)
            {

                subButtons[i].gameObject.SetActive(isActive);

                CodenameDockingElements.Instance.UpdateSidebarContainer();

            }

        }

        public void SelectSubButton(int index)
        {

            if(index != -1)
            {

                for (int i = 0; i < subButtons.Count; i++)
                {

                    if (i == index)
                    {

                        continue;

                    }

                    if(subButtons[i].gameObject.activeSelf)
                        subButtons[i].sidebarButtonBehavior.ResetClick();

                }

            }
            else
            {

                for (int i = 0; i < subButtons.Count; i++)
                {

                    if (subButtons[i].gameObject.activeSelf)
                        subButtons[i].sidebarButtonBehavior.ResetClick();

                }

            }

        }

    }

}