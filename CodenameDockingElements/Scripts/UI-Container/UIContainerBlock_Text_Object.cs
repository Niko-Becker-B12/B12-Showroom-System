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

    public class UIContainerBlock_Text_Object : MonoBehaviour
    {


        public Rectangle backgroundRect;
        public TextMeshProUGUI text;

        public UIContainerBlock_Text data;

        public virtual void SetUpButton()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Setting up TextBox Module");

            backgroundRect = this.GetComponent<Rectangle>();

            text.text = data.text;


            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        }

        public virtual void UpdateButton()
        {



        }

    }

}