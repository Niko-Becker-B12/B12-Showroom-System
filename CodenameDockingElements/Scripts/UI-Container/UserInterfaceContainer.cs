using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ThisOtherThing.UI.Shapes;
using TMPro;

namespace Showroom.UI
{

    [System.Serializable]
    [Sirenix.OdinInspector.InlineEditor]
    [CreateAssetMenu(fileName = "UI-Container", menuName = "Showroom/UI-Container")]
    public class UserInterfaceContainer : SerializedScriptableObject
    {

        public string uiContainerShortName;

        public string uiContainerLongName;

        public Vector2 uiContainerClosedPosition;
        public Vector2 uiContainerOpenedPosition;

        public Vector2 uiContainerSize;

        public DockingPositions uiContainerDockingPosition;

        [HideInInspector]
        public RectTransform uiContainerRect;
        [HideInInspector]
        public CanvasGroup uiContainerCanvasGroup;

        [OdinSerialize]
        private UserInterfaceContainerModule uiModule;

        [ReadOnly]
        public GameObject uiContainerObj;

        [ReadOnly]
        public GameObject uiContainerHeadlineObj;


        public virtual void CreateUIContainer()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log(string.Format("Generating UI-Container with the name: {0}!", uiContainerShortName));


            uiContainerObj = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerPrefab, CodenameDockingElements.Instance.uiContainerParent) as GameObject;
            uiContainerRect = uiContainerObj.GetComponent<RectTransform>();
            
            switch(uiContainerDockingPosition)
            {

                case DockingPositions.topCenter:
                    uiContainerRect.pivot = new Vector2(0.5f, 1f);
                    break;
                case DockingPositions.topRight:
                    uiContainerRect.pivot = new Vector2(1f, 1f);
                    break;
                case DockingPositions.rightCenter:
                    uiContainerRect.pivot = new Vector2(1f, 0.5f);
                    break;
                case DockingPositions.bottomRight:
                    uiContainerRect.pivot = new Vector2(1f, 0f);
                    break;
                case DockingPositions.bottomCenter:
                    uiContainerRect.pivot = new Vector2(0.5f, 0f);
                    break;
                case DockingPositions.bottomLeft:
                    uiContainerRect.pivot = new Vector2(0f, 0f);
                    break;
                case DockingPositions.leftCenter:
                    uiContainerRect.pivot = new Vector2(0f, 0.5f);
                    break;
                case DockingPositions.topLeft:
                    uiContainerRect.pivot = new Vector2(0f, 1f);
                    break;
                case DockingPositions.center:
                    uiContainerRect.pivot = new Vector2(0.5f, 0.5f);
                    break;

            }

            uiContainerRect.anchoredPosition = uiContainerClosedPosition;

            uiContainerRect.sizeDelta = uiContainerSize;

            uiContainerHeadlineObj = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerHeadlinePrefab, uiContainerObj.transform);

            uiContainerHeadlineObj.GetComponent<TextMeshProUGUI>().text = uiContainerLongName;

            uiModule.Create(uiContainerObj.transform);

            ApplyBaseSkin();

        }

        public virtual void UpdateUIContainer()
        {

            uiModule.Update(uiContainerObj.transform);

        }

        void ApplyBaseSkin()
        {

            uiContainerRect.GetComponent<Rectangle>().Sprite = CodenameDockingElements.Instance.baseUISkin.uiContainerBackground;
            uiContainerRect.GetComponent<Rectangle>().color = CodenameDockingElements.Instance.baseUISkin.uiContainerBackgroundColor;

            uiContainerRect.GetComponent<Rectangle>().RoundedProperties.BLRadius = CodenameDockingElements.Instance.baseUISkin.uiContainerRoundness.left;
            uiContainerRect.GetComponent<Rectangle>().RoundedProperties.TLRadius = CodenameDockingElements.Instance.baseUISkin.uiContainerRoundness.right;
            uiContainerRect.GetComponent<Rectangle>().RoundedProperties.TRRadius = CodenameDockingElements.Instance.baseUISkin.uiContainerRoundness.top;
            uiContainerRect.GetComponent<Rectangle>().RoundedProperties.BRRadius = CodenameDockingElements.Instance.baseUISkin.uiContainerRoundness.bottom;

        }

    }


    public enum UserInterfaceContainerModuleType
    {

        customModules,
        custom,
        textRightImageLeft,
        textLeftImageRight,
        textBox,
        imageBox,
        numericList,
        textList,
        bulletPoints

    }

}