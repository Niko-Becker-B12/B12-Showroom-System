using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Showroom
{
    [InlineEditor]
    public class ShowroomProductGroupManager : MonoBehaviour
    {

        public List<ShowroomProductGroup> productGroups = new List<ShowroomProductGroup>();

        [ReadOnly]
        public List<ShowroomProduct> products = new List<ShowroomProduct>();


        [ReadOnly]
        public int currentSelectedGroup = -1;

        [ReadOnly]
        public int currentSelectedProduct = -1;

        [ReadOnly]
        public int lastSelectedGroup = -1;

        [ReadOnly]
        public int lastSelectedProduct = -1;


        private void Start()
        {

            if(products.Count == 0)
            {

                int currentProductIDIndex = 0;

                for (int i = 0; i < productGroups.Count; i++)
                {

                    for (int j = 0; j < productGroups[i].products.Count; j++)
                    {

                        products.Add(productGroups[i].products[j]);
                        productGroups[i].products[j].productId = currentProductIDIndex;

                        currentProductIDIndex++;

                    }

                }

            }

            for(int i = 0; i < products.Count; i++)
            {

                products[i].DisableHighlight();
                products[i].DisableInfoButtons();

                products[i].ResetColliders();

            }

        }

        [Button]
        void GenerateProductIDs()
        {

            products.Clear();

            int currentProductIDIndex = 0;

            for (int i = 0; i < productGroups.Count; i++)
            {

                for (int j = 0; j < productGroups[i].products.Count; j++)
                {

                    products.Add(productGroups[i].products[j]);
                    productGroups[i].products[j].productId = currentProductIDIndex;

                    currentProductIDIndex++;

                }

            }

        }

        public void SelectLastProduct()
        {

            if(lastSelectedProduct == currentSelectedProduct)
                SelectProduct(lastSelectedProduct);
            else
                SelectProduct(-1);

        }

        public void SelectLastGroup()
        {
            if (lastSelectedGroup == currentSelectedGroup)
                SelectGroup(lastSelectedGroup);
            else if (lastSelectedProduct == -1)
                SelectGroup(-1);
            else
                SelectGroup(currentSelectedGroup);

        }

        [Button]
        public void SelectGroup(int index = -1)
        {

            if(index == -1)
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    for(int j = 0; j < productGroups[i].products.Count; j++)
                    {

                        productGroups[i].products[j].DisableHighlight();
                        productGroups[i].products[j].DisableInfoButtons();

                        productGroups[i].products[j].ResetColliders();

                    }

                }

                lastSelectedGroup = -1;
                currentSelectedGroup = -1;

                currentSelectedProduct = -1;
                lastSelectedProduct = -1;

                ShowroomManager.Instance.MoveToFixedPos(-1);

            }
            else
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    if(i == index)
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            productGroups[i].products[j].EnableHighlight();
                            productGroups[i].products[j].DisableInfoButtons();

                            productGroups[i].products[j].ResetColliders();

                        }

                        lastSelectedGroup = currentSelectedGroup;

                        currentSelectedGroup = i;

                        currentSelectedProduct = -1;
                        lastSelectedProduct = -1;

                        if(productGroups[i].groupCamera != null)
                        {

                            if (ShowroomManager.Instance.useCaseIndex == -1)
                            {

                                ShowroomManager.Instance.OnNewCamPos(productGroups[i].groupCamera.transform);

                            }
                            else
                            {

                                ShowroomManager.Instance.OnNewCamPos(productGroups[i].groupCamera.transform);

                            }

                        }

                    }
                    else
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            productGroups[i].products[j].DisableHighlight();
                            productGroups[i].products[j].DisableInfoButtons();

                            productGroups[i].products[j].ResetColliders();

                        }

                    }

                }

            }

        }

        [Button]
        public void SelectProduct(int index = -1)
        {

            Debug.Log($"Selecting Product {index}");

            if (index == -1)
            {

                for (int i = 0; i < products.Count; i++)
                {

                    products[i].DisableHighlight();
                    products[i].DisableInfoButtons();

                    products[i].ResetColliders();

                    Debug.Log($"Disabling Product {i}");

                }

                lastSelectedGroup = -1;
                lastSelectedProduct = -1;

                currentSelectedGroup = -1;
                currentSelectedProduct = -1;

            }
            else
            {

                for (int i = 0; i < products.Count; i++)
                {

                    if (i == index)
                    {

                        products[i].EnableHighlight();
                        products[i].EnableInfoButtons();

                        if(lastSelectedProduct == index)
                        {

                            for (int k = 0; k < products[i].secondClickColliders.Count; k++)
                            {

                                products[i].secondClickColliders[k].enabled = true;

                            }

                            //products[i].productBehavior.ResetClick();

                        }

                        Debug.Log($"Enabling Product1 {i} - last selected {lastSelectedProduct} - current {currentSelectedProduct}");

                        lastSelectedProduct = currentSelectedProduct;

                        currentSelectedProduct = i;

                        for (int j = 0; j < productGroups.Count; j++)
                        {

                            if(productGroups[j].products.Contains(products[i]))
                            {

                                lastSelectedGroup = currentSelectedGroup;

                                currentSelectedGroup = j;

                            }

                        }

                        Debug.Log($"Enabling Product2 {i} - last selected {lastSelectedProduct} - current {currentSelectedProduct}");

                        if (productGroups[lastSelectedGroup].groupCamera != null)
                            ShowroomManager.Instance.OnNewCamPos(productGroups[lastSelectedGroup].groupCamera.transform);
                        else if (productGroups[lastSelectedGroup].groupCameraIndex != -1)
                            ShowroomManager.Instance.MoveToFixedPos(productGroups[lastSelectedGroup].groupCameraIndex);

                        lastSelectedProduct = currentSelectedProduct;

                            continue;

                    }
                    else
                    {

                        products[i].DisableHighlight();
                        products[i].DisableInfoButtons();

                        products[i].ResetColliders();

                        Debug.Log($"Disabling Product {i}");

                    }

                }

            }

        }

        [Button]
        public void SelectProductInGroup(int groupIndex, int productIndex)
        {

            if (groupIndex == -1)
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    Debug.Log($"Disabling Group {i}");

                    for (int j = 0; j < productGroups[i].products.Count; j++)
                    {

                        productGroups[i].products[j].DisableHighlight();
                        productGroups[i].products[j].DisableInfoButtons();

                        productGroups[i].products[j].ResetColliders();

                        Debug.Log($"Disabling Product {j}");

                    }

                }

                lastSelectedGroup = -1;
                lastSelectedProduct = -1;

                currentSelectedGroup = -1;
                currentSelectedProduct = -1;

            }
            else
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    if (i == groupIndex)
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            if(j == productIndex)
                            {

                                productGroups[i].products[j].EnableHighlight();
                                productGroups[i].products[j].EnableInfoButtons();

                                Debug.Log($"Enabling Product {j}");

                                lastSelectedGroup = currentSelectedGroup;
                                lastSelectedProduct = currentSelectedProduct;

                                currentSelectedGroup = i;
                                currentSelectedProduct = j;

                            }
                            else
                            {

                                productGroups[i].products[j].DisableHighlight();
                                productGroups[i].products[j].DisableInfoButtons();

                                productGroups[i].products[j].ResetColliders();

                                Debug.Log($"Disabling Product {j}");

                            }

                        }

                    }
                    else
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            productGroups[i].products[j].DisableHighlight();
                            productGroups[i].products[j].DisableInfoButtons();

                            productGroups[i].products[j].ResetColliders();

                            Debug.Log($"Disabling Product {j}");

                        }

                    }

                }

            }

        }

        public void SelectProductInGroupViaString(string index)
        {

            string[] s = index.Split(';');

            int groupIndex = -1;
            int productIndex = -1;

            int.TryParse(s[0], out groupIndex);
            int.TryParse(s[1], out productIndex);

            if (groupIndex == -1)
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    for (int j = 0; j < productGroups[i].products.Count; j++)
                    {

                        productGroups[i].products[j].DisableHighlight();
                        productGroups[i].products[j].DisableInfoButtons();

                        productGroups[i].products[j].ResetColliders();

                    }

                }

                lastSelectedGroup = -1;
                lastSelectedProduct = -1;

                currentSelectedGroup = -1;
                currentSelectedProduct = -1;

            }
            else
            {

                for (int i = 0; i < productGroups.Count; i++)
                {

                    if (i == groupIndex)
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            if (j == productIndex)
                            {

                                productGroups[i].products[j].EnableHighlight();
                                productGroups[i].products[j].EnableInfoButtons();

                                lastSelectedGroup = currentSelectedGroup;
                                lastSelectedProduct = currentSelectedProduct;

                                currentSelectedGroup = i;
                                currentSelectedProduct = j;

                            }
                            else
                            {

                                productGroups[i].products[j].DisableHighlight();
                                productGroups[i].products[j].DisableInfoButtons();

                                productGroups[i].products[j].ResetColliders();

                            }

                        }

                    }
                    else
                    {

                        for (int j = 0; j < productGroups[i].products.Count; j++)
                        {

                            productGroups[i].products[j].DisableHighlight();
                            productGroups[i].products[j].DisableInfoButtons();

                            productGroups[i].products[j].ResetColliders();

                        }

                    }

                }

            }

        }

    }

    [Serializable]
    public class ShowroomProductGroup
    {

        public string groupName;

        public string posNumber;

        public Camera groupCamera;

        public int groupCameraIndex;

        public List<ShowroomProduct> products = new List<ShowroomProduct>();

    }

}