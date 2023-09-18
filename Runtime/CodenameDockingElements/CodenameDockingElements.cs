using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Serialization;
#endif

namespace Showroom.UI
{

    public class CodenameDockingElements : SerializedMonoBehaviour
    {

        #region Singleton


        public static CodenameDockingElements Instance;

        private void Awake()
        {

            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

        }


        #endregion

        [InlineEditor] public BaseUISkin baseUISkin;

        [ReadOnly]
        public List<UserInterfaceContainer> uiContainers = new List<UserInterfaceContainer>();

        [ReadOnly]
        public List<SidebarHeadButton> headButtons = new List<SidebarHeadButton>();

        [ReadOnly]
        public List<CustomGeneralMenuModule> CustomGeneralMenuModules = new List<CustomGeneralMenuModule>();

        [ReadOnly]
        public List<ButtonBehavior> spawnedStepperButtons = new List<ButtonBehavior>();

        [BoxGroup("Scale Settings")][Tooltip("Value is given in Diagonal-Inch")][SerializeField] float screenScaleValue;
        [BoxGroup("Scale Settings")][SerializeField] CanvasScaler showroomUICanvasScaler;
        [BoxGroup("Scale Settings")][SerializeField] AnimationCurve scaleFactor;
        [BoxGroup("Scale Settings")][SerializeField] Vector2 currentResolution;

        [BoxGroup("Sidebar Settings")][SerializeField] public RectTransform sidebarContainerRect;
        [BoxGroup("Sidebar Settings")][SerializeField] public GameObject sidebarHeadButtonPrefab;
        [BoxGroup("Sidebar Settings")][SerializeField] public GameObject sidebarButtonPrefab;
        [BoxGroup("Sidebar Settings")][SerializeField] public RectTransform sidebarButtonParent;
        [BoxGroup("Sidebar Settings/Sidebar Header")][SerializeField] public TextMeshProUGUI sidebarHeaderTitle;
        [BoxGroup("Sidebar Settings/Sidebar Header")][SerializeField] public TextMeshProUGUI sidebarHeaderSubTitle;
        [BoxGroup("Sidebar Settings")][ReadOnly] public bool sidebarIsOpen = false;

