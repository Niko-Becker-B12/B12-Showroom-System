using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using ThisOtherThing.UI;
using TMPro;
using ThisOtherThing.UI.Shapes;
using UnityEngine.Events;

namespace Showroom.UI
{

    public class SidebarButtonObject : MonoBehaviour
    {

        [FoldoutGroup("Design")] [ReadOnly] public ColorBlock sidebarButtonColors = ColorBlock.defaultColorBlock;
        [FoldoutGroup("Design")] [ReadOnly] public ColorBlock sidebarButtonTextColors = ColorBlock.defaultColorBlock;
        [FoldoutGroup("Design")] [ReadOnly] public ColorBlock sidebarButtonAdditionalColors = ColorBlock.defaultColorBlock;

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onButtonDown = new List<Function>();

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onButtonEnter = new List<Function>();

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onButtonExit = new List<Function>();

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onButtonReset = new List<Function>();

        [ReadOnly]
        public SidebarButton sidebarButtonDataContainer;
        [ReadOnly]
        public ButtonBehavior sidebarButtonBehavior;
        [ReadOnly]
        public Button sidebarButton;
        [ReadOnly]
        public TextMeshProUGUI sidebarButtonText;
        [ReadOnly]
        public Image sidebarButtonIcon;
        [ReadOnly]
        public Image sidebarButtonChevron;

        [ReadOnly]
        public PixelLine pixelLineTop;
        [ReadOnly]
        public PixelLine pixelLineBottom;


        public virtual void SetUpButton()
        {

            Debug.Log("Setting up Button");

            sidebarButtonBehavior = this.GetComponent<ButtonBehavior>();
            sidebarButton = this.GetComponent<Button>();
            sidebarButtonText = this.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
            sidebarButtonIcon = this.transform.GetChild(2).GetChild(0).GetComponent<Image>();
            sidebarButtonChevron = this.transform.GetChild(2).GetChild(2).GetComponent<Image>();

            pixelLineTop = this.transform.GetChild(0).GetComponent<PixelLine>();
            pixelLineBottom = this.transform.GetChild(1).GetComponent<PixelLine>();


            sidebarButtonIcon.gameObject.SetActive(false);
            sidebarButtonChevron.gameObject.SetActive(false);


            if(sidebarButtonDataContainer != null)
            {

                if(sidebarButtonDataContainer.sidebarButtonSprite != null)
                {

                    sidebarButtonIcon.gameObject.SetActive(true);
                    sidebarButtonIcon.sprite = sidebarButtonDataContainer.sidebarButtonSprite;

                }

                sidebarButtonText.text = sidebarButtonDataContainer.sidebarButtonText;

            }

            onButtonReset.AddRange(sidebarButtonDataContainer.sidebarButtonOnResetFunctions);
            onButtonDown.AddRange(sidebarButtonDataContainer.sidebarButtonOnClickFunctions);
            onButtonEnter.AddRange(sidebarButtonDataContainer.sidebarButtonOnEnterFunctions);
            onButtonExit.AddRange(sidebarButtonDataContainer.sidebarButtonOnExitFunctions);

            sidebarButtonBehavior.onButtonReset.AddRange(onButtonReset);
            sidebarButtonBehavior.onMouseDown.AddRange(onButtonDown);
            sidebarButtonBehavior.onMouseEnter.AddRange(onButtonEnter);
            sidebarButtonBehavior.onMouseExit.AddRange(onButtonExit);

            CodenameDockingElements.Instance.UpdateSidebarContainer();
            CodenameDockingElements.Instance.UpdateSidebarContainer();


            ButtonHighlight();

        }

        public virtual void UpdateButton()
        {

            sidebarButtonText.text = sidebarButtonDataContainer.sidebarButtonText;
            sidebarButtonIcon.sprite = sidebarButtonDataContainer.sidebarButtonSprite;

            ButtonHighlight();

        }

