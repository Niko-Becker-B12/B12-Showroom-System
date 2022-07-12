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
    public class CustomGeneralMenuDropdownObject : CustomGeneralMenuButtonObject
    {

        public override void SetUpButton()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up General Menu Dropdown module");

            generalMenuButtonBehavior = this.GetComponent<ButtonBehavior>();
            generalMenuButton = this.GetComponent<Button>();
            generalMenuButtonIcon = this.GetComponent<Image>();

            generalMenuButtonIcon.sprite = generalButtonDataContainer.dropdownSprite;


            ButtonHighlight();

        }

    }
}
