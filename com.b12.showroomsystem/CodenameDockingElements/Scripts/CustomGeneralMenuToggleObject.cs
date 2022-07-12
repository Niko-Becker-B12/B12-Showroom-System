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

    public class CustomGeneralMenuToggleObject : CustomGeneralMenuButtonObject
    {

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onSetActive = new List<Function>();

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onSetDeactive = new List<Function>();

        [ReadOnly]
        public ToggleBehavior generalMenuToggleBehavior;

        public override void SetUpButton()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up General Menu toggle module");

            generalMenuToggleBehavior = this.GetComponent<ToggleBehavior>();
            generalMenuButton = this.GetComponent<Button>();
            generalMenuButtonIcon = this.GetComponent<Image>();

            if(generalMenuToggleBehavior.isActive)
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleActiveSprite;
            else
                generalMenuButtonIcon.sprite= generalButtonDataContainer.toggleDeactiveSprite;

            onSetActive.AddRange(generalButtonDataContainer.onSetActiveFunctions);
            onSetDeactive.AddRange(generalButtonDataContainer.onSetDeactiveFunctions);

            generalMenuToggleBehavior.onButtonReset.AddRange(onButtonReset);
            generalMenuToggleBehavior.onSetActive.AddRange(onSetActive);
            generalMenuToggleBehavior.onSetDeactive.AddRange(onSetDeactive);
            generalMenuToggleBehavior.onMouseEnter.AddRange(onButtonEnter);
            generalMenuToggleBehavior.onMouseExit.AddRange(onButtonExit);
            
            
            ButtonHighlight();

        }

        public override void UpdateButton()
        {

            if (generalMenuToggleBehavior.isActive)
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleActiveSprite;
            else
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleDeactiveSprite;

        }

        public override void ButtonHighlight()
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

            generalMenuToggleBehavior.onMouseEnter.Add(onHoverFunction);


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

            generalMenuToggleBehavior.onMouseExit.Add(onExitFunction);


            #endregion

            #region onClick


            UnityEvent onClickActiveEvent = new UnityEvent();

            onClickActiveEvent.AddListener(delegate
            {

                //this.GeneralMenuButtonObjectOnClick();

            });

            Function onClickActiveFunction = new Function
            {
                functionName = onClickActiveEvent,
                functionDelay = 0f
            };

            generalMenuToggleBehavior.onSetActive.Add(onClickActiveFunction);


            UnityEvent onClickDeactiveEvent = new UnityEvent();

            onClickDeactiveEvent.AddListener(delegate
            {

                //this.GeneralMenuButtonObjectOnClick();

            });

            Function onClickDeactiveFunction = new Function
            {
                functionName = onClickDeactiveEvent,
                functionDelay = 0f
            };

            generalMenuToggleBehavior.onSetDeactive.Add(onClickDeactiveFunction);


            #endregion

        }

        public override void GeneralMenuButtonObjectOnClick()
        {

            if (generalMenuToggleBehavior.isActive)
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleActiveSprite;
            else
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleDeactiveSprite;

        }

    }

}