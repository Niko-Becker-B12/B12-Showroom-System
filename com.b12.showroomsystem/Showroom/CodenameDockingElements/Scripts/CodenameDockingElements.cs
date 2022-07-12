using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using ThisOtherThing.UI.Shapes;
using UnityEngine.UI;
using Showroom;

namespace Showroom.UI
{

    public class CodenameDockingElements : MonoBehaviour
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

        public List<UserInterfaceContainer> uiContainers = new List<UserInterfaceContainer>();

        public List<SidebarHeadButton> headButtons = new List<SidebarHeadButton>();

        public List<CustomGeneralMenuButton> customGeneralMenuButtons = new List<CustomGeneralMenuButton>();

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

        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuPlayButton;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuPauseButton;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuReplayButton;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuCameraDropdown;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuTransparencyToggle;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuDragModeToggle;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuBackButton;
        [BoxGroup("General Menu Settings")][SerializeField][ReadOnly] public CustomGeneralMenuButton generalMenuHomeButton;

        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuButtonPrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuTogglePrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuSliderPrefab;
        [BoxGroup("General Menu Settings/Custom")][SerializeField] public GameObject generalMenuDropdownPrefab;

        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public RectTransform tooltipRect;
        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public Rectangle tooltipShape;
        [BoxGroup("General Menu Settings/Tooltip")][SerializeField] public TextMeshProUGUI tooltipTextbox;


        private void Start()
        {

            currentResolution = new Vector2(Screen.width, Screen.height);

            for (int i = 0; i < headButtons.Count; i++)
            {

                headButtons[i].SetUpButton(i);

            }

            FinishedGettingData();


        }

        void FinishedGettingData()
        {

            ApplyBaseSkin();

            CreateBaseModules();

            ToggleSidebar(true);

            UpdateSidebarHeader();

            GenerateCustomGeneralMenuModules();

        }

        void CreateBaseModules()
        {

            

            generalMenuPlayButton.SetUpBaseModuleButton(generalMenuPlayButton);
            generalMenuPauseButton.SetUpBaseModuleButton(generalMenuPauseButton);
            generalMenuReplayButton.SetUpBaseModuleButton(generalMenuReplayButton);
            generalMenuCameraDropdown.SetUpBaseModuleButton(generalMenuCameraDropdown);
            generalMenuTransparencyToggle.SetUpBaseModuleButton(generalMenuTransparencyToggle);
            generalMenuBackButton.SetUpBaseModuleButton(generalMenuBackButton);
            generalMenuHomeButton.SetUpBaseModuleButton(generalMenuHomeButton);

        }

        void GenerateCustomGeneralMenuModules()
        {

            for (int i = customGeneralMenuButtons.Count; i > 0; i--)
            {
                Debug.Log(i - 1);

                customGeneralMenuButtons[i - 1].SetUpButton(i - 1);

            }

        }

        void ApplyBaseSkin()
        {

            if(baseUISkin.useSidebarSettings)
            {

                sidebarContainerRect.GetComponent<Image>().color = baseUISkin.sidebarBackgroundColor;

            }

            if(baseUISkin.useGeneralMenuSettings)
            {

                generalMenuContainerRect.GetComponent<Rectangle>().color = baseUISkin.generalMenuBackgroundColor;

                generalMenuPlayButton.buttonSprite = baseUISkin.generalMenuPlayButtonIcon;
                generalMenuPauseButton.buttonSprite = baseUISkin.generalMenuPauseButtonIcon;
                generalMenuReplayButton.buttonSprite = baseUISkin.generalMenuReplayButtonIcon;
                generalMenuCameraDropdown.buttonSprite = baseUISkin.generalMenuCameraDropdownIcon;
                generalMenuTransparencyToggle.buttonSprite = baseUISkin.generalMenuTransparencyToggleActiveIcon;
                generalMenuBackButton.buttonSprite = baseUISkin.generalMenuBackButtonIcon;
                generalMenuHomeButton.buttonSprite = baseUISkin.generalMenuHomeButtonIcon;

            }

            if (baseUISkin.useMenuFoilSettings)
            {



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
                Debug.Log(string.Format("Could not find any UI-Containers with the name: {0}!", containerName));

            return null;

        }

        void UpdateUIContainer(UserInterfaceContainer tempContainer)
        {

            if (ShowroomManager.Instance.showDebugMessages)
                Debug.Log(string.Format("Updating UI-Container with the name: {0}!", tempContainer.uiContainerShortName));

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

                        Debug.Log("Found Selected Sidebar Button | Sidebar Button: " + i);

                        continue;

                    }

                    Debug.Log("Updating Button Selection | Sidebar Button: " + i);

                    headButtons[i].sidebarHeadButtonObject.sidebarButtonBehavior.ResetClick();

                }