        [BoxGroup("General Menu Settings")][SerializeField] public RectTransform generalMenuContainerRect;
        [BoxGroup("General Menu Settings")][SerializeField] public RectTransform generalMenuButtonParent;
        [BoxGroup("General Menu Settings")][ReadOnly] public bool generalMenuOneButtonActive = false;
        [BoxGroup("General Menu Settings")][ReadOnly] public bool generalMenuIsOpen = false;

        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuPlayButton = new CustomGeneralMenuModule
        {

            tooltipText = "Play Animation",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuReplayButton = new CustomGeneralMenuModule
        {

            tooltipText = "Restart Animation",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuCameraDropdown = new CustomGeneralMenuModule_Dropdown
        {

            tooltipText = "",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuTransparencyToggle = new CustomGeneralMenuModule
        {


            tooltipText = "Toggle Transparency Mode",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuDragModeToggle = new CustomGeneralMenuModule
        {

            tooltipText = "Toggle Drag Mode",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuBackButton = new CustomGeneralMenuModule
        {

            tooltipText = "Back",
            isIndexed = false

        };
        [BoxGroup("General Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule generalMenuHomeButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Return to Home-Position",
            isIndexed = false

        };

        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuButtonPrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuTogglePrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuSliderPrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuDropdownPrefab;


        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public RectTransform tooltipRect;
        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public Rectangle tooltipShape;
        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public TextMeshProUGUI tooltipTextbox;


        [BoxGroup("Home Menu Settings")][SerializeField] public RectTransform homeMenuContainerRect;
        [BoxGroup("Home Menu Settings")][SerializeField] public RectTransform homeMenuButtonParent;
        [BoxGroup("Home Menu Settings")][ReadOnly] public bool homeMenuIsOpen = false;

        [BoxGroup("Focus Menu Settings")][SerializeField] public RectTransform focusMenuContainerRect;
        [BoxGroup("Focus Menu Settings")][SerializeField] public RectTransform focusMenuButtonParent;
        [BoxGroup("Focus Menu Settings")][ReadOnly] public bool focusMenuIsOpen = false;

        [BoxGroup("Focus Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule focusMenuResetRotationButton = new CustomGeneralMenuModule
        {

            tooltipText = "Reset Rotation",
            isIndexed = false

        };
        [BoxGroup("Focus Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule focusMenuBackButton = new CustomGeneralMenuModule
        {

            tooltipText = "Return Object",
            isIndexed = false

        };
        [BoxGroup("Focus Menu Settings")]
        [SerializeField]
        public CustomGeneralMenuModule focusMenuResetRotationButtonNoRotationButton = new CustomGeneralMenuModule
        {

            tooltipText = "Reset Rotation",
            isIndexed = false

        };

        [BoxGroup("Focus Menu Settings/Header")][SerializeField] public RectTransform focusMenuHeaderContainerRect;

        [BoxGroup("Focus Menu Settings/Header")][SerializeField] public TextMeshProUGUI focusMenuHeaderTitle;
        [BoxGroup("Focus Menu Settings/Header")][SerializeField] public TextMeshProUGUI focusMenuHeaderSubtitle;


        [BoxGroup("Custom UI Containers")] public GameObject uiContainerPrefab;
        [BoxGroup("Custom UI Containers")] public Transform uiContainerParent;

        [BoxGroup("Custom UI Containers")] public GameObject uiContainerButtonPrefab;
        [BoxGroup("Custom UI Containers")] public GameObject uiContainerHeadlinePrefab;
        [BoxGroup("Custom UI Containers")] public GameObject uiContainerTextBoxPrefab;


        private void Start()
        {

            currentResolution = new Vector2(Screen.width, Screen.height);

            if (!ShowroomManager.Instance.isOnlyLevelInBuild && !ShowroomManager.Instance.downloadSSPData)
            {

                GetData();

                FinishedGettingData();

            }


        }

        [Button]
        void ResetGeneralMenuModules()
        {



        }

        public bool GetData()
        {

            ApplyBaseSkin();

            CreateGeneralMenuBaseModules();

            UpdateSidebarHeader();

            GenerateShowroomExtensions();

            CreateSidebarButtons();

            GenerateCustomGeneralMenuModules();

            GenerateBaseFocusMenuModules();

            CreateCustomUIContainers();

            return true;

        }

        public void FinishedGettingData()
        {

            ToggleSidebar(true);

            if(ShowroomManager.Instance.subLevelHasGeneralMenu)
                ToggleGeneralMenu(true);

        }

        void CreateGeneralMenuBaseModules()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Generating General Menu Default Modules");


            if(ShowroomManager.Instance.useCaseIndex == -1)
            {

                Debug.Log("No Use Case Selected");

                if(ShowroomManager.Instance.hasPlayButton)
                {

                    generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                    Debug.Log("Generating Play Button");

                    generalMenuPlayButton.SetUpButton(generalMenuPlayButton, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.hasRestartButton)
                {

                    generalMenuReplayButton = ShowroomManager.Instance.generalMenuReplayButton;

                    generalMenuReplayButton.SetUpButton(generalMenuReplayButton, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.hasTransparencyButton)
                {

                    generalMenuTransparencyToggle = ShowroomManager.Instance.generalMenuTransparencyToggle;

                    generalMenuTransparencyToggle.SetUpButton(generalMenuTransparencyToggle, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.hasCameraPosButton)
                {

                    generalMenuCameraDropdown = ShowroomManager.Instance.generalMenuCameraDropdown;

                    generalMenuCameraDropdown.SetUpButton(generalMenuCameraDropdown, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.hasDragModeButton)
                {

                    generalMenuDragModeToggle = ShowroomManager.Instance.generalMenuDragModeToggle;

                    generalMenuDragModeToggle.SetUpButton(generalMenuDragModeToggle, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.hasResetCameraButton)
                {

                    generalMenuBackButton = new CustomGeneralMenuModule_Button
                    {

                        buttonSprite = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).buttonSprite,
                        tooltipText = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).tooltipText,
                        isIndexed = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).isIndexed,

                        buttonOnClickFunctions = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions,
                        buttonOnEnterFunctions = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnEnterFunctions,
                        buttonOnExitFunctions = (ShowroomManager.Instance.generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnExitFunctions

                    };

                    generalMenuHomeButton = new CustomGeneralMenuModule_Button
                    {

                        buttonSprite = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonSprite,
                        tooltipText = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).tooltipText,
                        isIndexed = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).isIndexed,

                        buttonOnClickFunctions = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions,
                        buttonOnEnterFunctions = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnEnterFunctions,
                        buttonOnExitFunctions = (ShowroomManager.Instance.generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnExitFunctions

                    };

                    UnityEvent backButtonOnclick = new UnityEvent();

                    backButtonOnclick.AddListener(() =>
                    {

                        if (ShowroomManager.Instance.useCaseIndex != -1)
                        {

                            if (ShowroomManager.Instance.isAtUseCaseHomePos && !headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarSubButtonsAreActive())
                                ShowroomManager.Instance.SwitchUseCase(-2);
                            else if (!ShowroomManager.Instance.isAtUseCaseHomePos)
                                ShowroomManager.Instance.MoveToFixedPos(-1);
                            else if (ShowroomManager.Instance.isAtUseCaseHomePos && headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarSubButtonsAreActive())
                            {

                                headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.ResetSubButtons();
                                headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarButtonObjectOnClick();

                            }

                        }
                        else
                        {

                            if (ShowroomManager.Instance.isAtUseCaseHomePos)
                                ShowroomManager.Instance.SwitchUseCase(-2);
                            else
                                ShowroomManager.Instance.MoveToFixedPos(-1);

                        }


                    });

                    Function backButtonOnClickFunction = new Function
                    {
                        functionName = backButtonOnclick,
                        functionDelay = 0f
                    };

                    (generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(backButtonOnClickFunction);


                    UnityEvent homeButtonOnclick = new UnityEvent();

                    homeButtonOnclick.AddListener(() =>
                    {

                        ShowroomManager.Instance.SwitchUseCase(-2);


                        if (ShowroomManager.Instance.useCaseIndex != -1)
                            ToggleGeneralMenu(true);

                        //ShowroomManager.Instance.MoveToFixedPos(-2);

                    });

                    Function homeButtonOnclickFunction = new Function
                    {
                        functionName = homeButtonOnclick,
                        functionDelay = 0f
                    };

                    (generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(homeButtonOnclickFunction);

                    generalMenuBackButton.SetUpButton(generalMenuBackButton, -1, -1, homeMenuButtonParent);
                    generalMenuHomeButton.SetUpButton(generalMenuHomeButton, -1, -1, homeMenuButtonParent);

                }

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log("Finished generating General Menu Default Modules");

                ShowroomManager.Instance.generalMenuPlayButton = generalMenuPlayButton;
                ShowroomManager.Instance.generalMenuReplayButton = generalMenuReplayButton;
                ShowroomManager.Instance.generalMenuCameraDropdown = generalMenuCameraDropdown;
                ShowroomManager.Instance.generalMenuTransparencyToggle = generalMenuTransparencyToggle;
                ShowroomManager.Instance.generalMenuDragModeToggle = generalMenuDragModeToggle;
                ShowroomManager.Instance.generalMenuBackButton = generalMenuBackButton;
                ShowroomManager.Instance.generalMenuHomeButton = generalMenuHomeButton;

                return;

            }
            else
            {

                Debug.Log($"Use Case {ShowroomManager.Instance.useCaseIndex} Selected");

                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton)
                {

                    generalMenuPlayButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuPlayButton;

                    generalMenuPlayButton.SetUpButton(generalMenuPlayButton, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton)
                {

                    generalMenuReplayButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuReplayButton;

                    generalMenuReplayButton.SetUpButton(generalMenuReplayButton, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton)
                {

                    generalMenuTransparencyToggle = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuTransparencyToggle;

                    generalMenuTransparencyToggle.SetUpButton(generalMenuTransparencyToggle, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton)
                {

                    generalMenuCameraDropdown = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuCameraDropdown;

                    generalMenuCameraDropdown.SetUpButton(generalMenuCameraDropdown, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton)
                {

                    generalMenuDragModeToggle = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuDragModeToggle;

                    generalMenuDragModeToggle.SetUpButton(generalMenuDragModeToggle, -1, -1, generalMenuButtonParent);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton)
                {

                    generalMenuBackButton = new CustomGeneralMenuModule_Button
                    {

                        buttonSprite = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).buttonSprite,
                        tooltipText = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).tooltipText,
                        isIndexed = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).isIndexed,

                        buttonOnClickFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions,
                        buttonOnEnterFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnEnterFunctions,
                        buttonOnExitFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnExitFunctions

                    };

                    generalMenuHomeButton = new CustomGeneralMenuModule_Button
                    {

                        buttonSprite = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonSprite,
                        tooltipText = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).tooltipText,
                        isIndexed = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).isIndexed,

                        buttonOnClickFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions,
                        buttonOnEnterFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnEnterFunctions,
                        buttonOnExitFunctions = (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnExitFunctions

                    };

                    UnityEvent backButtonOnclick = new UnityEvent();

                    backButtonOnclick.AddListener(() =>
                    {

                        if (ShowroomManager.Instance.useCaseIndex != -1)
                        {

                            if (ShowroomManager.Instance.isAtUseCaseHomePos && !headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarSubButtonsAreActive())
                                ShowroomManager.Instance.SwitchUseCase(-2);
                            else if (!ShowroomManager.Instance.isAtUseCaseHomePos)
                                ShowroomManager.Instance.MoveToFixedPos(-1);
                            else if (ShowroomManager.Instance.isAtUseCaseHomePos && headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarSubButtonsAreActive())
                            {

                                headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.ResetSubButtons();
                                headButtons[ShowroomManager.Instance.useCaseIndex].sidebarHeadButtonObject.SidebarButtonObjectOnClick();

                            }

                        }
                        else
                        {

                            if (ShowroomManager.Instance.isAtUseCaseHomePos)
                                ShowroomManager.Instance.SwitchUseCase(-2);
                            else
                                ShowroomManager.Instance.MoveToFixedPos(-1);

                        }


                    });

                    Function backButtonOnClickFunction = new Function
                    {
                        functionName = backButtonOnclick,
                        functionDelay = 0f
                    };

                    (generalMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(backButtonOnClickFunction);



                    UnityEvent homeButtonOnclick = new UnityEvent();

                    homeButtonOnclick.AddListener(() =>
                    {

                        ShowroomManager.Instance.SwitchUseCase(-2);


                        if (ShowroomManager.Instance.useCaseIndex != -1)
                            ToggleGeneralMenu(true);

                        //ShowroomManager.Instance.MoveToFixedPos(-2);

                    });

                    Function homeButtonOnclickFunction = new Function
                    {
                        functionName = homeButtonOnclick,
                        functionDelay = 0f
                    };

                    (generalMenuHomeButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(homeButtonOnclickFunction);

                    generalMenuBackButton.SetUpButton(generalMenuBackButton, -1, -1, homeMenuButtonParent);
                    generalMenuHomeButton.SetUpButton(generalMenuHomeButton, -1, -1, homeMenuButtonParent);

                }

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log("Finished generating General Menu Default Modules");

                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuPlayButton = generalMenuPlayButton;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuReplayButton = generalMenuReplayButton;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuCameraDropdown = generalMenuCameraDropdown;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuTransparencyToggle = generalMenuTransparencyToggle;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuDragModeToggle = generalMenuDragModeToggle;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuBackButton = generalMenuBackButton;
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuHomeButton = generalMenuHomeButton;

                return;

            }

        }

        void GenerateBaseFocusMenuModules()
        {

            if(ShowroomManager.Instance.useCaseIndex == -1)
            {

                Debug.Log("Generating Focus Menu, No Use Case Selected");

                focusMenuBackButton = ShowroomManager.Instance.focusMenuBackButton;
                focusMenuBackButton.SetUpButton(focusMenuBackButton, -1, -1, focusMenuButtonParent);

                UnityEvent focusMenuBackButtonOnclick = new UnityEvent();

                focusMenuBackButtonOnclick.AddListener(() =>
                {

                    ShowroomManager.Instance.UnfocusObject();

                    ToggleFocusMenu(false);

                });

                Function focusMenuBackButtonOnClickFunction = new Function
                {
                    functionName = focusMenuBackButtonOnclick,
                    functionDelay = 0f
                };

                (focusMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(focusMenuBackButtonOnClickFunction);

                UnityEvent focusMenuBackButtonOnclick2 = new UnityEvent();

                focusMenuBackButtonOnclick2.AddListener(() =>
                {

                    ToggleSidebar(true);

                });

                Function focusMenuBackButtonOnClickFunction2 = new Function
                {
                    functionName = focusMenuBackButtonOnclick2,
                    functionDelay = 2f
                };

                (focusMenuBackButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(focusMenuBackButtonOnClickFunction2);

                focusMenuResetRotationButton = ShowroomManager.Instance.focusMenuResetRotationButton;
                focusMenuResetRotationButton.SetUpButton(focusMenuResetRotationButton, -1, -1, focusMenuButtonParent);

                UnityEvent focusMenuResetRotButtonOnclick = new UnityEvent();

                focusMenuResetRotButtonOnclick.AddListener(() =>
                {

                    ShowroomManager.Instance.ResetFocusedObjRotation();

                    focusMenuResetRotationButton.generalMenuModuleObject.gameObject.SetActive(false);
                    focusMenuResetRotationButtonNoRotationButton.generalMenuModuleObject.gameObject.SetActive(true);

                });

                Function focusMenuResetRotButtonOnclickFunction = new Function
                {
                    functionName = focusMenuResetRotButtonOnclick,
                    functionDelay = 0f
                };

                (focusMenuResetRotationButton as CustomGeneralMenuModule_Button).buttonOnClickFunctions.Add(focusMenuResetRotButtonOnclickFunction);

                focusMenuResetRotationButtonNoRotationButton = ShowroomManager.Instance.focusMenuRotationButton;
                focusMenuResetRotationButtonNoRotationButton.SetUpButton(focusMenuResetRotationButtonNoRotationButton, -1, -1, focusMenuButtonParent);

                focusMenuResetRotationButton.generalMenuModuleObject.gameObject.SetActive(false);

            }
            else
            {

                Debug.Log($"Generating Focus Menu, Use Case {ShowroomManager.Instance.useCaseIndex} Selected");

                focusMenuBackButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].focusMenuBackButton;
                focusMenuBackButton.SetUpButton(focusMenuBackButton, -1, -1, focusMenuButtonParent);

                focusMenuResetRotationButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].focusMenuResetRotationButton;
                focusMenuResetRotationButton.SetUpButton(focusMenuResetRotationButton, -1, -1, focusMenuButtonParent);

                focusMenuResetRotationButtonNoRotationButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].focusMenuRotationButton;
                focusMenuResetRotationButtonNoRotationButton.SetUpButton(focusMenuResetRotationButtonNoRotationButton, -1, -1, focusMenuButtonParent);

                focusMenuResetRotationButton.generalMenuModuleObject.gameObject.SetActive(false);

            }

        }

        void GenerateShowroomExtensions()
        {

            for(int i = 0; i < ShowroomManager.Instance.showroomManagerExtensions.Count; i++)
            {

                List<string> extensionGroupData = ShowroomManager.Instance.showroomManagerExtensions[i].GetGroupData();
                List<string> extensionProductData = ShowroomManager.Instance.showroomManagerExtensions[i].GetSubGroupData();

            }

        }

        void CreateSidebarButtons()
        {

            for (int i = 0; i < ShowroomManager.Instance.useCases.Count; i++)
            {

                headButtons.Add(ShowroomManager.Instance.useCases[i].useCaseSidebarHeaderButton);

                headButtons[i].sidebarHeadButtonUseCaseIndex = i;

                //headButtons[i].sidebarHeadButtonSubButtons.AddRange(ShowroomManager.Instance.useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonSubButtons);
                //
                //for(int j = 0; j < headButtons[i].sidebarHeadButtonSubButtons.Count; j++)
                //{
                //
                //    headButtons[i].sidebarHeadButtonSubButtons[j].sidebarButtonUseCaseIndex = i;
                //
                //}

            }

            for (int i = 0; i < headButtons.Count; i++)
            {

                headButtons[i].SetUpButton(i);

            }

        }

        void GenerateCustomGeneralMenuModules()
        {

            if(ShowroomManager.Instance.useCaseIndex == -1)
            {

                CustomGeneralMenuModules.AddRange(ShowroomManager.Instance.CustomGeneralMenuModules);

                for (int i = CustomGeneralMenuModules.Count; i > 0; i--)
                {
                    int index = i - 1;

                    Debug.Log(index);



                    CustomGeneralMenuModules[index].SetUpButton(CustomGeneralMenuModules[index], index, -1, generalMenuButtonParent);

                }

            }
            else
            {

                //CustomGeneralMenuModules.AddRange(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].CustomGeneralMenuModules);
                //
                //for (int i = CustomGeneralMenuModules.Count; i > 0; i--)
                //{
                //    Debug.Log(i - 1);
                //
                //    CustomGeneralMenuModules[i - 1].SetUpButton(CustomGeneralMenuModules[i], i - 1);
                //
                //}

            }

        }

        void ApplyBaseSkin()
        {

            #region General Menu

            generalMenuContainerRect.GetComponent<Rectangle>().Sprite = baseUISkin.generalMenuBackground;
            generalMenuContainerRect.GetComponent<Rectangle>().color = baseUISkin.generalMenuBackgroundColor;

            generalMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;

            generalMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BLRadius = baseUISkin.generalMenuRoundness.left;
            generalMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TLRadius = baseUISkin.generalMenuRoundness.right;
            generalMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TRRadius = baseUISkin.generalMenuRoundness.top;
            generalMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BRRadius = baseUISkin.generalMenuRoundness.bottom;

            generalMenuContainerRect.GetComponent<HorizontalOrVerticalLayoutGroup>().padding = baseUISkin.generalMenuPadding;

            //generalMenuPlayButton.buttonSprite = baseUISkin.generalMenuPlayButtonIcon;
            //generalMenuReplayButton.buttonSprite = baseUISkin.generalMenuReplayButtonIcon;
            //generalMenuCameraDropdown.dropdownSprite = baseUISkin.generalMenuCameraDropdownIcon;
            //generalMenuTransparencyToggle.toggleActiveSprite = baseUISkin.generalMenuTransparencyToggleActiveIcon;
            //generalMenuTransparencyToggle.toggleDeactiveSprite = baseUISkin.generalMenuTransparencyToggleDeactiveIcon;
            //generalMenuDragModeToggle.toggleActiveSprite = baseUISkin.generalMenuDragModeToggleActiveIcon;
            //generalMenuDragModeToggle.toggleDeactiveSprite = baseUISkin.generalMenuDragModeToggleDeactiveIcon;
            //generalMenuBackButton.buttonSprite = baseUISkin.generalMenuBackButtonIcon;
            //generalMenuHomeButton.buttonSprite = baseUISkin.generalMenuHomeButtonIcon;

            #endregion

            #region Sidebar

            sidebarContainerRect.GetComponent<Rectangle>().Sprite = baseUISkin.sidebarBackground;
            sidebarContainerRect.GetComponent<Rectangle>().color = baseUISkin.sidebarBackgroundColor;

            sidebarContainerRect.transform.GetChild(1).GetChild(2).gameObject.SetActive(baseUISkin.sidebarHasScrollGradient);
            sidebarContainerRect.transform.GetChild(1).GetChild(2).GetComponent<Image>().color = baseUISkin.sidebarScrollGradientColor;

            #endregion

            #region Home Menu

            homeMenuContainerRect.GetComponent<Rectangle>().Sprite = baseUISkin.homeMenuBackground;
            homeMenuContainerRect.GetComponent<Rectangle>().color = baseUISkin.homeMenuBackgroundColor;

            homeMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;

            homeMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BLRadius = baseUISkin.homeMenuRoundness.left;
            homeMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TLRadius = baseUISkin.homeMenuRoundness.right;
            homeMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TRRadius = baseUISkin.homeMenuRoundness.top;
            homeMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BRRadius = baseUISkin.homeMenuRoundness.bottom;



            //focusMenuBackButton.buttonSprite = baseUISkin.focusMenuBackButtonIcon;
            //focusMenuResetRotationButton.buttonSprite = baseUISkin.focusMenuActualResetRotationButtonIcon;
            //focusMenuResetRotationButtonNoRotationButton.buttonSprite = baseUISkin.focusMenuResetRotationButtonIcon;

            #endregion

            #region Focus Menu

            focusMenuContainerRect.GetComponent<Rectangle>().Sprite = baseUISkin.focusMenuBackground;
            focusMenuContainerRect.GetComponent<Rectangle>().color = baseUISkin.focusMenuBackgroundColor;

            focusMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;

            focusMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BLRadius = baseUISkin.focusMenuRoundness.left;
            focusMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TLRadius = baseUISkin.focusMenuRoundness.right;
            focusMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.TRRadius = baseUISkin.focusMenuRoundness.top;
            focusMenuContainerRect.GetComponent<Rectangle>().RoundedProperties.BRRadius = baseUISkin.focusMenuRoundness.bottom;

            //focusMenuBackButton.buttonSprite = baseUISkin.focusMenuBackButtonIcon;
            //focusMenuResetRotationButton.buttonSprite = baseUISkin.focusMenuActualResetRotationButtonIcon;
            //focusMenuResetRotationButtonNoRotationButton.buttonSprite = baseUISkin.focusMenuResetRotationButtonIcon;



            #endregion

            #region Focus Menu Header

            focusMenuHeaderContainerRect.GetComponent<Rectangle>().Sprite = baseUISkin.focusMenuHeaderBackground;
            focusMenuHeaderContainerRect.GetComponent<Rectangle>().color = baseUISkin.focusMenuHeaderBackgroundColor;

            focusMenuHeaderContainerRect.GetComponent<Rectangle>().RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;

            focusMenuHeaderContainerRect.GetComponent<Rectangle>().RoundedProperties.BLRadius = baseUISkin.focusMenuHeaderRoundness.left;
            focusMenuHeaderContainerRect.GetComponent<Rectangle>().RoundedProperties.TLRadius = baseUISkin.focusMenuHeaderRoundness.right;
            focusMenuHeaderContainerRect.GetComponent<Rectangle>().RoundedProperties.TRRadius = baseUISkin.focusMenuHeaderRoundness.top;
            focusMenuHeaderContainerRect.GetComponent<Rectangle>().RoundedProperties.BRRadius = baseUISkin.focusMenuHeaderRoundness.bottom;

            //focusMenuBackButton.buttonSprite = baseUISkin.focusMenuBackButtonIcon;
            //focusMenuResetRotationButton.buttonSprite = baseUISkin.focusMenuActualResetRotationButtonIcon;
            //focusMenuResetRotationButtonNoRotationButton.buttonSprite = baseUISkin.focusMenuResetRotationButtonIcon;



            #endregion

            #region Tooltip


            tooltipShape.color = baseUISkin.tooltipBackgroundColor;
            tooltipShape.Sprite = baseUISkin.tooltipBackground;

            tooltipTextbox.color = baseUISkin.tooltipTextColor;

            tooltipShape.RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;

            tooltipShape.RoundedProperties.BLRadius = baseUISkin.tooltipRoundness.left;
            tooltipShape.RoundedProperties.TLRadius = baseUISkin.tooltipRoundness.right;
            tooltipShape.RoundedProperties.TRRadius = baseUISkin.tooltipRoundness.top;
            tooltipShape.RoundedProperties.BRRadius = baseUISkin.tooltipRoundness.bottom;


            #endregion



            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Applied currently active Skin!");

        }

        void CreateCustomUIContainers()
        {

            for (int i = 0; i < ShowroomManager.Instance.userInterfaceContainers.Count; i++)
            {

                UserInterfaceContainer newContainer = ShowroomManager.Instance.userInterfaceContainers[i];//Instantiate(ShowroomManager.Instance.userInterfaceContainers[i]);

                uiContainers.Add(newContainer);

                uiContainers[i].CreateUIContainer();

            }

        }

        public UserInterfaceContainer ReturnUIContainer(int containerName)
        {

            for (int i = 0; i < uiContainers.Count; i++)
            {

                if (uiContainers[i].uiContainerShortName.Equals(containerName))
                {

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Found UI-Container with the name: {0}!", containerName));

                    return uiContainers[i];

                }
                else
                    continue;

            }

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.LogError(string.Format("Could not find any UI-Containers with the name: {0}!", containerName));

            return null;

        }

        void UpdateUIContainer(UserInterfaceContainer tempContainer)
        {

            if (tempContainer != null)
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Updating UI-Container with the name: {0}!", tempContainer.uiContainerShortName));

                tempContainer.UpdateUIContainer();

            }
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.LogError(string.Format("There's no UI-Container with the name: {0}!", tempContainer.uiContainerShortName));

            }

        }

        public void UpdateContainerViaString(string containerName)
        {

            UserInterfaceContainer newContainer = null;

            for (int i = 0; i < uiContainers.Count; i++)
            {

                if (uiContainers[i].uiContainerShortName.Equals(containerName))
                {

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Found UI-Container with the name: {0}!", containerName));

                    newContainer = uiContainers[i];

                    break;

                }
                else
                    continue;

            }

            if (newContainer != null)
                UpdateUIContainer(newContainer);
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", containerName));

                return;

            }

        }

        public void UpdateContainerViaIndex(int containerIndex)
        {

            UserInterfaceContainer newContainer = null;

            if (uiContainers.Count > containerIndex && uiContainers[containerIndex] != null)
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Found UI-Container with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                newContainer = uiContainers[containerIndex];

            }

            if (newContainer != null)
                UpdateUIContainer(newContainer);
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                return;

            }

        }

        public void DisplayContainerViaIndex(int containerIndex)
        {

            UserInterfaceContainer newContainer = null;

            if (uiContainers.Count > containerIndex && uiContainers[containerIndex] != null)
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Found UI-Container with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                newContainer = uiContainers[containerIndex];

            }

            if (newContainer != null)
            {

                newContainer.uiContainerObj.GetComponent<RectTransform>().DOAnchorPos(newContainer.uiContainerOpenedPosition, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {



                });

            }
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                return;

            }

        }

        public void DisplayContainerViaString(string containerName)
        {

            UserInterfaceContainer newContainer = null;

            for (int i = 0; i < uiContainers.Count; i++)
            {

                if (uiContainers[i].uiContainerShortName.Equals(containerName))
                {

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Found UI-Container with the name: {0}!", containerName));

                    newContainer = uiContainers[i];

                    break;

                }
                else
                    continue;

            }

            if (newContainer != null)
            {

                newContainer.uiContainerObj.GetComponent<RectTransform>().DOAnchorPos(newContainer.uiContainerOpenedPosition, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {



                });

            }
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", containerName));

                return;

            }

        }

        public void HideContainerViaIndex(int containerIndex)
        {

            UserInterfaceContainer newContainer = null;

            if (uiContainers.Count > containerIndex && uiContainers[containerIndex] != null)
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Found UI-Container with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                newContainer = uiContainers[containerIndex];

            }

            if (newContainer != null)
            {

                newContainer.uiContainerObj.GetComponent<RectTransform>().DOAnchorPos(newContainer.uiContainerClosedPosition, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {



                });

            }
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", uiContainers[containerIndex].uiContainerShortName));

                return;

            }

        }

        public void HideContainerViaString(string containerName)
        {

            UserInterfaceContainer newContainer = null;

            for (int i = 0; i < uiContainers.Count; i++)
            {

                if (uiContainers[i].uiContainerShortName.Equals(containerName))
                {

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Found UI-Container with the name: {0}!", containerName));

                    newContainer = uiContainers[i];

                    break;

                }
                else
                    continue;

            }

            if (newContainer != null)
            {

                newContainer.uiContainerObj.GetComponent<RectTransform>().DOAnchorPos(newContainer.uiContainerClosedPosition, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {



                });

            }
            else
            {

                if (ShowroomManager.Instance.showDebugMessages)
                    Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", containerName));

                return;

            }

        }

        public void UpdateSidebarContainer()
        {

            LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarButtonParent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarButtonParent);

        }

        public void UpdateSidebarHeader()
        {

            if (ShowroomManager.Instance == null)
            {

                sidebarHeaderTitle.text = "Sub-Level-Headline<br>Sub-Level-Headline";
                sidebarHeaderSubTitle.text = "Sub-Level-Sub-Headline<br>Sub-Level-Sub-Headline";

                LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarHeaderTitle.transform.parent.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarHeaderTitle.transform.parent.GetComponent<RectTransform>());

                return;

            }

            sidebarHeaderTitle.text = ShowroomManager.Instance.subLevelName;
            sidebarHeaderSubTitle.text = ShowroomManager.Instance.subLevelSubTitle;

            LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarHeaderTitle.transform.parent.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(sidebarHeaderTitle.transform.parent.GetComponent<RectTransform>());

        }

        public void SelectSidebarButton(int index = -1)
        {

            if (index == -1)
            {

                for (int i = 0; i < headButtons.Count; i++)
                {

                    headButtons[i].sidebarHeadButtonObject.sidebarButtonBehavior.ResetClick();

                }

                return;

            }
            else
            {

                for (int i = 0; i < headButtons.Count; i++)
                {

                    if (i == index)
                    {

                        if (ShowroomManager.Instance.showDebugMessages)
                            Debug.Log(string.Format("Found selected Button | Sidebar Button: {0}", i));

                        continue;

                    }

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Updating Button Selection | Sidebar Button: {0}", i));

                    headButtons[i].sidebarHeadButtonObject.sidebarButtonBehavior.ResetClick();

                }

                return;

            }

        }

        public void ResetSidebarButton(int index = -1)
        {
            if (index == -1)
            {

                for (int i = 0; i < headButtons.Count; i++)
                {

                    headButtons[i].sidebarHeadButtonObject.sidebarButtonBehavior.ResetClick();

                }

                return;

            }
            else
            {

                for (int i = 0; i < headButtons.Count; i++)
                {

                    if (i == index)
                    {

                        if (ShowroomManager.Instance.showDebugMessages)
                            Debug.Log(string.Format("Found Button to not reset | Sidebar Button: {0}", i));

                        continue;

                    }

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Reseting Button Selection | Sidebar Button: {0}", i));

                    headButtons[i].sidebarHeadButtonObject.SidebarButtonObjectOnReset();

                }

                return;

            }

        }

        public void ResetSidebarButtonSubButtons(int index = -1)
        {

            if (index == -1)
                return;

            for (int i = 0; i < headButtons.Count; i++)
            {

                if (i != index)
                {

                    continue;

                }
                else
                {

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log(string.Format("Reseting Button Selection | Sidebar Button: {0}", i));

                    headButtons[i].sidebarHeadButtonObject.ResetSubButtons(-1);

                }

            }

        }

        public void FadeCanvasGroup(CanvasGroup canvasGroup, bool isActive)
        {

            if (isActive)
                canvasGroup.DOFade(1f, .5f)
                    .OnStart(() =>
                    {
                        canvasGroup.blocksRaycasts = true;
                        canvasGroup.interactable = true;
                    });
            else
                canvasGroup.DOFade(0f, .5f)
                    .OnComplete(() =>
                    {
                        canvasGroup.blocksRaycasts = false;
                        canvasGroup.interactable = false;
                    });

        }

        public void FadeCanvasGroupIn(CanvasGroup canvasGroup)
        {

            FadeCanvasGroup(canvasGroup, true);

        }

        public void FadeCanvasGroupOut(CanvasGroup canvasGroup)
        {

            FadeCanvasGroup(canvasGroup, false);

        }

        public void DisplayTooltip(RectTransform rectPos = null, string tooltipText = null)
        {

            tooltipRect.gameObject.SetActive(true);

            tooltipTextbox.text = tooltipText;

            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect.GetChild(0).GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect.GetChild(0).GetComponent<RectTransform>());


            if (rectPos == null || tooltipText == "" || string.IsNullOrEmpty(tooltipText))
            {

                Vector2 screenSpacePos = new Vector2(-10000f, -10000f);

                tooltipRect.position = screenSpacePos;

                tooltipShape.rectTransform.pivot = new Vector2(1f, .5f);
                tooltipShape.rectTransform.anchoredPosition = new Vector2(-41f, 0f);

                tooltipShape.transform.GetChild(1).gameObject.SetActive(false);
                tooltipRect.transform.GetChild(1).gameObject.SetActive(true);

            }
            else
            {

                if (Mouse.current.position.ReadValue().x >= (currentResolution.x * .90f))
                {

                    Vector2 screenSpacePos = rectPos.position;

                    tooltipRect.position = screenSpacePos;

                    tooltipShape.rectTransform.pivot = new Vector2(1f, .5f);
                    tooltipShape.rectTransform.anchoredPosition = new Vector2(-41f, 0f);

                    tooltipShape.transform.GetChild(1).gameObject.SetActive(false);
                    tooltipRect.transform.GetChild(1).gameObject.SetActive(true);

                }
                else
                {

                    Vector2 screenSpacePos = rectPos.position;

                    tooltipRect.position = screenSpacePos;

                    tooltipShape.rectTransform.pivot = new Vector2(.5f, .5f);
                    tooltipShape.rectTransform.anchoredPosition = new Vector2(0f, 52f);

                    tooltipShape.transform.GetChild(1).gameObject.SetActive(true);
                    tooltipRect.transform.GetChild(1).gameObject.SetActive(false);

                }

            }



        }

