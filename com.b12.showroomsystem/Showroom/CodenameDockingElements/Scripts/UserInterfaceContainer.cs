using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    }

}