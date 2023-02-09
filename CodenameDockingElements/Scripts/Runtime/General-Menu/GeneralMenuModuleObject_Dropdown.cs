using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Showroom.UI
{

    public class GeneralMenuModuleObject_Dropdown : GeneralMenuModuleObject
    {

        [ReadOnly]
        public Button button;
        [ReadOnly]
        public ButtonBehavior behavior;
        [ReadOnly]
        public Image icon;

        //public CustomGeneralMenuModule_Button data;

        [FoldoutGroup("Design")][ReadOnly] public ColorBlock generalMenuButtonColors = ColorBlock.defaultColorBlock;
        [FoldoutGroup("Design")][ReadOnly] public ColorBlock generalMenuButtonIconColors = ColorBlock.defaultColorBlock;

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


        public override void SetUp()
        {

            //base.SetUp();

            data.generalMenuModuleObject = this.gameObject.GetComponent<GeneralMenuModuleObject>();

            Debug.Log($"Setting up General Menu Button {data.GetType()}");

            button = GetComponent<Button>();
            behavior = GetComponent<ButtonBehavior>();
            icon = this.transform.GetChild(0).GetComponent<Image>();

            icon.sprite = (data as CustomGeneralMenuModule_Button).buttonSprite;

            icon.color = CodenameDockingElements.Instance.baseUISkin.generalMenuButtonIconColors.normalColor;

            generalMenuButtonColors = CodenameDockingElements.Instance.baseUISkin.generalMenuButtonColors;
            generalMenuButtonIconColors = CodenameDockingElements.Instance.baseUISkin.generalMenuButtonIconColors;

            button.colors = generalMenuButtonColors;

            onButtonDown.AddRange((data as CustomGeneralMenuModule_Button).buttonOnClickFunctions);
            onButtonEnter.AddRange((data as CustomGeneralMenuModule_Button).buttonOnEnterFunctions);
            onButtonExit.AddRange((data as CustomGeneralMenuModule_Button).buttonOnExitFunctions);

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

            behavior.onMouseEnter.Add(onHoverFunction);
            behavior.onMouseEnter.AddRange(onButtonEnter);

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

            behavior.onMouseExit.Add(onExitFunction);
            behavior.onMouseExit.AddRange(onButtonExit);


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

            behavior.onMouseDown.Add(onClickFunction);
            behavior.onMouseDown.AddRange(onButtonDown);


            #endregion

            #region onReset


            UnityEvent onResetEvent = new UnityEvent();

            onResetEvent.AddListener(delegate
            {

                this.GeneralMenuButtonObjectOnReset();

            });

            Function onResetFunction = new Function
            {
                functionName = onResetEvent,
                functionDelay = 0f
            };

            behavior.onButtonReset.Add(onResetFunction);


            #endregion

            for (int i = 0; i < onButtonDown.Count; i++)
            {

                Debug.Log(onButtonDown[i].functionName + " " + i);

            }

        }

        public virtual void GeneralMenuButtonObjectOnHover()
        {

            icon.color = generalMenuButtonIconColors.highlightedColor;

            CodenameDockingElements.Instance.DisplayTooltip(this.GetComponent<RectTransform>(), data.tooltipText);

        }

        public virtual void GeneralMenuButtonObjectOnExit()
        {

            icon.color = generalMenuButtonIconColors.normalColor;

            CodenameDockingElements.Instance.DisableTooltip();

        }

        public virtual void GeneralMenuButtonObjectOnClick()
        {

            icon.color = generalMenuButtonIconColors.pressedColor;

        }

        public virtual void GeneralMenuButtonObjectOnReset()
        {

            icon.color = generalMenuButtonIconColors.normalColor;

        }

    }

}