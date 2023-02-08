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
    public class UIContainerBlock_Headline_Object : MonoBehaviour
    {

        public Rectangle backgroundRect;

        public TextMeshProUGUI headlineText;
        public TextMeshProUGUI subHeadlineText;

        public UIContainerBlock_Headline data;


        public virtual void SetUpButton()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up Headline Module");

            backgroundRect = this.GetComponent<Rectangle>();

            headlineText.text = data.headlineText;
            subHeadlineText.text = data.subHeadlineText;


            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        }

        public virtual void UpdateButton()
        {



        }

    }
}