        public void DisableTooltip()
        {

            tooltipRect.gameObject.SetActive(false);

        }

        public void ToggleSidebar(bool isOpen)
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log(string.Format("Sidebar is toggling to: {0}!", isOpen));

            if (isOpen)
            {

                sidebarContainerRect.DOAnchorPos(baseUISkin.sidebarOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {

                    ShowroomNavigation.Instance.ShiftCameraLens(new Vector2(-.15f, 0f), baseUISkin.animationSpeed);

                })
                .OnComplete(() =>
                {

                    sidebarIsOpen = true;

                });

            }
            else
            {

                sidebarContainerRect.DOAnchorPos(baseUISkin.sidebarClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {

                    ShowroomNavigation.Instance.ShiftCameraLens(Vector2.zero, baseUISkin.animationSpeed);

                })
                .OnComplete(() =>
                {

                    sidebarIsOpen = false;

                });

            }

        }

        void UpdateSidebarButtons()
        {

            for (int i = 0; i < headButtons.Count; i++)
            {

                headButtons[i].UpdateButton();

            }

        }

        public void ToggleGeneralMenu(bool reOpenAfter = false)
        {

            DOTween.Kill(generalMenuContainerRect);
            DOTween.Kill(homeMenuContainerRect);

            if (ShowroomManager.Instance.useCaseIndex == -1)
            {

                if (generalMenuIsOpen)
                {

                    generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                    .OnStart(() =>
                    {

                        ToggleHomeMenu(false);

                    })
                    .OnComplete(() =>
                    {

                        generalMenuIsOpen = false;

                        if (reOpenAfter)
                        {

                            UpdateGeneralMenuModules();

                            if(ShowroomManager.Instance.subLevelHasGeneralMenu)
                            {

                                if (ShowroomManager.Instance.hasPlayButton
                                || ShowroomManager.Instance.hasResetCameraButton
                                || ShowroomManager.Instance.hasRestartButton
                                || ShowroomManager.Instance.hasCameraPosButton
                                || ShowroomManager.Instance.hasDragModeButton
                                || ShowroomManager.Instance.hasTransparencyButton)
                                {

                                    generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                                    .OnStart(() =>
                                    {

                                        homeMenuContainerRect.DOAnchorPos(baseUISkin.homeMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType);

                                    })
                                    .OnComplete(() =>
                                    {

                                        generalMenuIsOpen = true;

                                    });

                                }

                            }

                                

                        }

                    });

                }
                else
                {

                    if (ShowroomManager.Instance.subLevelHasGeneralMenu)
                    {

                        if (ShowroomManager.Instance.hasPlayButton
                        || ShowroomManager.Instance.hasResetCameraButton
                        || ShowroomManager.Instance.hasRestartButton
                        || ShowroomManager.Instance.hasCameraPosButton
                        || ShowroomManager.Instance.hasDragModeButton
                        || ShowroomManager.Instance.hasTransparencyButton)
                        {

                            generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                            .OnStart(() =>
                            {

                                ToggleHomeMenu(true);

                            })
                            .OnComplete(() =>
                            {

                                generalMenuIsOpen = true;

                            });

                        }

                    }

                }

            }
            else
            {

                if (generalMenuIsOpen)
                {

                    generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                    .OnStart(() =>
                    {

                        ToggleHomeMenu(false);

                    })
                    .OnComplete(() =>
                    {

                        generalMenuIsOpen = false;

                        if (reOpenAfter)
                        {

                            UpdateGeneralMenuModules();

                            if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton)
                            {

                                generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                                .OnStart(() =>
                                {

                                    ToggleHomeMenu(true);

                                })
                                .OnComplete(() =>
                                {

                                    generalMenuIsOpen = true;

                                });

                            }

                        }

                    });

                }
                else
                {

                    UpdateGeneralMenuModules();

                    if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton
                                || ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton)
                    {

                        generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                        .OnStart(() =>
                        {

                            ToggleHomeMenu(true);

                        })
                        .OnComplete(() =>
                        {

                            generalMenuIsOpen = true;

                        });

                    } 

                }

            }

        }

        public void ToggleHomeMenu(bool isOpen)
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log(string.Format("Home Menu is toggling to: {0}!", isOpen));

            if (isOpen)
            {

                foreach (Transform child in homeMenuButtonParent)
                {

                    Destroy(child.gameObject);

                }

                homeMenuContainerRect.DOAnchorPos(baseUISkin.homeMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {

                    homeMenuIsOpen = true;

                });

            }
            else
            {

                homeMenuContainerRect.DOAnchorPos(baseUISkin.homeMenuClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {

                    homeMenuIsOpen = false;

                });

            }

        }

        public void ToggleFocusMenu(bool isOpen)
        {

            if(isOpen)
            {

                foreach (Transform child in focusMenuButtonParent)
                {

                    Destroy(child.gameObject);

                }

                GenerateBaseFocusMenuModules();

                focusMenuContainerRect.DOAnchorPos(baseUISkin.focusMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType).SetDelay(baseUISkin.animationSpeed)
                .OnStart(() =>
                {

                    ToggleGeneralMenu(false);
                    ToggleHomeMenu(false);

                    if(baseUISkin.useFocusMenuHeaderSettings)
                    {

                        focusMenuHeaderTitle.text = ShowroomManager.Instance.currentlyFocusedObj.GetComponent<ShowroomProduct>().productName;
                        focusMenuHeaderSubtitle.text = ShowroomManager.Instance.currentlyFocusedObj.GetComponent<ShowroomProduct>().productSubtitle;

                        focusMenuHeaderContainerRect.DOAnchorPos(baseUISkin.focusMenuHeaderOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType);

                    }

                })
                .OnComplete(() =>
                {

                    focusMenuIsOpen = true;

                });

            }
            else
            {

                focusMenuContainerRect.DOAnchorPos(baseUISkin.focusMenuClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {

                    focusMenuHeaderContainerRect.DOAnchorPos(baseUISkin.focusMenuHeaderClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType);

                })
                .OnComplete(() =>
                {

                    focusMenuIsOpen = false;

                    ToggleGeneralMenu(true);
                    ToggleHomeMenu(true);

                });

            }

        }

        void UpdateGeneralMenuModules()
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log("Updating General Menu Modules!");

            foreach(Transform child in generalMenuButtonParent)
            {

                Destroy(child.gameObject);

            }

            CreateGeneralMenuBaseModules();

            #region old
            /*

            if (ShowroomManager.Instance.useCaseIndex == -1)
            {

                if (ShowroomManager.Instance.subLevelHasGeneralMenu)
                {

                    if (!ShowroomManager.Instance.hasPlayButton &&
                    !ShowroomManager.Instance.hasRestartButton &&
                    !ShowroomManager.Instance.hasCameraPosButton &&
                    !ShowroomManager.Instance.hasTransparencyButton &&
                    !ShowroomManager.Instance.hasDragModeButton &&
                    !ShowroomManager.Instance.hasResetCameraButton)
                    {

                        DOTween.Kill(generalMenuContainerRect);

                        generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, 0.01f).SetEase(baseUISkin.animationEasingType);

                        generalMenuIsOpen = false;

                        generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                        generalMenuReplayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                        generalMenuDragModeToggle.customGeneralMenuToggleObj.gameObject.SetActive(false);
                        generalMenuTransparencyToggle.customGeneralMenuToggleObj.gameObject.SetActive(false);
                        generalMenuCameraDropdown.customGeneralMenuDropdownObj.gameObject.SetActive(false);
                        generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                        generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);


                    }
                    else if (ShowroomManager.Instance.hasPlayButton ||
                    ShowroomManager.Instance.hasRestartButton ||
                    ShowroomManager.Instance.hasCameraPosButton ||
                    ShowroomManager.Instance.hasTransparencyButton ||
                    ShowroomManager.Instance.hasDragModeButton ||
                    ShowroomManager.Instance.hasResetCameraButton)
                    {

                        //Update Buttons

                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }

                        tempButton = generalMenuReplayButton;
                        generalMenuReplayButton = ShowroomManager.Instance.generalMenuReplayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuReplayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuReplayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuReplayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }
                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }
                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }
                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }
                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }
                        CustomGeneralMenuModule tempButton = generalMenuPlayButton;
                        generalMenuPlayButton = ShowroomManager.Instance.generalMenuPlayButton;

                        if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.button)
                            generalMenuPlayButton.buttonSprite = tempButton.buttonSprite;
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.toggle)
                        {

                            generalMenuPlayButton.toggleActiveSprite = tempButton.toggleActiveSprite;
                            generalMenuPlayButton.toggleDeactiveSprite = tempButton.toggleDeactiveSprite;

                        }
                        else if (tempButton.CustomGeneralMenuModule.customGeneralMenuModuleType == CustomGeneralMenuModuleType.dropdown)
                        {

                            generalMenuPlayButton.dropdownSprite = tempButton.dropdownSprite;
                            generalMenuPlayButton.dropdownChildSprite = tempButton.dropdownChildSprite;

                        }

                        generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.hasPlayButton);
                        generalMenuReplayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.hasRestartButton);
                        generalMenuDragModeToggle.customGeneralMenuToggleObj.gameObject.SetActive(ShowroomManager.Instance.hasDragModeButton);
                        generalMenuTransparencyToggle.customGeneralMenuToggleObj.gameObject.SetActive(ShowroomManager.Instance.hasTransparencyButton);
                        generalMenuCameraDropdown.customGeneralMenuDropdownObj.gameObject.SetActive(ShowroomManager.Instance.hasCameraPosButton);
                        generalMenuBackButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.hasResetCameraButton);
                        generalMenuHomeButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.hasResetCameraButton);

                    }

                }
                else
                {

                    DOTween.Kill(generalMenuContainerRect);

                    generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, 0.01f).SetEase(baseUISkin.animationEasingType);

                    generalMenuIsOpen = false;

                }

            }
            else
            {

                if (!ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton &&
                !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton &&
                !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton &&
                !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton &&
                !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton &&
                !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton)
                {

                    DOTween.Kill(generalMenuContainerRect);

                    generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, 0.01f).SetEase(baseUISkin.animationEasingType);

                    generalMenuIsOpen = false;

                    generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                    generalMenuReplayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                    generalMenuDragModeToggle.customGeneralMenuToggleObj.gameObject.SetActive(false);
                    generalMenuTransparencyToggle.customGeneralMenuToggleObj.gameObject.SetActive(false);
                    generalMenuCameraDropdown.customGeneralMenuDropdownObj.gameObject.SetActive(false);
                    generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);
                    generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(false);


                }
                else if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton ||
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton ||
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton ||
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton ||
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton ||
                ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton)
                {

                    //Update Buttons
                    generalMenuPlayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton);
                    generalMenuReplayButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton);
                    generalMenuDragModeToggle.customGeneralMenuToggleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton);
                    generalMenuTransparencyToggle.customGeneralMenuToggleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton);
                    generalMenuCameraDropdown.customGeneralMenuDropdownObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton);
                    generalMenuBackButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton);
                    generalMenuHomeButton.CustomGeneralMenuModuleObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton);

                }

            }

            */
            #endregion
        }

