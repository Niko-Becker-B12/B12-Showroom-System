using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Showroom.UI
{

    [System.Serializable]
    [Sirenix.OdinInspector.InlineEditor]
    [CreateAssetMenu(fileName = "UI-Container", menuName = "Showroom/UI-Container")]
    public class UserInterfaceContainer : ScriptableObject
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

        public UserInterfaceContainerType userInterfaceContainerType;


        public GameObject uiContainerObj;


        public virtual void CreateUIContainer()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log(string.Format("Generating UI-Container with the name: {0}!", uiContainerShortName));

            uiContainerObj = GameObject.Instantiate(CodenameDockingElements.Instance.uiContainerPrefab, CodenameDockingElements.Instance.uiContainerParent) as GameObject;

            uiContainerRect = uiContainerObj.GetComponent<RectTransform>();


            uiContainerRect.anchoredPosition = uiContainerClosedPosition;

            ApplyBaseSkin();



        }

        public virtual void UpdateUIContainer()
        {



        }

        void ApplyBaseSkin()
        {

            uiContainerRect.GetComponent<Image>().sprite = CodenameDockingElements.Instance.baseUISkin.uiContainerBackground;
            uiContainerRect.GetComponent<Image>().color = CodenameDockingElements.Instance.baseUISkin.uiContainerBackgroundColor;

        }

    }


    public enum UserInterfaceContainerType
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