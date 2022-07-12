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

            generalMenuButtonBehavior = this.GetComponent<ButtonBehavior>();
            generalMenuButton = this.GetComponent<Button>();
            generalMenuButtonIcon = this.GetComponent<Image>();

            if(generalMenuToggleBehavior.isActive)
                generalMenuButtonIcon.sprite = generalButtonDataContainer.toggleActiveSprite;
            else
                generalMenuButtonIcon.sprite= generalButtonDataContainer.toggleDeactiveSprite;

            generalMenuToggleBehavior.onButtonReset.AddRange(onButtonReset);
            generalMenuToggleBehavior.onSetActive.AddRange(onSetActive);
            generalMenuToggleBehavior.onSetDeactive.AddRange(onSetDeactive);
            generalMenuToggleBehavior.onMouseEnter.AddRange(onButtonEnter);
            generalMenuToggleBehavior.onMouseExit.AddRange(onButtonExit);


            ButtonHighlight();

        }

    }

}