#if UNITY_EDITOR

        [Button]
        private void OnDrawGizmosSelected()
        {

            List<UserInterfaceContainer> containers = new List<UserInterfaceContainer>();
            ShowroomManager showroomManager = GameObject.Find("/--- Showroom Manager ---").GetComponent<ShowroomManager>();
            containers.AddRange(showroomManager.userInterfaceContainers);


            for (int i = 0; i < containers.Count; i++)
            {

                Random.seed = i;
                Color randomColor = Random.ColorHSV();

                Handles.color = randomColor;
                GUI.color = Handles.color;

                DrawContainerRect(containers[i], true, i);
                DrawContainerRect(containers[i], false, i);

            }

        }

#endif

        float UnitIntervalRange(float stageStartRange, float stageFinishRange, float newStartRange, float newFinishRange, float floatingValue)
        {
            float outRange = Math.Abs(newFinishRange - newStartRange);
            float inRange = Math.Abs(stageFinishRange - stageStartRange);
            float range = (outRange / inRange);
            return (newStartRange + (range * (floatingValue - stageStartRange)));
        }

#if UNITY_EDITOR

        void DrawContainerRect(UserInterfaceContainer container, bool isClosed, int index)
        {

            Rect RectInOwnSpace;
            
            RectInOwnSpace = (isClosed) ? new Rect(container.uiContainerClosedPosition, new Vector2(container.uiContainerSize.x, container.uiContainerSize.y * 1.25f)) 
                : new Rect(container.uiContainerOpenedPosition, new Vector2(container.uiContainerSize.x, container.uiContainerSize.y * 1.25f));


            Rect RectInParentSpace = RectInOwnSpace;

            Transform parentSpace = uiContainerParent;
            RectTransform guiParent = null;

            Vector2 displacementPos = Vector2.zero;

            if(isClosed)
            {

                switch (container.uiContainerDockingPosition)
                {

                    case DockingPositions.center:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.rightCenter:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.bottomRight:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x,
                            container.uiContainerClosedPosition.y);
                        break;
                    case DockingPositions.bottomCenter:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerClosedPosition.y);
                        break;
                    case DockingPositions.bottomLeft:
                        displacementPos = container.uiContainerClosedPosition;
                        break;
                    case DockingPositions.leftCenter:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.topLeft:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y);
                        break;
                    case DockingPositions.topCenter:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y);
                        break;
                    case DockingPositions.topRight:
                        displacementPos = new Vector2(container.uiContainerClosedPosition.x - container.uiContainerSize.x,
                            container.uiContainerClosedPosition.y - container.uiContainerSize.y);
                        break;

                }

            }
            else
            {

                switch (container.uiContainerDockingPosition)
                {

                    case DockingPositions.center:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.rightCenter:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.bottomRight:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x,
                            container.uiContainerOpenedPosition.y);
                        break;
                    case DockingPositions.bottomCenter:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerOpenedPosition.y);
                        break;
                    case DockingPositions.bottomLeft:
                        displacementPos = container.uiContainerOpenedPosition;
                        break;
                    case DockingPositions.leftCenter:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y / 2);
                        break;
                    case DockingPositions.topLeft:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y);
                        break;
                    case DockingPositions.topCenter:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x / 2,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y);
                        break;
                    case DockingPositions.topRight:
                        displacementPos = new Vector2(container.uiContainerOpenedPosition.x - container.uiContainerSize.x,
                            container.uiContainerOpenedPosition.y - container.uiContainerSize.y);
                        break;

                }

            }

            RectInParentSpace.position = displacementPos;

            DrawRect(RectInParentSpace, parentSpace, !isClosed);

            //Vector2 closed = rectInParentSpace.position;

            Vector2 FinalPos = new Vector2(currentResolution.x / 2 + container.uiContainerSize.x / 2 + RectInParentSpace.position.x,
                currentResolution.y / 2 + container.uiContainerSize.y / 2 + RectInParentSpace.position.y / 2);


            //Vector2 FinalPos = new Vector2(UnitIntervalRange(0, 1920, -960, 960,currentResolution.x / 2 + container.uiContainerSize.x / 2 + displacementPos.x),
            //    UnitIntervalRange(-960, 960, 0, 1920, currentResolution.y / 2 + container.uiContainerSize.y / 2 + displacementPos.y));


            if (IsSceneViewCameraInRange(FinalPos, 10000f))
            {

                GUIStyle newStyle = new GUIStyle("Tooltip");

                newStyle.alignment = TextAnchor.MiddleCenter;

                if (!isClosed)
                    Handles.Label(FinalPos, $"{container.uiContainerShortName} + Index: {index} Opened-Pos");
                else
                    Handles.Label(FinalPos, $"{container.uiContainerShortName} + Index: {index} Closed-Pos", newStyle);

            }

        }

        void DrawRect(Rect rect, Transform space, bool dotted)
        {
            Vector3 p0 = space.TransformPoint(new Vector2(rect.x, rect.y));
            Vector3 p1 = space.TransformPoint(new Vector2(rect.x, rect.yMax));
            Vector3 p2 = space.TransformPoint(new Vector2(rect.xMax, rect.yMax));
            Vector3 p3 = space.TransformPoint(new Vector2(rect.xMax, rect.y));
            if (!dotted)
            {
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p1, p2);
                Handles.DrawLine(p2, p3);
                Handles.DrawLine(p3, p0);
            }
            else
            {
                Handles.DrawDottedLine(p0, p1, 5f);
                Handles.DrawDottedLine(p1, p2, 5f);
                Handles.DrawDottedLine(p2, p3, 5f);
                Handles.DrawDottedLine(p3, p0, 5f);
            }
        }

        public static bool IsSceneViewCameraInRange(Vector3 position, float distance)
        {
            Vector3 cameraPos = Camera.current.WorldToScreenPoint(position);
            return ((cameraPos.x >= 0) &&
            (cameraPos.x <= Camera.current.pixelWidth) &&
            (cameraPos.y >= 0) &&
            (cameraPos.y <= Camera.current.pixelHeight) &&
            (cameraPos.z > 0) &&
            (cameraPos.z < distance));
        }

