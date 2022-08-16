using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [OdinSerialize]
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
        public CustomGeneralMenuModule generalMenuCameraDropdown = new CustomGeneralMenuModule
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


        [BoxGroup("Focus Menu Settings")][SerializeField] public RectTransform focusMenuContainerRect;

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


        [BoxGroup("Custom UI Containers")] public GameObject uiContainerPrefab;
        [BoxGroup("Custom UI Containers")] public Transform uiContainerParent;

        [BoxGroup("Custom UI Containers")] public GameObject uiContainerButtonPrefab;
        [BoxGroup("Custom UI Containers")] public GameObject uiContainerHeadlinePrefab;


        private void Start()
        {

            currentResolution = new Vector2(Screen.width, Screen.height);

            if (!ShowroomManager.Instance.isOnlyLevelInBuild && !ShowroomManager.Instance.downloadFairtouchData)
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

            //CreateGeneralMenuBaseModules();

            UpdateSidebarHeader();

            CreateSidebarButtons();

            GenerateCustomGeneralMenuModules();

            GenerateBaseFocusMenuModules();

            CreateCustomUIContainers();

            return true;

        }

        public void FinishedGettingData()
        {

            ToggleSidebar(true);

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

                    generalMenuPlayButton.SetUpButton(generalMenuPlayButton, -1, -1);

                }
                if (ShowroomManager.Instance.hasRestartButton)
                {

                    generalMenuReplayButton = ShowroomManager.Instance.generalMenuReplayButton;

                    generalMenuReplayButton.SetUpButton(generalMenuReplayButton, -1, -1);

                }
                if (ShowroomManager.Instance.hasTransparencyButton)
                {

                    generalMenuTransparencyToggle = ShowroomManager.Instance.generalMenuTransparencyToggle;

                    generalMenuTransparencyToggle.SetUpButton(generalMenuTransparencyToggle, -1, -1);

                }
                if (ShowroomManager.Instance.hasCameraPosButton)
                {

                    generalMenuCameraDropdown = ShowroomManager.Instance.generalMenuCameraDropdown;

                    generalMenuCameraDropdown.SetUpButton(generalMenuCameraDropdown, -1, -1);

                }
                if (ShowroomManager.Instance.hasDragModeButton)
                {

                    generalMenuDragModeToggle = ShowroomManager.Instance.generalMenuDragModeToggle;

                    generalMenuDragModeToggle.SetUpButton(generalMenuDragModeToggle, -1, -1);

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

                    generalMenuBackButton.SetUpButton(generalMenuBackButton, -1, -1);
                    generalMenuHomeButton.SetUpButton(generalMenuHomeButton, -1, -1);

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

                    generalMenuPlayButton.SetUpButton(generalMenuPlayButton, -1, -1);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton)
                {

                    generalMenuReplayButton = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuReplayButton;

                    generalMenuReplayButton.SetUpButton(generalMenuReplayButton, -1, -1);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton)
                {

                    generalMenuTransparencyToggle = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuTransparencyToggle;

                    generalMenuTransparencyToggle.SetUpButton(generalMenuTransparencyToggle, -1, -1);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton)
                {

                    generalMenuCameraDropdown = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuCameraDropdown;

                    generalMenuCameraDropdown.SetUpButton(generalMenuCameraDropdown, -1, -1);

                }
                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton)
                {

                    generalMenuDragModeToggle = ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].generalMenuDragModeToggle;

                    generalMenuDragModeToggle.SetUpButton(generalMenuDragModeToggle, -1, -1);

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

                    generalMenuBackButton.SetUpButton(generalMenuBackButton, -1, -1);
                    generalMenuHomeButton.SetUpButton(generalMenuHomeButton, -1, -1);

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

            for (int i = CustomGeneralMenuModules.Count; i > 0; i--)
            {
                Debug.Log(i - 1);

                CustomGeneralMenuModules[i - 1].SetUpButton(CustomGeneralMenuModules[i], i - 1);

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

                UserInterfaceContainer newContainer = Instantiate(ShowroomManager.Instance.userInterfaceContainers[i]);

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

        public void ToggleGeneralMenu(bool reOpenAfter)
        {

            DOTween.Kill(generalMenuContainerRect);

            if (generalMenuIsOpen)
            {

                generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuClosedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {



                })
                .OnComplete(() =>
                {

                    generalMenuIsOpen = false;

                    if (reOpenAfter)
                    {

                        UpdateGeneralMenuModules();

                        generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                        .OnStart(() =>
                        {



                        })
                        .OnComplete(() =>
                        {

                            generalMenuIsOpen = true;

                        });

                    }

                });

            }
            else
            {

                generalMenuContainerRect.DOAnchorPos(baseUISkin.generalMenuOpenedPos, baseUISkin.animationSpeed).SetEase(baseUISkin.animationEasingType)
                .OnStart(() =>
                {

                    UpdateGeneralMenuModules();

                })
                .OnComplete(() =>
                {

                    generalMenuIsOpen = true;

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