        public virtual void ButtonHighlight()
        {

            sidebarButtonColors = CodenameDockingElements.Instance.baseUISkin.sidebarButtonColors;
            sidebarButtonTextColors = CodenameDockingElements.Instance.baseUISkin.sidebarButtonTextColors;
            sidebarButtonAdditionalColors = CodenameDockingElements.Instance.baseUISkin.sidebarButtonAdditionalColors;

            sidebarButton.colors = sidebarButtonColors;

            #region onHover


            UnityEvent onHoverEvent = new UnityEvent();

            onHoverEvent.AddListener(delegate
            {

                this.SidebarButtonObjectOnHover();

            });

            Function onHoverFunction = new Function
            {
                functionName = onHoverEvent,
                functionDelay = 0f
            };

            sidebarButtonBehavior.onMouseEnter.Add(onHoverFunction);


            #endregion

            #region onExit


            UnityEvent onExitEvent = new UnityEvent();

            onExitEvent.AddListener(delegate
            {

                this.SidebarButtonObjectOnExit();

            });

            Function onExitFunction = new Function
            {
                functionName = onExitEvent,
                functionDelay = 0f
            };

            sidebarButtonBehavior.onMouseExit.Add(onExitFunction);


            #endregion

            #region onClick


            UnityEvent onClickEvent = new UnityEvent();

            onClickEvent.AddListener(delegate
            {

                this.SidebarButtonObjectOnClick();

            });

            Function onClickFunction = new Function
            {
                functionName = onClickEvent,
                functionDelay = 0f
            };

            sidebarButtonBehavior.onMouseDown.Add(onClickFunction);


            #endregion

            #region onReset


            UnityEvent onResetEvent = new UnityEvent();

            onResetEvent.AddListener(delegate
            {

                this.SidebarButtonObjectOnReset();

            });

            Function onResetFunction = new Function
            {
                functionName = onResetEvent,
                functionDelay = 0f
            };

            sidebarButtonBehavior.onButtonReset.Add(onResetFunction);


            #endregion

        }

        public virtual void SidebarButtonObjectOnHover()
        {

            sidebarButtonText.color = sidebarButtonTextColors.highlightedColor;

            sidebarButtonChevron.color = sidebarButtonTextColors.highlightedColor;

            pixelLineTop.color = sidebarButtonAdditionalColors.highlightedColor;
            pixelLineBottom.color = sidebarButtonAdditionalColors.highlightedColor;

        }

        public virtual void SidebarButtonObjectOnExit()
        {

            sidebarButtonText.color = sidebarButtonTextColors.normalColor;

            sidebarButtonChevron.color = sidebarButtonTextColors.normalColor;

            pixelLineTop.color = sidebarButtonAdditionalColors.normalColor;
            pixelLineBottom.color = sidebarButtonAdditionalColors.normalColor;

        }

        public virtual void SidebarButtonObjectOnClick()
        {

            sidebarButtonText.color = sidebarButtonTextColors.pressedColor;

            sidebarButtonChevron.color = sidebarButtonTextColors.pressedColor;

            pixelLineTop.color = sidebarButtonAdditionalColors.pressedColor;
            pixelLineBottom.color = sidebarButtonAdditionalColors.pressedColor;

            CodenameDockingElements.Instance.headButtons[sidebarButtonDataContainer.sidebarButtonUseCaseIndex].sidebarHeadButtonObject.ResetSubButtons(sidebarButtonDataContainer.sidebarButtonSiblingIndex);

        }

        public virtual void SidebarButtonObjectOnReset()
        {

            sidebarButtonText.color = sidebarButtonTextColors.normalColor;

            sidebarButtonChevron.color = sidebarButtonTextColors.normalColor;

            pixelLineTop.color = sidebarButtonAdditionalColors.normalColor;
            pixelLineBottom.color = sidebarButtonAdditionalColors.normalColor;

            //sidebarButtonBehavior.ResetClick();

        }

    }

}