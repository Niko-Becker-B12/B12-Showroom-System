using Showroom.UI;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Showroom
{

    public class ShowroomProductCreatorEditorWindow : OdinEditorWindow
    {

        public GameObject product;

        public GameObject productParent;

        public List<Collider> firstClickColliders = new List<Collider>();
        public List<Collider> secondClickColliders = new List<Collider>();

        public List<GameObject> highlightableObjects = new List<GameObject>();


        ShowroomProduct showroomProduct;


        [MenuItem("Window/Showroom/Product Creator")]
        private static void OpenWindow()
        {

            GetWindow<ShowroomProductCreatorEditorWindow>().Show();

        }

        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left", 0)]
        [Button("Clear")]
        public void Clear()
        {

            product = null;
            productParent = null;

            firstClickColliders.Clear();
            secondClickColliders.Clear();

            highlightableObjects.Clear();

            showroomProduct = null;

        }

        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right", 1)]
        [Button("Create Product")]
        public void CreateProduct()
        {

            if (product == null || productParent == null)
            {

                return;

            }

            if (productParent != null)
            {

                if (!productParent.GetComponent<ButtonBehavior>())
                    productParent.AddComponent<ButtonBehavior>().clickableOnce = true;
                if (firstClickColliders.Count == 0 && !productParent.GetComponent<Collider>())
                {

                    productParent.AddComponent<BoxCollider>();
                    productParent.GetComponent<BoxCollider>().size = new Vector3(.3f, .3f, .3f);

                    firstClickColliders.Add(productParent.GetComponent<Collider>());

                }

                if (!product.GetComponent<ButtonBehavior>())
                    product.AddComponent<ButtonBehavior>();
                if (secondClickColliders.Count == 0 && !product.GetComponent<Collider>())
                {

                    product.AddComponent<BoxCollider>();
                    product.GetComponent<BoxCollider>().size = new Vector3(.3f, .3f, .3f);
                    product.GetComponent<Collider>().enabled = false;

                    secondClickColliders.Add(product.GetComponent<Collider>());

                }
                if (highlightableObjects.Count == 0)
                {

                    foreach (Transform child in product.transform)
                    {

                        highlightableObjects.Add(child.gameObject);

                        foreach (Transform childChild in child.transform)
                        {

                            highlightableObjects.Add(childChild.gameObject);

                        }

                    }

                }
                if (!product.GetComponent<ShowroomProduct>())
                {

                    product.AddComponent<ShowroomProduct>();

                    product.GetComponent<ButtonBehavior>().clickableOnce = true;
                    productParent.GetComponent<ButtonBehavior>().clickableOnce = true;

                    showroomProduct = product.GetComponent<ShowroomProduct>();

                    showroomProduct.firstClickColliders.AddRange(firstClickColliders);
                    showroomProduct.secondClickColliders.AddRange(secondClickColliders);

                    showroomProduct.highlightObjects.AddRange(highlightableObjects);

                }
                else
                {

                    showroomProduct = product.GetComponent<ShowroomProduct>();

                    showroomProduct.firstClickColliders.AddRange(firstClickColliders);
                    showroomProduct.secondClickColliders.AddRange(secondClickColliders);

                    showroomProduct.highlightObjects.AddRange(highlightableObjects);

                }

            }

        }

    }

}