using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Showroom.UI;
using Sirenix.Serialization;

namespace Showroom
{

    public class ShowroomManager : SerializedMonoBehaviour
    {

        #region Singleton

        public static ShowroomManager Instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void Awake()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            else
                Instance = this;

            if(isOnlyLevelInBuild && ShowroomLoadingScreen.Instance == null)
            {

                isOnlyLevelInBuild = false;

                SceneManager.LoadScene("Sub-Level_LoadingScreen");

                float loadingScreenTimer = 0;

                DOTween.To(() => loadingScreenTimer, x => loadingScreenTimer = x, 1, .25f)
                .OnComplete(() =>
                {

                    ShowroomLoadingScreen.Instance.sceneToLoad = subLevelID;

                    ShowroomLoadingScreen.Instance.StartSceneLoad();

                });

               

            }
            else if(!isOnlyLevelInBuild && ShowroomLoadingScreen.Instance == null && downloadFairtouchData)
            {

                ShowroomSSPDataHandler.Instance.StartDownloadingFairtouchData();

            }

        }

        #endregion


        [FoldoutGroup("General")] public CodenameDockingElements showroomUI;

        [InfoBox("There is no Sub-Level Name set!", InfoMessageType.Error, "hasNoLevelName")]
        [FoldoutGroup("General")] public string subLevelName;

        [InfoBox("There is no Sub-Level Sub Title set!", InfoMessageType.Warning, "hasNoLevelSubTitle")]
        [FoldoutGroup("General")] public string subLevelSubTitle;

        [InfoBox("There is no Sub-Level ID set!", InfoMessageType.Error, "hasNoLevelID")]
        [FoldoutGroup("General")] public string subLevelID;


        [InfoBox("This Camera is used if there is no Use Case selected or there are no Use Cases.", InfoMessageType.Info, "moreThanOneUseCase")]
        [InfoBox("There is no Sub-Level Main Camera set!", InfoMessageType.Error, "hasNoSubLevelHomeCamera")]
        [FoldoutGroup("General")] public Camera subLevelMainCamera;

        [FoldoutGroup("General")] public List<Camera> subLevelCameras = new List<Camera>();

        [FoldoutGroup("General")] public List<Function> onLevelLoaded = new List<Function>();


        [FoldoutGroup("General/Project Settings")] public bool isOnlyLevelInBuild = false;
        [FoldoutGroup("General/Project Settings")] public bool showDebugMessages = false;
        [FoldoutGroup("General/Project Settings")] public bool openSidebarMenu = true;


        [FoldoutGroup("General Menu Contents")] public bool subLevelHasGeneralMenu;
        [FoldoutGroup("General Menu Contents")] public Vector2 clickMeVFXRandomTime = new Vector2(10f, 45f);


        bool hasNoTransparentObjects()
        {
            if (transparentObjects.Count == 0)
                return true;
            else
                return false;
        }

        [FoldoutGroup("General Menu Contents")] [ShowIf("subLevelHasGeneralMenu")] public bool hasPlayButton;
        [FoldoutGroup("General Menu Contents")] [ShowIf("@this.subLevelHasGeneralMenu == true && this.hasPlayButton == true && this.playButtonIsRestartButton != true")] public bool playButtonIsPauseButton;
        [FoldoutGroup("General Menu Contents")] [ShowIf("@this.subLevelHasGeneralMenu == true && this.hasPlayButton == true && this.hasRestartButton != true && this.playButtonIsPauseButton != true")] public bool playButtonIsRestartButton;

        [FoldoutGroup("General Menu Contents")] [ShowIf("subLevelHasGeneralMenu")] public bool hasTransparencyButton;
        [FoldoutGroup("General Menu Contents")] [SerializeField] private Material transparencyMaterial;
        [FoldoutGroup("General Menu Contents")] [ShowIf("@this.subLevelHasGeneralMenu == true && hasTransparencyButton")] public List<MeshRenderer> transparentObjects = new List<MeshRenderer>();
        [FoldoutGroup("General Menu Contents")] [ShowIf("@this.subLevelHasGeneralMenu == true && hasTransparencyButton")] [SerializeField] public Animator xRayLinesObj;

        [FoldoutGroup("General Menu Contents")] [ShowIf("@this.subLevelHasGeneralMenu == true && playButtonIsRestartButton == false")] public bool hasRestartButton;

        [FoldoutGroup("General Menu Contents")] [ShowIf("subLevelHasGeneralMenu")] public bool hasResetCameraButton;

        [FoldoutGroup("General Menu Contents")] [ShowIf("subLevelHasGeneralMenu")] public bool hasCameraPosButton;

