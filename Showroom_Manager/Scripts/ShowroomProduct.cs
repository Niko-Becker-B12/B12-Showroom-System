using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showroom.UI;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Showroom.WorldSpace;

namespace Showroom
{

    [Sirenix.OdinInspector.InlineEditor]
    public class ShowroomProduct : MonoBehaviour
    {

        [FoldoutGroup("General Product Info")]
        public string productName;

        [FoldoutGroup("General Product Info")]
        public string productSubtitle;

        [FoldoutGroup("General Product Info")]
        public int productId;

        [FoldoutGroup("General Product Info")]
        public bool usesCameraFromList = true;

        [FoldoutGroup("General Product Info")]
        [EnableIf("usesCameraFromList")]
        public int cameraID;

        [FoldoutGroup("General Product Info")]
        [DisableIf("usesCameraFromList")]
        public Camera cameraPos;

        [FoldoutGroup("General Product Info")]
        public Transform productFocusPoint;

        [FoldoutGroup("General Product Info")]
        public ButtonBehavior parentBehavior;

        [FoldoutGroup("General Product Info")]
        public ButtonBehavior productBehavior;

        [FoldoutGroup("General Product Info/Fairtouch")]
        public string posNumber;


        [FoldoutGroup("Colliders")]
        public List<Collider> firstClickColliders = new List<Collider>();

        [FoldoutGroup("Colliders")]
        public List<Collider> secondClickColliders = new List<Collider>();

        [FoldoutGroup("Highlightable Objects")]
        public List<GameObject> highlightObjects = new List<GameObject>();

        [FoldoutGroup("Additional Info Buttons")]
        public List<GameObject> infoButtons = new List<GameObject>();


        [FoldoutGroup("General Product Info/Events")]
        [FoldoutGroup("General Product Info/Events/Parent")]
        public List<Function> parentClickFunctions = new List<Function>();

        [FoldoutGroup("General Product Info/Events/Parent")]
        public List<Function> parentResetFunctions = new List<Function>();

        [FoldoutGroup("General Product Info/Events/Product")]
        public List<Function> productClickFunction = new List<Function>();

        [FoldoutGroup("General Product Info/Events/Product")]
        public List<Function> productResetFunction = new List<Function>();


        void Start()
        {

            productBehavior = this.GetComponent<ButtonBehavior>();
            parentBehavior = this.transform.parent.GetComponent<ButtonBehavior>();

            for (int i = 0; i < infoButtons.Count; i++)
            {

                infoButtons[i].SetActive(false);

            }

            #region Product Behavior


            UnityEvent onProductClickEvent = new UnityEvent();

            onProductClickEvent.AddListener(delegate
            {

                DisableHighlight();

                if (this.GetComponent<ShowroomWorldSpaceRotatable>())
                    this.GetComponent<ShowroomWorldSpaceRotatable>().enabled = true;

                ShowroomManager.Instance.FocusOntoObject(this.gameObject);
                ShowroomManager.Instance.showroomUI.ToggleFocusMenu(true);

                DisableInfoButtons();

            });

            Function onProductClickFunction = new Function
            {
                functionName = onProductClickEvent,
                functionDelay = 1f
            };

            if (productBehavior != null)
            {

                productBehavior.onMouseDown.Add(onProductClickFunction);
                productBehavior.onMouseDown.AddRange(productClickFunction);

            }

            UnityEvent onProductResetEvent = new UnityEvent();

            onProductResetEvent.AddListener(delegate
            {

                if (usesCameraFromList)
                    ShowroomManager.Instance.MoveToFixedPos(cameraID);
                else if(!usesCameraFromList && cameraPos != null)
                    ShowroomManager.Instance.OnNewCamPos(cameraPos.transform);

            });

            Function onProductResetFunction = new Function
            {
                functionName = onProductResetEvent,
                functionDelay = 0f
            };

            if (productBehavior != null)
            {

                productBehavior.onButtonReset.Add(onProductResetFunction);
                productBehavior.onButtonReset.AddRange(productResetFunction);

            }


            #endregion

            #region Parent Behavior


            UnityEvent onParentClickEvent = new UnityEvent();

            onParentClickEvent.AddListener(delegate
            {

                for (int i = 0; i < highlightObjects.Count; i++)
                {

                    highlightObjects[i].layer = 10;

                }

                for (int i = 0; i < firstClickColliders.Count; i++)
                {

                    firstClickColliders[i].enabled = false;

                }

                for (int i = 0; i < secondClickColliders.Count; i++)
                {

                    secondClickColliders[i].enabled = true;

                }

                EnableInfoButtons();

                if(ShowroomManager.Instance.useCaseIndex == -1)
                {

                    ShowroomManager.Instance.focusObjectPosition = productFocusPoint;

                }
                else
                {

                    ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].focusObjectPosition = productFocusPoint;

                }
                
                if(productBehavior.wasClicked)
                    productBehavior.ResetClick();

                if (usesCameraFromList)
                    ShowroomManager.Instance.MoveToFixedPos(cameraID);
                else if (!usesCameraFromList && cameraPos != null)
                    ShowroomManager.Instance.OnNewCamPos(cameraPos.transform);

            });

            Function onParentClickFucntion = new Function
            {
                functionName = onParentClickEvent,
                functionDelay = 0f
            };


            if (parentBehavior != null)
            {

                parentBehavior.onMouseDown.Add(onParentClickFucntion);
                parentBehavior.onMouseDown.AddRange(parentClickFunctions);


            }



            UnityEvent onParentResetEvent = new UnityEvent();

            onParentResetEvent.AddListener(delegate
            {

                for (int i = 0; i < firstClickColliders.Count; i++)
                {

                    firstClickColliders[i].enabled = true;

                }

                for (int i = 0; i < secondClickColliders.Count; i++)
                {

                    secondClickColliders[i].enabled = false;

                }

                //DisableInfoButtons();

            });

            Function onParentResetFunction = new Function
            {
                functionName = onParentResetEvent,
                functionDelay = 0f
            };

            if (parentBehavior != null)
            {

                parentBehavior.onButtonReset.Add(onParentResetFunction);
                parentBehavior.onButtonReset.AddRange(parentResetFunctions);


            }

            #endregion

        }

        public void EnableHighlight()
        {

            for (int i = 0; i < highlightObjects.Count; i++)
            {

                highlightObjects[i].layer = 10;

            }

        }

        public void DisableHighlight()
        {

            for (int i = 0; i < highlightObjects.Count; i++)
            {

                highlightObjects[i].layer = 0;

            }

        }

        public void ResetColliders()
        {

            for (int i = 0; i < secondClickColliders.Count; i++)
            {

                secondClickColliders[i].enabled = false;

            }

            for (int i = 0; i < firstClickColliders.Count; i++)
            {

                firstClickColliders[i].enabled = true;

            }

            if (this.GetComponent<ShowroomWorldSpaceRotatable>())
                this.GetComponent<ShowroomWorldSpaceRotatable>().enabled = false;


            if (parentBehavior != null)
            {

                parentBehavior.ResetClick();

            }

            //productBehavior.ResetClick();

        }

        public void DisableInfoButtons()
        {

            for (int i = 0; i < infoButtons.Count; i++)
            {

                infoButtons[i].SetActive(false);

            }

        }

        public void EnableInfoButtons()
        {

            for (int i = 0; i < infoButtons.Count; i++)
            {

                infoButtons[i].SetActive(true);

            }

        }

    }

}