                return;

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

            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect);

            if (rectPos == null || tooltipText == "" || string.IsNullOrEmpty(tooltipText))
            {

                Vector2 screenSpacePos = new Vector2(-10000f, -10000f);

                tooltipRect.position = screenSpacePos;

            }
            else
            {

                Vector2 screenSpacePos = rectPos.position;

                if (Mouse.current.position.ReadValue().x >= (currentResolution.x * .85f))
                {

                    tooltipShape.RoundedProperties.BLRadius = 16f;
                    tooltipShape.RoundedProperties.TLRadius = 16f;
                    tooltipShape.RoundedProperties.BRRadius = 0f;
                    tooltipShape.RoundedProperties.TRRadius = 0f;


                    if (Mouse.current.position.ReadValue().y >= (currentResolution.y * .8f))
                        tooltipRect.pivot = new Vector2(1.375f, 0.5f);
                    else
                        tooltipRect.pivot = new Vector2(1.12f, 0.5f);

                    screenSpacePos = new Vector2(screenSpacePos.x, screenSpacePos.y);
                }
                else
                {

                    if (generalMenuOneButtonActive)
                    {

                        tooltipRect.pivot = new Vector2(-0.11f, 0.5f);

                        tooltipShape.RoundedProperties.BLRadius = 0f;
                        tooltipShape.RoundedProperties.TLRadius = 0f;
                        tooltipShape.RoundedProperties.BRRadius = 16f;
                        tooltipShape.RoundedProperties.TRRadius = 16f;

                    }
                    else
                    {

                        tooltipRect.pivot = new Vector2(0.5f, -0.86f);

                        tooltipShape.RoundedProperties.BLRadius = 16f;
                        tooltipShape.RoundedProperties.TLRadius = 16f;
                        tooltipShape.RoundedProperties.BRRadius = 16f;
                        tooltipShape.RoundedProperties.TRRadius = 16f;

                    }

                    screenSpacePos = new Vector2(screenSpacePos.x, screenSpacePos.y);
                }

                tooltipRect.position = screenSpacePos;

            }

        }

        public void DisableTooltip()
        {

            tooltipRect.gameObject.SetActive(false);

        }

        public void ToggleSidebar(bool isOpen)
        {

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

            for(int i = 0; i < headButtons.Count; i++)
            {

                headButtons[i].UpdateButton();

            }

        }

        public void ToggleGeneralMenu(bool reOpenAfter)
        {

            DOTween.Kill(generalMenuContainerRect);

            if(generalMenuIsOpen)
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

                        UpdateGeneralMenuContents();

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



                })
                .OnComplete(() =>
                {

                    generalMenuIsOpen = true;

                });

            }

        }

        void UpdateGeneralMenuContents()
        {

            if(ShowroomManager.Instance.useCaseIndex != -1)
            {

                generalMenuPlayButton.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasPlayButton);

                generalMenuPauseButton.customGeneralMenuButtonObj.gameObject.SetActive(false);

                generalMenuTransparencyToggle.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasTransparencyButton);

                if (ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasRestartButton && !ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].playButtonIsRestartButton)
                    generalMenuReplayButton.customGeneralMenuButtonObj.gameObject.SetActive(true);
                else
                    generalMenuReplayButton.customGeneralMenuButtonObj.gameObject.SetActive(false);

                generalMenuHomeButton.customGeneralMenuButtonObj.gameObject.SetActive(false);//ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton);
                generalMenuBackButton.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasResetCameraButton);

                generalMenuCameraDropdown.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasCameraPosButton);

                generalMenuDragModeToggle.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.useCases[ShowroomManager.Instance.useCaseIndex].hasDragModeButton);


            }
            else
            {

                generalMenuPlayButton.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.hasPlayButton);

                generalMenuPauseButton.customGeneralMenuButtonObj.gameObject.SetActive(false);

                generalMenuTransparencyToggle.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.hasTransparencyButton);

                if (ShowroomManager.Instance.hasRestartButton && !ShowroomManager.Instance.playButtonIsRestartButton)
                    generalMenuReplayButton.customGeneralMenuButtonObj.gameObject.SetActive(true);
                else
                    generalMenuReplayButton.customGeneralMenuButtonObj.gameObject.SetActive(false);

                generalMenuHomeButton.customGeneralMenuButtonObj.gameObject.SetActive(false);//ShowroomManager.Instance.hasResetCameraButton);
                generalMenuBackButton.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.hasResetCameraButton);

                generalMenuCameraDropdown.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.hasCameraPosButton);

                generalMenuDragModeToggle.customGeneralMenuButtonObj.gameObject.SetActive(ShowroomManager.Instance.hasDragModeButton);

            }



        }

    }

    public enum DockingPositions
    {

        Top,
        Right,
        LowerRight,
        LowerLeft

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

        public List<SidebarButton> sidebarHeadButtonSubButtons = new List<SidebarButton>();


        public void SetUpButton(int useCaseIndex = -1)
        {

            GameObject newSidebarHeadButton = GameObject.Instantiate(CodenameDockingElements.Instance.sidebarHeadButtonPrefab, CodenameDockingElements.Instance.sidebarButtonParent) as GameObject;

            sidebarHeadButtonObject = newSidebarHeadButton.GetComponent<SidebarHeaderButtonObject>();
            sidebarHeadButtonObject.sidebarHeadButtonDataContainer = CodenameDockingElements.Instance.headButtons[useCaseIndex];

            sidebarHeadButtonObject.sidebarHeadButtonDataContainer.sidebarHeadButtonUseCaseIndex = useCaseIndex;

            for (int i = 0; i < sidebarHeadButtonSubButtons.Count; i++)
            {

                GameObject newSidebarButton = GameObject.Instantiate(CodenameDockingElements.Instance.sidebarButtonPrefab, CodenameDockingElements.Instance.sidebarButtonParent) as GameObject;
                SidebarButtonObject sidebarButtonObject = newSidebarButton.GetComponent<SidebarButtonObject>();

                sidebarHeadButtonSubButtons[i].sidebarButtonObj = sidebarButtonObject;

                sidebarHeadButtonObject.subButtons.Add(sidebarButtonObject);

                Debug.Log(sidebarButtonObject);

                sidebarButtonObject.sidebarButtonDataContainer.sidebarbuttonSiblingIndex = i;
                sidebarButtonObject.sidebarButtonDataContainer.sidebarbuttonUseCaseIndex = useCaseIndex;

                Debug.Log(sidebarButtonObject.name + sidebarButtonObject.sidebarButtonDataContainer.sidebarbuttonUseCaseIndex + "-" + sidebarButtonObject.sidebarButtonDataContainer.sidebarbuttonSiblingIndex);

            }

            sidebarHeadButtonObject.SetUpButton();

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
        public SidebarButtonObject sidebarButtonObj;

        [ReadOnly]
        public int sidebarbuttonUseCaseIndex = -1;

        [ReadOnly]
        public int sidebarbuttonSiblingIndex = -1;

        public List<Function> sidebarButtonOnClickFunctions = new List<Function>();
        public List<Function> sidebarButtonOnEnterFunctions = new List<Function>();
        public List<Function> sidebarButtonOnExitFunctions = new List<Function>();
        public List<Function> sidebarButtonOnResetFunctions = new List<Function>();

    }

    [System.Serializable]
    public class CustomGeneralMenuButton
    {

        public enum CustomGeneralMenuButtonType
        {

            button,
            toggle,
            slider,
            dropdown

        }

        public CustomGeneralMenuButtonType customButtonType;


        public string tooltipText;

        public bool isIndexed = false;

        #region Toggle


        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle), ReadOnly]
        public CustomGeneralMenuButtonObject customGeneralMenuToggleObj;

        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle)]
        public Sprite toggleActiveSprite;

        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle)]
        public Sprite toggleDeactiveSprite;

        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle), ReadOnly]
        public bool toggleIsActive;

        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle)]
        public List<Function> onSetActiveFunctions = new List<Function>();
        [BoxGroup("Toggle"), ShowIf("customButtonType", CustomGeneralMenuButtonType.toggle)]
        public List<Function> onSetDeactiveFunctions = new List<Function>();


        #endregion

        #region Button


        [BoxGroup("Button"), ShowIf("customButtonType", CustomGeneralMenuButtonType.button), ReadOnly]
        public CustomGeneralMenuButtonObject customGeneralMenuButtonObj;

        [BoxGroup("Button"), ShowIf("customButtonType", CustomGeneralMenuButtonType.button)]
        public Sprite buttonSprite;

        [BoxGroup("Button"), ShowIf("customButtonType", CustomGeneralMenuButtonType.button)]
        public List<Function> buttonOnClickFunctions = new List<Function>();
        [BoxGroup("Button"), ShowIf("customButtonType", CustomGeneralMenuButtonType.button)]
        public List<Function> buttonOnEnterFunctions = new List<Function>();
        [BoxGroup("Button"), ShowIf("customButtonType", CustomGeneralMenuButtonType.button)]
        public List<Function> buttonOnExitFunctions = new List<Function>();


        #endregion

        #region Dropdown


        [BoxGroup("Dropdown"), ShowIf("customButtonType", CustomGeneralMenuButtonType.dropdown), ReadOnly]
        public CustomGeneralMenuButtonObject customGeneralMenuDropdownObj;

        [BoxGroup("Dropdown"), ShowIf("customButtonType", CustomGeneralMenuButtonType.dropdown)]
        public Sprite dropdownSprite;

        [BoxGroup("Dropdown"), ShowIf("customButtonType", CustomGeneralMenuButtonType.dropdown)]
        public Sprite dropdownChildSprite;

        [BoxGroup("Dropdown"), ShowIf("customButtonType", CustomGeneralMenuButtonType.dropdown)]
        public List<Function> dropdownFunctions = new List<Function>();


        #endregion

        #region Slider


        [BoxGroup("Slider"), ShowIf("customButtonType", CustomGeneralMenuButtonType.slider), ReadOnly]
        public CustomGeneralMenuButtonObject customGeneralMenuSliderObj;

        [BoxGroup("Slider"), ShowIf("customButtonType", CustomGeneralMenuButtonType.slider)]
        public Vector2 sliderMinMaxValues;

        [BoxGroup("Slider"), ShowIf("customButtonType", CustomGeneralMenuButtonType.slider)]
        public List<Function> sliderFunctions = new List<Function>();


        #endregion


        public void SetUpButton(int buttonIndex = -1, int useCaseIndex = -1)
        {

            if (customButtonType == CustomGeneralMenuButtonType.toggle)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuTogglePrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                newGeneralMenuButton.transform.SetAsFirstSibling();

                CustomGeneralMenuToggleObject customGeneralMenuButtonToggleObj = (CustomGeneralMenuToggleObject)customGeneralMenuButtonObj;

            }
            else if (customButtonType == CustomGeneralMenuButtonType.button)
            {


                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuButtonPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                newGeneralMenuButton.transform.SetAsFirstSibling();

                customGeneralMenuButtonObj = newGeneralMenuButton.GetComponent<CustomGeneralMenuButtonObject>();
                customGeneralMenuButtonObj.generalButtonDataContainer = CodenameDockingElements.Instance.customGeneralMenuButtons[buttonIndex];

                customGeneralMenuButtonObj.SetUpButton();

            }
            else if (customButtonType == CustomGeneralMenuButtonType.slider)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuSliderPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;
                newGeneralMenuButton.transform.SetAsFirstSibling();

            }
            else if (customButtonType == CustomGeneralMenuButtonType.dropdown)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuDropdownPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                newGeneralMenuButton.transform.SetAsFirstSibling();

            }

        }

        public void SetUpBaseModuleButton(CustomGeneralMenuButton customGeneralMenuDataContainer = null)
        {

            if (customButtonType == CustomGeneralMenuButtonType.toggle)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuTogglePrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                //newGeneralMenuButton.transform.SetAsFirstSibling();

                CustomGeneralMenuToggleObject customGeneralMenuButtonToggleObj = (CustomGeneralMenuToggleObject)customGeneralMenuButtonObj;

            }
            else if (customButtonType == CustomGeneralMenuButtonType.button)
            {


                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuButtonPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                //newGeneralMenuButton.transform.SetAsFirstSibling();

                customGeneralMenuButtonObj = newGeneralMenuButton.GetComponent<CustomGeneralMenuButtonObject>();
                customGeneralMenuButtonObj.generalButtonDataContainer = customGeneralMenuDataContainer;

                customGeneralMenuButtonObj.SetUpButton();

            }
            else if (customButtonType == CustomGeneralMenuButtonType.slider)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuSliderPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                newGeneralMenuButton.transform.SetAsFirstSibling();

                //newGeneralMenuButton.transform.SetAsFirstSibling();

            }
            else if (customButtonType == CustomGeneralMenuButtonType.dropdown)
            {

                GameObject newGeneralMenuButton = GameObject.Instantiate(CodenameDockingElements.Instance.generalMenuDropdownPrefab, CodenameDockingElements.Instance.generalMenuButtonParent) as GameObject;

                //newGeneralMenuButton.transform.SetAsFirstSibling();

            }

        }

        public void ResetFunctions(CustomGeneralMenuButton customGeneralMenuDataContainer = null)
        {

            if (customGeneralMenuDataContainer == null)
                return;

            onSetActiveFunctions.Clear();
            onSetDeactiveFunctions.Clear();

            buttonOnClickFunctions.Clear();
            buttonOnEnterFunctions.Clear();
            buttonOnExitFunctions.Clear();
            
            dropdownFunctions.Clear();

            sliderFunctions.Clear();

        }

    }

}