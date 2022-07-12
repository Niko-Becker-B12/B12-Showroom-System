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

    public class CustomGeneralMenuButtonObject : MonoBehaviour
    {

        [FoldoutGroup("Design")][ReadOnly] public ColorBlock generalMenuButtonColors = ColorBlock.defaultColorBlock;

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
        public CustomGeneralMenuButton generalButtonDataContainer;
        [ReadOnly]
        public ButtonBehavior generalMenuButtonBehavior;
        [ReadOnly]
        public Button generalMenuButton;
        [ReadOnly]
        public Image generalMenuButtonIcon;


        public virtual void SetUpButton()
        {

            generalMenuButtonBehavior = this.GetComponent<ButtonBehavior>();
            generalMenuButton = this.GetComponent<Button>();
            generalMenuButtonIcon = this.GetComponent<Image>();

            generalMenuButtonIcon.sprite = generalButtonDataContainer.buttonSprite;


            generalMenuButtonBehavior.onButtonReset.AddRange(onButtonReset);
            generalMenuButtonBehavior.onMouseDown.AddRange(onButtonDown);
            generalMenuButtonBehavior.onMouseEnter.AddRange(onButtonEnter);
            generalMenuButtonBehavior.onMouseExit.AddRange(onButtonExit);


            ButtonHighlight();

        }

        public void ButtonHighlight()
        {

            generalMenuButtonColors = CodenameDockingElements.Instance.baseUISkin.generalMenuButtonColors;

            generalMenuButton.colors = generalMenuButtonColors;

            #region onHover


            UnityEvent onHoverEvent = new UnityEvent();

            onHoverEvent.AddListener(delegate
            {

                this.GeneralMenuButtonObjectOnHover();

            });

            Function onHoverFunction = new Function
            {
                functionName = onHoverEvent,
                functionDelay = 0f
            };

            generalMenuButtonBehavior.onMouseEnter.Add(onHoverFunction);


            #endregion

            #region onExit


            UnityEvent onExitEvent = new UnityEvent();

            onExitEvent.AddListener(delegate
            {

                this.GeneralMenuButtonObjectOnExit();

            });

            Function onExitFunction = new Function
            {
                functionName = onExitEvent,
                functionDelay = 0f
            };

            generalMenuButtonBehavior.onMouseExit.Add(onExitFunction);


            #endregion

            #region onClick


            UnityEvent onClickEvent = new UnityEvent();

            onClickEvent.AddListener(delegate
            {

                this.GeneralMenuButtonObjectOnClick();

            });

            Function onClickFunction = new Function
            {
                functionName = onClickEvent,
                functionDelay = 0f
            };

            generalMenuButtonBehavior.onMouseDown.Add(onClickFunction);


            #endregion

        }

        public virtual void GeneralMenuButtonObjectOnHover()
        {

            CodenameDockingElements.Instance.DisplayTooltip(this.GetComponent<RectTransform>(), generalButtonDataContainer.tooltipText);

        }

        public virtual void GeneralMenuButtonObjectOnExit()
        {

            CodenameDockingElements.Instance.DisableTooltip();

        }

        public virtual void GeneralMenuButtonObjectOnClick()
        {

            CodenameDockingElements.Instance.DisableTooltip();

        }

    }

}