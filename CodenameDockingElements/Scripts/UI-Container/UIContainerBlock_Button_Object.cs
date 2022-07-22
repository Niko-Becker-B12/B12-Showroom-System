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
    public class UIContainerBlock_Button_Object : MonoBehaviour
    {

        public ButtonBehavior behavior;
        public Button button;
        public Rectangle backgroundRect;
        public Image icon;
        public TextMeshProUGUI text;

        public UIContainerBlock_Button data;
        public int index = -1;

        public ColorBlock buttonColors;
        public ColorBlock buttonTextColors;

        public List<Function> buttonOnClickFunctions = new List<Function>();
        public List<Function> buttonOnEnterFunctions = new List<Function>();
        public List<Function> buttonOnExitFunctions = new List<Function>();
        public List<Function> buttonOnResetFunctions = new List<Function>();

        public virtual void SetUpButton(string buttonText)
        {
        
            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up General Menu button module");
        
            behavior = this.GetComponent<ButtonBehavior>();
            button = this.GetComponent<Button>();
            backgroundRect = this.GetComponent<Rectangle>();
            icon = this.transform.GetChild(0).GetComponent<Image>();
            text = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            text.text = buttonText;

            //icon.sprite = generalButtonDataContainer.buttonSprite;

            buttonOnClickFunctions.AddRange(data.buttonOnClickFunctions);
            buttonOnEnterFunctions.AddRange(data.buttonOnEnterFunctions);
            buttonOnExitFunctions.AddRange(data.buttonOnExitFunctions);
            buttonOnResetFunctions.AddRange(data.buttonOnResetFunctions);

            behavior.onButtonReset.AddRange(buttonOnResetFunctions);
            behavior.onMouseDown.AddRange(buttonOnClickFunctions);
            behavior.onMouseEnter.AddRange(buttonOnEnterFunctions);
            behavior.onMouseExit.AddRange(buttonOnExitFunctions);


            ButtonHighlight();
        
        }

        public virtual void ButtonHighlight()
        {

            buttonColors = CodenameDockingElements.Instance.baseUISkin.customUIContainerButtonColors;
            buttonTextColors = CodenameDockingElements.Instance.baseUISkin.customUIContainerButtonTextColors;

            button.colors = buttonColors;

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


            #endregion

        }

        public virtual void UpdateButton()
        {



        }

        public virtual void GeneralMenuButtonObjectOnHover()
        {

            text.color = buttonTextColors.highlightedColor;

        }

        public virtual void GeneralMenuButtonObjectOnExit()
        {

            text.color = buttonTextColors.normalColor;

        }

        public virtual void GeneralMenuButtonObjectOnClick()
        {

            text.color = buttonTextColors.selectedColor;

        }

    }
}
