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
    public class CustomGeneralMenuSliderObject : CustomGeneralMenuButtonObject
    {

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onSetActive = new List<Function>();

        [FoldoutGroup("Functions")]
        [ReadOnly]
        public List<Function> onSetDeactive = new List<Function>();

        [ReadOnly]
        public ButtonBehavior generalMenuSliderBehavior;

        public override void SetUpButton()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up General Menu slider module");

            generalMenuButtonBehavior = this.GetComponent<ButtonBehavior>();
            generalMenuButton = this.GetComponent<Button>();
            generalMenuButtonIcon = this.GetComponent<Image>();


            ButtonHighlight();

        }

    }
}