        [FoldoutGroup("General Menu Contents")] [ShowIf("subLevelHasGeneralMenu")] public bool hasDragModeButton;


        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Play Button")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasPlayButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuPlayButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Play Animation",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Replay Button")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasRestartButton || this.subLevelHasGeneralMenu == true && playButtonIsRestartButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuReplayButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Restart Animation",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Pause Button")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && playButtonIsPauseButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuPauseButton;

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Transparency Toggle")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasTransparencyButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuTransparencyToggle = new CustomGeneralMenuModule_Toggle
        {

            tooltipText = "Toggle Transparency Mode",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Camera Dropdown")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasCameraPosButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuCameraDropdown = new CustomGeneralMenuModule_Dropdown
        {

            tooltipText = "",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Dragmode Toggle")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasDragModeButton")] 
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuDragModeToggle = new CustomGeneralMenuModule_Toggle
        {

            tooltipText = "Toggle Drag Mode",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Back Button")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasResetCameraButton")] 
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuBackButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Back",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Home Button")]
        [ShowIf("@this.subLevelHasGeneralMenu == true && hasResetCameraButton")] 
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuHomeButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Return to Home-Position",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")] 
        [ShowIf("subLevelHasGeneralMenu")] 
        [OdinSerialize] 
        private List<CustomGeneralMenuModule> CustomGeneralMenuModules = new List<CustomGeneralMenuModule>();

        //[FoldoutGroup("General Menu Contents/Camera Position Button")] [ShowIf("@this.subLevelHasGeneralMenu == true && hasCameraPosButton")] public List<AdditionalCameraPositionButtons> cameraButtons = new List<AdditionalCameraPositionButtons>();

        [HideInInspector] public int cameraPosIndex = -1;

        bool hasNoSubLevelHomeCamera()
        {
            return !subLevelMainCamera;
        }

        bool hasNoLevelName()
        {
            if (string.IsNullOrEmpty(subLevelName))
                return true;
            else
                return false;
        }

        bool hasNoLevelSubTitle()
        {
            if (string.IsNullOrEmpty(subLevelSubTitle))
                return true;
            else
                return false;
        }

        bool hasNoLevelID()
        {
            if (string.IsNullOrEmpty(subLevelID))
                return true;
            else
                return false;
        }

        [FoldoutGroup("Focus Menu Contents")] public bool hasFocusMenu = false;

        [FoldoutGroup("Focus Menu Contents")]
        [ShowIf("hasFocusMenu")] 
        public Transform focusObjectPosition;

        [FoldoutGroup("Focus Menu Contents")]
        [BoxGroup("Focus Menu Contents/Back Button")]
        [ShowIf("hasFocusMenu")]
        [OdinSerialize]
        public CustomGeneralMenuModule focusMenuBackButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Reset Rotation",
            isIndexed = false

        };

        [FoldoutGroup("Focus Menu Contents")]
        [BoxGroup("Focus Menu Contents/Reset Rotation Button")]
        [ShowIf("hasFocusMenu")] 
        [OdinSerialize]
        public CustomGeneralMenuModule focusMenuResetRotationButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Return Object",
            isIndexed = false

        };

        [FoldoutGroup("Focus Menu Contents")]
        [BoxGroup("Focus Menu Contents/Rotation Button")]
        [ShowIf("hasFocusMenu")]
        [OdinSerialize]
        public CustomGeneralMenuModule focusMenuRotationButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Reset Rotation",
            isIndexed = false

        };


        [FoldoutGroup("Bullet Points")] [ReadOnly] public int activeBulletPointIndex = -1; //== -1 => No active BulletPoint
        [FoldoutGroup("Bullet Points")] public List<BulletPoint> bulletPoints = new List<BulletPoint>();


        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToMove;
        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToRotate;
        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToTeleport;

        [FoldoutGroup("Player Settings")] [ShowIf("hasDragModeButton")] public Vector2 playerDragModeBoundaries = new Vector2(0, 0);

        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartWalking = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartTeleporting = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartRotating = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopWalking = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopTeleporting = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopRotating = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onPlayerReset = new List<Function>();


        [FoldoutGroup("Timeline Stepper")] public bool hasTimelineStepper = false;

        [FoldoutGroup("Timeline Stepper")] [ReadOnly] public PlayableDirector currentTimeline;

        [FoldoutGroup("Timeline Stepper")] public bool timelineStepperAutoPlay = false;

        [FoldoutGroup("Timeline Stepper")] [ReadOnly] public int timelineStepperIndex = -1;
        [FoldoutGroup("Timeline Stepper")] [ReadOnly] public int timelineOldIndex = -1;
        [FoldoutGroup("Timeline Stepper")] [ReadOnly] public float currentTimelineTime = -1;
        [FoldoutGroup("Timeline Stepper")] [ShowIf("hasTimelineStepper")] public List<ShowroomTimelineStep> timelineSteps = new List<ShowroomTimelineStep>();


        [FoldoutGroup("Custom UI Containers")]
        [OdinSerialize]
        public List<UserInterfaceContainer> userInterfaceContainers = new List<UserInterfaceContainer>();


        [InfoBox("There are no Use Cases! Please add atleast one Use Case!", InfoMessageType.Error, "hasNoUseCases")]
        [FoldoutGroup("Use Cases")] [ShowIf("hasUseCases")] [ReadOnly] [SerializeField] public int useCaseIndex = -1;
        [FoldoutGroup("Use Cases")] [ShowIf("hasUseCases")] [SerializeField] public bool hasStandardUseCase;
        [FoldoutGroup("Use Cases")] [ShowIf("hasStandardUseCase")] [SerializeField] public int standardUseCaseIndex = 0;

        [FoldoutGroup("Use Cases")] 
        //[SerializeField, OdinSerialize]
        public List<UseCase> useCases = new List<UseCase>();


        bool hasUseCases()
        {
            if (useCases.Count == 0)
                return false;
            else
                return true;
        }

        bool hasNoUseCases()
        {
            if (useCases.Count == 0)
                return true;
            else
                return false;
        }

        bool moreThanOneUseCase()
        {
            if (useCases.Count > 1)
                return true;
            else
                return false;
        }


        [FoldoutGroup("Fairtouch"), InfoBox("You're using Fairtouch Data and prefering it over the entered Data inside the Showroom Manager. This might not achieve the desired result!",
            InfoMessageType.Warning, "downloadFairtouchData")]

        [FoldoutGroup("Fairtouch")]
        public bool downloadFairtouchData = false;

        [FoldoutGroup("Fairtouch")]
        void GetFairtouchData()
        {

            ShowroomSSPDataHandler newHandler = new ShowroomSSPDataHandler();

            newHandler.GetFairtouchData();

        }


        [ReadOnly]
        public bool isTransparent;

        [ReadOnly]
        public bool isDragMode;

        private List<Material> materials = new List<Material>();

        [ReadOnly]
        public bool isAtUseCaseHomePos;

        [HideInInspector]
        public GameObject currentlyFocusedObj;

        private void Start()
        {
            
            if(!isOnlyLevelInBuild && ShowroomLoadingScreen.Instance == null)
            {

                StartLevel();

            }

        }

        private void Update()
        {

            if(currentTimeline != null && currentTimeline.playableGraph.IsPlaying())
            {

                currentTimelineTime = (float)currentTimeline.time;

            }

        }

        public void StartLevel()
        {

            if (hasStandardUseCase)
            {
                showroomUI.headButtons[standardUseCaseIndex].sidebarHeadButtonObject.sidebarButtonBehavior.OnMouseDown();
                showroomUI.headButtons[standardUseCaseIndex].sidebarHeadButtonObject.sidebarButtonBehavior.wasClicked = true;

                isAtUseCaseHomePos = true;

                SetPlayerProperties();
            }
            else if (!hasStandardUseCase && hasUseCases() || !hasUseCases())
                MoveToFixedPos(-1);

        }

        IEnumerator Invoke(UnityEvent function, float delay)
        {

            yield return new WaitForSecondsRealtime(delay);

            function?.Invoke();

        }

        public void OnNewCamPos(Transform cameraPos)
        {

            Debug.LogError("OnNewCamPos(Transform cameraPos) has been deprecated in version 4.0.0, please use MoveToFixPos(int cameraIndex)! More info in the Documentation!");

            return;

        }

        public void MoveToFixedPos(int cameraIndex)
        {

            #region old code
            /*
            if (i == -1 && isAtUseCaseHomePos || i == -1 && !hasUseCases() || i == -1 && useCaseIndex == -1)
            {

                if (showDebugMessages)
                    Debug.Log("Returning to Sub-Level home camera");

                ShowroomNavigation.Instance.ableToRotate = false;

                Vector3 pos = SubLevelMainCamera.transform.position;

                float yRot = SubLevelMainCamera.transform.eulerAngles.y;
                float xRot = SubLevelMainCamera.transform.eulerAngles.x;

                Vector3 playerRot = new Vector3(0f, yRot, 0f);
                Vector3 cameraRot = new Vector3(-xRot, -yRot, 0f);

                ShowroomNavigation.Instance.TurnCameraTowards(new Vector2(xRot, yRot));

                ShowroomNavigation.Instance.transform.DOMove(pos, 1f).SetEase(Ease.InOutSine);
                ShowroomNavigation.Instance.transform.DORotate(playerRot, 1f).SetEase(Ease.InOutSine).SetOptions(true);
                ShowroomNavigation.Instance.playerCamera.transform.DORotate(-cameraRot, 1f).SetEase(Ease.InOutSine).OnComplete(() => FinishedMovingPlayer(true)).SetOptions(true);

                ShowroomNavigation.Instance.teleportPosition = pos;

                ShowroomNavigation.Instance.playerCamera.DOFieldOfView(SubLevelMainCamera.fieldOfView, 1f);

                if (hasCameraPosButton)
                {

                    subLevelUI.generalMenuCameraPosButton.GetComponent<Image>().sprite = subLevelUI.generalMenuCameraPosButtonSprites[0];
                    subLevelUI.generalMenuCameraPosButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);

                }

                if (useCaseIndex != -1)
                    SwitchUseCase(-1);

                subLevelUI.ResetAllSidebarHeadButtons(-1);

                subLevelUI.CloseBulletPointContainer();

                SetPlayerProperties();

                return;
            }
            if (i == -1)
            {

                if (showDebugMessages)
                    Debug.Log("Returning to Use-Case home camera");

                ShowroomNavigation.Instance.ableToRotate = false;

                Vector3 pos = useCases[useCaseIndex].useCaseHomeCamera.transform.position;

                float yRot = useCases[useCaseIndex].useCaseHomeCamera.transform.eulerAngles.y;
                float xRot = useCases[useCaseIndex].useCaseHomeCamera.transform.eulerAngles.x;

                Vector3 playerRot = new Vector3(0f, yRot, 0f);
                Vector3 cameraRot = new Vector3(-xRot, -yRot, 0f);

                ShowroomNavigation.Instance.TurnCameraTowards(new Vector2(xRot, yRot));

                ShowroomNavigation.Instance.transform.DOMove(pos, 1f).SetEase(Ease.InOutSine);
                ShowroomNavigation.Instance.transform.DORotate(playerRot, 1f).SetEase(Ease.InOutSine).SetOptions(true);
                ShowroomNavigation.Instance.playerCamera.transform.DORotate(-cameraRot, 1f).SetEase(Ease.InOutSine).OnComplete(() => FinishedMovingPlayer(true)).SetOptions(true);

                ShowroomNavigation.Instance.teleportPosition = pos;

                ShowroomNavigation.Instance.playerCamera.DOFieldOfView(useCases[useCaseIndex].useCaseHomeCamera.fieldOfView, 1f);

                if (useCases[useCaseIndex].hasCameraPosButton)
                {

                    subLevelUI.generalMenuCameraPosButton.GetComponent<Image>().sprite = subLevelUI.generalMenuCameraPosButtonSprites[0];
                    //subLevelUI.generalMenuCameraPosButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);

                }

                subLevelUI.CloseBulletPointContainer();

                return;
            }
            else
            {
                ShowroomNavigation.Instance.ableToRotate = false;

                Vector3 pos = useCases[useCaseIndex].useCaseCameras[i].transform.position;

                float yRot = useCases[useCaseIndex].useCaseCameras[i].transform.eulerAngles.y;
                float xRot = useCases[useCaseIndex].useCaseCameras[i].transform.eulerAngles.x;

                Vector3 playerRot = new Vector3(0f, yRot, 0f);
                Vector3 cameraRot = new Vector3(-xRot, -yRot, 0f);

                ShowroomNavigation.Instance.TurnCameraTowards(new Vector2(xRot, yRot));

                ShowroomNavigation.Instance.transform.DOMove(pos, 1f).SetEase(Ease.InOutSine);
                ShowroomNavigation.Instance.transform.DORotate(playerRot, 1f).SetEase(Ease.InOutSine).SetOptions(true);
                ShowroomNavigation.Instance.playerCamera.transform.DORotate(-cameraRot, 1f).SetEase(Ease.InOutSine).OnComplete(() => FinishedMovingPlayer(false)).SetOptions(true);

                ShowroomNavigation.Instance.teleportPosition = pos;

                ShowroomNavigation.Instance.playerCamera.DOFieldOfView(useCases[useCaseIndex].useCaseCameras[i].fieldOfView, 1f);

                if (useCases[useCaseIndex].hasCameraPosButton)
                {

                    subLevelUI.generalMenuCameraPosButton.GetComponent<Image>().sprite = subLevelUI.generalMenuCameraPosButtonSprites[0];
                    subLevelUI.generalMenuCameraPosButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);

                }

                //subLevelUI.CloseBulletPointContainer();

                return;
            }
            */
            #endregion

            if(cameraIndex == -2)                                                                                                                   //Return directly to Sub-Level Home (Home-Button)
            {

                //SwitchUseCase(-1);

                ShowroomNavigation.Instance.MoveToFixedPos(subLevelMainCamera, true);

                return;

            }
            else if (cameraIndex == -1 && isAtUseCaseHomePos || cameraIndex == -1 && !hasUseCases() || cameraIndex == -1 && useCaseIndex == -1)     //Return to Sub-Level Home (Back-Button 2x)
            {

                ShowroomNavigation.Instance.MoveToFixedPos(subLevelMainCamera, true);

                return;

            }
            else if (cameraIndex == -1)                                                                                                             //Return to Use-Case Home (Back-Button)
            {

                if (useCaseIndex == -1)
                {

                    ShowroomNavigation.Instance.MoveToFixedPos(subLevelMainCamera);

                    return;

                }
                else
                {

                    ShowroomNavigation.Instance.MoveToFixedPos(useCases[useCaseIndex].useCaseHomeCamera, true);

                    return;

                }

            }
            else
            {

                if(useCaseIndex == -1)
                {

                    ShowroomNavigation.Instance.MoveToFixedPos(subLevelCameras[cameraIndex]);

                    return;

                }
                else
                {

                    ShowroomNavigation.Instance.MoveToFixedPos(useCases[useCaseIndex].useCaseCameras[cameraIndex]);

                    return;

                }

            }

        }

        public void SetPlayerProperties()
        {

            if (useCaseIndex == -1)
            {

                ShowroomNavigation.Instance.allowedToWalk = playerIsAllowedToMove;
                ShowroomNavigation.Instance.allowedToTeleport = playerIsAllowedToTeleport;
                ShowroomNavigation.Instance.allowedToRotate = playerIsAllowedToRotate;

            }
            else
            {

                ShowroomNavigation.Instance.allowedToWalk = useCases[useCaseIndex].playerIsAllowedToMove;
                ShowroomNavigation.Instance.allowedToTeleport = useCases[useCaseIndex].playerIsAllowedToTeleport;
                ShowroomNavigation.Instance.allowedToRotate = useCases[useCaseIndex].playerIsAllowedToRotate;

            }

        }

        public void SwitchUseCase(int index)
        {

            if (showDebugMessages)
                Debug.Log("Use Case Index: " + index);

            if (index == -2)
            {

                bool wasHomeAlready = false;

                if((index + 1) == useCaseIndex)
                    wasHomeAlready = true;

                useCaseIndex = -1;

                MoveToFixedPos(-2);
                SetPlayerProperties();

                CodenameDockingElements.Instance.SelectSidebarButton();

                if(!wasHomeAlready)
                    CodenameDockingElements.Instance.ToggleGeneralMenu(true);

                return;
            }
            else if (index == -1)
            {
                useCaseIndex = index;

                MoveToFixedPos(-1);
                SetPlayerProperties();

                CodenameDockingElements.Instance.SelectSidebarButton();

                CodenameDockingElements.Instance.ToggleGeneralMenu(true);

                return;
            }
            else
            {
                useCaseIndex = index;

                isAtUseCaseHomePos = false;

                MoveToFixedPos(-1);

                //ShowroomNavigation.Instance.MoveToFixedPos(useCases[useCaseIndex].useCaseHomeCamera);
                SetPlayerProperties();

                CodenameDockingElements.Instance.ToggleGeneralMenu(true);

                return;
            }


        }

        public void ToggleTransparency()
        {

            #region old code

            /*

            isTransparent = !isTransparent;

            if (materials.Count == 0)
            {

                for (int i = 0; i < transparentObjects.Count; i++)
                {

                    materials.Add(transparentObjects[i].materials[0]);

                }

            }
            for (int i = 0; i < useCases.Count; i++)
            {

                if (useCases[i].materials.Count == 0)
                {

                    for (int j = 0; j < useCases[i].transparentObjects.Count; j++)
                    {

                        useCases[i].materials.Add(useCases[i].transparentObjects[j].materials[0]);

                    }

                }

            }


            StartCoroutine(ChangeMaterial(isTransparent));

            */

            #endregion


            if(isTransparent)
            {



            }

        }

        IEnumerator ChangeMaterial(bool isActive)
        {

            if (isActive)
            {
                if (xRayLinesObj != null)
                {

                    if (useCaseIndex != -1)
                    {

                        useCases[useCaseIndex].xRayLinesObj.gameObject.SetActive(true);
                        useCases[useCaseIndex].xRayLinesObj.CrossFadeInFixedTime("Activate Lines", 0.01f);

                    }
                    else
                    {

                        xRayLinesObj.gameObject.SetActive(true);
                        xRayLinesObj.CrossFadeInFixedTime("Activate Lines", 0.01f);

                    }

                }

            }

            yield return new WaitForSeconds(0.25f);

            if (isActive)
            {

                if (useCaseIndex != -1)
                {

                    for (int i = 0; i < useCases[useCaseIndex].transparentObjects.Count; i++)
                    {

                        useCases[useCaseIndex].transparentObjects[i].material = transparencyMaterial;

                    }

                }
                else
                {

                    for (int i = 0; i < transparentObjects.Count; i++)
                    {

                        transparentObjects[i].material = transparencyMaterial;

                    }

                }

            }
            else
            {

                if (xRayLinesObj != null)
                    xRayLinesObj.CrossFadeInFixedTime("Deactivate Lines", 0.01f);

                if (xRayLinesObj != null)
                    StartCoroutine(DeActivateLines());

                for (int i = 0; i < materials.Count; i++)
                {

                    transparentObjects[i].material = materials[i];

                }

                for (int i = 0; i < useCases.Count; i++)
                {

                    for (int j = 0; j < useCases[i].materials.Count; j++)
                    {

                        useCases[i].transparentObjects[j].material = materials[j];

                    }

                }

            }

            //showroomUI.generalMenuTransparencyToggle.CustomGeneralMenuModuleObj.UpdateButton();

        }

        IEnumerator DeActivateLines()
        {

            yield return new WaitForSeconds(.5f);

            xRayLinesObj.gameObject.SetActive(false);

        }

        public void EnableTransparency()
        {
            if (isTransparent)
                return;

            isTransparent = false;

            ToggleTransparency();

            //showroomUI.generalMenuTransparencyToggle.customGeneralMenuToggleObj.UpdateButton();

        }

        public void DisableTransparency()
        {
            if (!isTransparent)
                return;

            isTransparent = true;

            ToggleTransparency();

            //showroomUI.generalMenuTransparencyToggle.CustomGeneralMenuModuleObj.UpdateButton();

        }

        public void Quit()
        {

            if (isOnlyLevelInBuild)
            {

                if (showDebugMessages)
                    Debug.Log("Closing the Application!");

                Application.Quit();
            }
            else
                return;   

        }

        public void FocusOntoObject(GameObject focusObj)
        {

            currentlyFocusedObj = focusObj;

            Showroom.ShowroomNavigation.Instance.ableToRotate = false;
            Showroom.ShowroomNavigation.Instance.ableToTeleport = false;
            Showroom.ShowroomNavigation.Instance.allowedToWalk = false;

            Showroom.ShowroomNavigation.Instance.focusVolumeObj.SetActive(true);
            Showroom.ShowroomNavigation.Instance.playerCamera.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = true;

            //showroomUI.MoveCloseButtonOffScreen();
            showroomUI.ToggleSidebar(false);
            showroomUI.ToggleGeneralMenu(false);


            if (useCaseIndex == -1)
            {

                currentlyFocusedObj.transform.DOMove(focusObjectPosition.position, 1f)
                .OnComplete(() =>
                {

                    currentlyFocusedObj.GetComponent<Showroom.WorldSpace.ShowroomWorldSpaceRotatable>().enabled = true;
                    currentlyFocusedObj.GetComponent<BoxCollider>().enabled = true;

                });

            }
            else
            {

                currentlyFocusedObj.transform.DOMove(useCases[useCaseIndex].focusObjectPosition.position, 1f)
                .OnComplete(() =>
                {

                    currentlyFocusedObj.GetComponent<Showroom.WorldSpace.ShowroomWorldSpaceRotatable>().enabled = true;
                    currentlyFocusedObj.GetComponent<BoxCollider>().enabled = true;

                });

            }



        }

        public void UnfocusObject()
        {

            if (currentlyFocusedObj == null)
                return;

            currentlyFocusedObj.transform.DOLocalMove(Vector3.zero, 1f)
                .OnComplete(() =>
                {

                    currentlyFocusedObj.GetComponent<Showroom.WorldSpace.ShowroomWorldSpaceRotatable>().enabled = false;
                    currentlyFocusedObj.GetComponent<BoxCollider>().enabled = false;

                    currentlyFocusedObj = null;
                    Showroom.ShowroomNavigation.Instance.focusVolumeObj.SetActive(false);
                    Showroom.ShowroomNavigation.Instance.playerCamera.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = false;

                    if (useCaseIndex == -1)
                    {

                        Showroom.ShowroomNavigation.Instance.ableToRotate = playerIsAllowedToRotate;
                        Showroom.ShowroomNavigation.Instance.ableToTeleport = playerIsAllowedToTeleport;
                        Showroom.ShowroomNavigation.Instance.allowedToWalk = playerIsAllowedToMove;

                    }
                    else
                    {

                        Showroom.ShowroomNavigation.Instance.ableToRotate = useCases[useCaseIndex].playerIsAllowedToRotate;
                        Showroom.ShowroomNavigation.Instance.ableToTeleport = useCases[useCaseIndex].playerIsAllowedToTeleport;
                        Showroom.ShowroomNavigation.Instance.allowedToWalk = useCases[useCaseIndex].playerIsAllowedToMove;

                    }

                });

            currentlyFocusedObj.transform.DOLocalRotate(Vector3.zero, 1f);

        }

        public void ResetFocusedObjRotation()
        {

            currentlyFocusedObj.transform.DOLocalRotate(Vector3.zero, .5f);

        }

        public void OpenTimelineStepper()
        {

            //showroomUI.MoveCloseButtonOffScreen();
            showroomUI.ToggleSidebar(false);
            //showroomUI.OpenTimelineStepper();

        }

        public void CloseTimelineStepper()
        {

            //showroomUI.MoveCloseButtonOntoScreen();
            showroomUI.ToggleSidebar(true);
            //subLevelUI.MoveGeneralMenuOntoScreen();
            //showroomUI.CloseTimelineStepper();

            if (useCaseIndex == -1)
            {

                Showroom.ShowroomNavigation.Instance.ableToRotate = playerIsAllowedToRotate;
                Showroom.ShowroomNavigation.Instance.ableToTeleport = playerIsAllowedToTeleport;
                Showroom.ShowroomNavigation.Instance.allowedToWalk = playerIsAllowedToMove;

            }
            else
            {

                Showroom.ShowroomNavigation.Instance.ableToRotate = useCases[useCaseIndex].playerIsAllowedToRotate;
                Showroom.ShowroomNavigation.Instance.ableToTeleport = useCases[useCaseIndex].playerIsAllowedToTeleport;
                Showroom.ShowroomNavigation.Instance.allowedToWalk = useCases[useCaseIndex].playerIsAllowedToMove;

            }

        }

        public void PlayCurrentTimeline()
        {

            if(useCaseIndex == -1)
            {

                if (timelineSteps.Count > 0 && timelineSteps[0].timeline != null)//(currentTimeline == null && timelineSteps.Count > 0 && timelineSteps[0].timeline != null)
                {

                    if (currentTimelineTime == 0)
                    {
                        if (timelineStepperIndex != -1)
                            showroomUI.spawnedStepperButtons[timelineStepperIndex].OnMouseDown();
                        else
                            showroomUI.spawnedStepperButtons[0].OnMouseDown();

                        return;
                    }
                    else
                    {

                        currentTimeline.Play();
                        DOTween.Play("TimelineSlider");

                        return;

                    }

                }
                else if (currentTimeline == null && timelineSteps.Count == 0 || currentTimeline == null && timelineSteps[0].timeline == null)
                    return;

                //currentTimeline.time = currentTimelineTime;

                currentTimeline.Play();// -> Resume is currently Broken, Unity-team doesn't know either

                //currentTimeline.playableGraph.GetRootPlayable(0).SetSpeed(1f);

                DOTween.Play("TimelineSlider");

                return;

            }
            else
            {

                if (useCases[useCaseIndex].timelineSteps.Count > 0 && useCases[useCaseIndex].timelineSteps[0].timeline != null)//(currentTimeline == null && useCases[useCaseIndex].timelineSteps.Count > 0 && useCases[useCaseIndex].timelineSteps[0].timeline != null)
                {
                    if(currentTimelineTime == 0)
                    {
                        if (timelineStepperIndex != -1)
                            showroomUI.spawnedStepperButtons[timelineStepperIndex].OnMouseDown();
                        else
                            showroomUI.spawnedStepperButtons[0].OnMouseDown();

                        return;
                    }
                    else
                    {

                        currentTimeline.Play();
                        DOTween.Play("TimelineSlider");

                    }


                    return;

                }
                else if (currentTimeline == null && useCases[useCaseIndex].timelineSteps.Count == 0 || currentTimeline == null && useCases[useCaseIndex].timelineSteps[0].timeline == null)
                    return;

                //currentTimeline.time = currentTimelineTime;

                currentTimeline.Play();// -> Resume is currently Broken, Unity-team doesn't know either

                //currentTimeline.playableGraph.GetRootPlayable(0).SetSpeed(1f);

                DOTween.Play("TimelineSlider");

                return;

            }

        }

        public void PauseCurrentTimeline()
        {

            if (currentTimeline == null)
                return;

            currentTimeline.Pause();// -> Resume is currently Broken, Unity-team doesn't know either

            //currentTimeline.playableGraph.GetRootPlayable(0).SetSpeed(0f);

            //currentTimelineTime = (float)currentTimeline.time;

            DOTween.Pause("TimelineSlider");

        }

        public void RestartCurrentTimeline()
        {

            if (currentTimeline == null)
                return;

            currentTimeline.Stop();

            DOTween.Pause("TimelineSlider");

        }

        public void ClearCurrentTimeline()
        {

            if (currentTimeline == null)
                return;

            currentTimeline.Stop();

            DOTween.Pause("TimelineSlider");

            currentTimeline = null;

            currentTimelineTime = 0;

        }

        public void ToggleDragMode()
        {

            isDragMode = !isDragMode;

            ShowroomNavigation.Instance.dragModeActive = isDragMode;

        }

        public void DisableDragMode()
        {

            isDragMode = true;

            ToggleDragMode();
            //showroomUI.generalMenuDragModeToggle.CustomGeneralMenuModuleObj.UpdateButton();

        }

        public void PressUseCaseButtonWithoutNotice(int index)
        {

            showroomUI.headButtons[index].sidebarHeadButtonObject.sidebarButtonBehavior.OnMouseDown();

        }

        [ShowOdinSerializedPropertiesInInspector]
        public class UseCase
        {
            [InfoBox("There is no Use Case Name set!", InfoMessageType.Error, "hasNoUseCaseName")]
            [FoldoutGroup("$useCaseName")] public string useCaseName;

            bool hasNoUseCaseName()
            {
                if (string.IsNullOrEmpty(useCaseName))
                    return true;
                else
                    return false;
            }

            [FoldoutGroup("$useCaseName/General Settings")]
            [InfoBox("There is no Home Camera Set!", InfoMessageType.Error, "hasNoHomeCamera")]
            [FoldoutGroup("$useCaseName/General Settings")]
            [OdinSerialize, SerializeField, SerializeReference]
            [SceneObjectsOnly]
            public Camera useCaseHomeCamera;

            bool hasNoHomeCamera()
            {
                return !useCaseHomeCamera;
            }

            [FoldoutGroup("$useCaseName/General Settings")]
            [SceneObjectsOnly]
            public List<Camera> useCaseCameras = new List<Camera>();

            bool hasNoTransparentObjects()
            {
                if (transparentObjects.Count == 0)
                    return true;
                else
                    return false;
            }


            [FoldoutGroup("$useCaseName/General Menu Contents")] public bool hasPlayButton;
            [FoldoutGroup("$useCaseName/General Menu Contents")][ShowIf("@this.hasPlayButton == true && this.playButtonIsRestartButton != true")] public bool playButtonIsPauseButton;
            [FoldoutGroup("$useCaseName/General Menu Contents")][ShowIf("@this.hasPlayButton == true && this.hasRestartButton != true && this.playButtonIsPauseButton != true")] public bool playButtonIsRestartButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")] public bool hasTransparencyButton;
            [FoldoutGroup("$useCaseName/General Menu Contents")][ShowIf("hasTransparencyButton")] public List<MeshRenderer> transparentObjects = new List<MeshRenderer>();
            [FoldoutGroup("$useCaseName/General Menu Contents")][ShowIf("hasTransparencyButton")][SerializeField] public Animator xRayLinesObj;

            [FoldoutGroup("$useCaseName/General Menu Contents")][ShowIf("@this.playButtonIsRestartButton == false")] public bool hasRestartButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")] public bool hasResetCameraButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")] public bool hasCameraPosButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")] public bool hasDragModeButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("hasPlayButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Play Button")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuPlayButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Play Animation",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("@this.hasRestartButton || this.playButtonIsRestartButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Replay Button")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuReplayButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Restart Animation",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("playButtonIsPauseButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Pause Button")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuPauseButton;

            [FoldoutGroup("$useCaseName/General Menu Contents")] 
            [ShowIf("hasTransparencyButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Transparency Mode Toggle")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuTransparencyToggle = new CustomGeneralMenuModule_Toggle
            {

                tooltipText = "Toggle Transparency Mode",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")] 
            [ShowIf("hasCameraPosButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Camera Position Dropdown")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuCameraDropdown = new CustomGeneralMenuModule_Button
            {

                tooltipText = "",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("hasDragModeButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Drag Mode Toggle")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuDragModeToggle = new CustomGeneralMenuModule_Toggle
            {

                tooltipText = "Toggle Drag Mode",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("hasResetCameraButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Back Button")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuBackButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Back",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [ShowIf("hasResetCameraButton")]
            [BoxGroup("$useCaseName/General Menu Contents/Home Button")]
            [OdinSerialize]
            public CustomGeneralMenuModule generalMenuHomeButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Return to Home-Position",
                isIndexed = false

            };

            [FoldoutGroup("$useCaseName/General Menu Contents")]
            [OdinSerialize]
            private List<CustomGeneralMenuModule> CustomGeneralMenuModules = new List<CustomGeneralMenuModule>();


            [FoldoutGroup("$useCaseName/Focus Menu Contents")] public bool hasFocusMenu = false;

            [FoldoutGroup("$useCaseName/Focus Menu Contents")] [ShowIf("hasFocusMenu")] public Transform focusObjectPosition;

            [FoldoutGroup("$useCaseName/Focus Menu Contents")]
            [BoxGroup("$useCaseName/Focus Menu Contents/Back Button")]
            [OdinSerialize, ShowInInspector]
            [ShowIf("hasFocusMenu")]
            public CustomGeneralMenuModule focusMenuBackButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Reset Rotation",
                isIndexed = false

            };
            [FoldoutGroup("$useCaseName/Focus Menu Contents")]
            [BoxGroup("$useCaseName/Focus Menu Contents/Reset Rotation Button")]
            [OdinSerialize]
            [ShowIf("hasFocusMenu")]
            public CustomGeneralMenuModule focusMenuResetRotationButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Return Object",
                isIndexed = false

            };
            [FoldoutGroup("$useCaseName/Focus Menu Contents")]
            [BoxGroup("$useCaseName/Focus Menu Contents/Rotation Button")]
            [OdinSerialize]
            [ShowIf("hasFocusMenu")]
            public CustomGeneralMenuModule focusMenuRotationButton = new CustomGeneralMenuModule_Button
            {

                tooltipText = "Reset Rotation",
                isIndexed = false

            };

            bool hasNoResetCameraButton()
            {
                return !hasResetCameraButton;
            }

            [FoldoutGroup("$useCaseName/Bullet Points")] [ReadOnly] public int activeBulletPointIndex = -1; //== -1 => No active BulletPoint
            [FoldoutGroup("$useCaseName/Bullet Points")] public List<BulletPoint> bulletPoints = new List<BulletPoint>();


            [FoldoutGroup("$useCaseName/Player Settings")] public bool playerIsAllowedToMove;
            [FoldoutGroup("$useCaseName/Player Settings")] public bool playerIsAllowedToRotate;
            [FoldoutGroup("$useCaseName/Player Settings")] public bool playerIsAllowedToTeleport;

            [FoldoutGroup("$useCaseName/Player Settings")] [ShowIf("hasDragModeButton")] public Vector2 playerDragModeBoundaries = new Vector2(0, 0);

            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStartWalking = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStartTeleporting = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStartRotating = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStopWalking = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStopTeleporting = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onStopRotating = new List<Function>();
            [FoldoutGroup("$useCaseName/Player Settings/Player Navigation Events")] public List<Function> onPlayerReset = new List<Function>();


            [FoldoutGroup("$useCaseName/Timeline Stepper")] public bool hasTimelineStepper = false;
            [FoldoutGroup("$useCaseName/Timeline Stepper")] [ShowIf("hasTimelineStepper")] public bool timelineStepperAutoPlay = false;
            [FoldoutGroup("$useCaseName/Timeline Stepper")] [ShowIf("hasTimelineStepper")] public List<ShowroomTimelineStep> timelineSteps = new List<ShowroomTimelineStep>();

            [FoldoutGroup("$useCaseName/Sidebar Settings")] public SidebarHeadButton useCaseSidebarHeaderButton;


            [HideInInspector]
            public List<Material> materials = new List<Material>();

        }
    }

}