#endif
    }


    public enum DockingPositions
    {

        topCenter,
        topRight,
        rightCenter,
        bottomRight,
        bottomCenter,
        bottomLeft,
        leftCenter,
        topLeft,
        center

    }

    [System.Serializable]
    public class SidebarHeadButton
    {

        public string sidebarHeadButtonText;

        public Sprite sidebarHeadButtonSprite;

        [ReadOnly]
        public int sidebarHeadButtonUseCaseIndex = -1;      //Just as a fallback

        public string sspPosNumber;

        [ReadOnly]
        public SidebarHeaderButtonObject sidebarHeadButtonObject;

        public List<Function> sidebarHeadButtonOnClickFunctions = new List<Function>();
        public List<Function> sidebarHeadButtonOnEnterFunctions = new List<Function>();
        public List<Function> sidebarHeadButtonOnExitFunctions = new List<Function>();
        public List<Function> sidebarHeadButtonOnResetFunctions = new List<Function>();

        [Space]
        [FoldoutGroup("Sidebar Button - Sub Buttons")]
        public List<SidebarButton> sidebarHeadButtonSubButtons = new List<SidebarButton>();


        public void SetUpButton(int useCaseIndex = -1)
        {

            Debug.Log($"Setting up a Sidebar Button {useCaseIndex}");

            if (useCaseIndex == -1)
                return;

            GameObject newSidebarHeadButton = GameObject.Instantiate(CodenameDockingElements.Instance.sidebarHeadButtonPrefab, CodenameDockingElements.Instance.sidebarButtonParent) as GameObject;

            sidebarHeadButtonObject = newSidebarHeadButton.GetComponent<SidebarHeaderButtonObject>();
            sidebarHeadButtonObject.sidebarHeadButtonDataContainer = CodenameDockingElements.Instance.headButtons[useCaseIndex];

            sidebarHeadButtonObject.sidebarHeadButtonDataContainer.sidebarHeadButtonUseCaseIndex = useCaseIndex;

            for (int i = 0; i < sidebarHeadButtonSubButtons.Count; i++)
            {

                sidebarHeadButtonObject.subButtons.Add(sidebarHeadButtonSubButtons[i].SetUpButton(sidebarHeadButtonUseCaseIndex, i));

            }

            sidebarHeadButtonObject.SetUpButton();

            CodenameDockingElements.Instance.UpdateSidebarContainer();

        }

        public void UpdateButton()
        {

            sidebarHeadButtonObject.UpdateButton();

        }

    }

    [System.Serializable]
    public class SidebarButton
    {

        public string sidebarButtonText;

        public Sprite sidebarButtonSprite;

        [ReadOnly]
        public int sidebarButtonUseCaseIndex = -1;      //Just as a fallback

        [ReadOnly]
        public int sidebarButtonSiblingIndex = -1;

        public string sspPosNumber;

        [ReadOnly]
        public SidebarButtonObject sidebarButtonObj;

        public List<Function> sidebarButtonOnClickFunctions = new List<Function>();
        public List<Function> sidebarButtonOnEnterFunctions = new List<Function>();
        public List<Function> sidebarButtonOnExitFunctions = new List<Function>();
        public List<Function> sidebarButtonOnResetFunctions = new List<Function>();


        public SidebarButtonObject SetUpButton(int useCaseIndex = -1, int siblingIndex = -1)
        {

            sidebarButtonUseCaseIndex = useCaseIndex;
            sidebarButtonSiblingIndex = siblingIndex;

            GameObject newSidebarButton = GameObject.Instantiate(CodenameDockingElements.Instance.sidebarButtonPrefab, CodenameDockingElements.Instance.sidebarButtonParent) as GameObject;
            SidebarButtonObject sidebarButtonObject = newSidebarButton.GetComponent<SidebarButtonObject>();

            CodenameDockingElements.Instance.headButtons[sidebarButtonUseCaseIndex].sidebarHeadButtonSubButtons[sidebarButtonSiblingIndex].sidebarButtonObj = sidebarButtonObject;

            return sidebarButtonObj;

        }

    }

    [System.Serializable]
    public class AdditionalCameraPositionButtons
    {

        public Camera cameraPositionButtonCamera;

        public List<Function> cameraPositionButtonFunctions = new List<Function>();

    }

}