using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using Sirenix.Serialization;

namespace Showroom
{

    public class ShowroomManager : MonoBehaviour
    {

        #region Singleton

        public static ShowroomManager Instance;

        private void Awake()
        {

            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

        }


        #endregion


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

        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToMove;
        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToRotate;
        [FoldoutGroup("Player Settings")] public bool playerIsAllowedToTeleport;

        [FoldoutGroup("Player Settings")][ShowIf("hasDragModeButton")] public Vector2 playerDragModeBoundaries = new Vector2(0, 0);

        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartWalking = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartTeleporting = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStartRotating = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopWalking = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopTeleporting = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onStopRotating = new List<Function>();
        [FoldoutGroup("Player Settings/Player Navigation Events")] public List<Function> onPlayerReset = new List<Function>();

        [FoldoutGroup("General Menu Contents")][ShowIf("subLevelHasGeneralMenu")] public bool hasPlayButton;
        [FoldoutGroup("General Menu Contents")][ShowIf("this.hasPlayButton == true && this.playButtonIsRestartButton != true")] public bool playButtonIsPauseButton;
        [FoldoutGroup("General Menu Contents")][ShowIf("this.hasPlayButton == true && this.hasRestartButton != true && this.playButtonIsPauseButton != true")] public bool playButtonIsRestartButton;

        [FoldoutGroup("General Menu Contents")][ShowIf("subLevelHasGeneralMenu")] public bool hasTransparencyButton;
        [FoldoutGroup("General Menu Contents")][SerializeField] private Material transparencyMaterial;
        [FoldoutGroup("General Menu Contents")][ShowIf("hasTransparencyButton")] public List<MeshRenderer> transparentObjects = new List<MeshRenderer>();
        [FoldoutGroup("General Menu Contents")][ShowIf("hasTransparencyButton")][SerializeField] public Animator xRayLinesObj;

        [FoldoutGroup("General Menu Contents")][ShowIf("playButtonIsRestartButton == false")] public bool hasRestartButton;

        [FoldoutGroup("General Menu Contents")][ShowIf("subLevelHasGeneralMenu")] public bool hasResetCameraButton;

        [FoldoutGroup("General Menu Contents")][ShowIf("subLevelHasGeneralMenu")] public bool hasCameraPosButton;

        [FoldoutGroup("General Menu Contents")][ShowIf("subLevelHasGeneralMenu")] public bool hasDragModeButton;


        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Play Button")]
        [ShowIf("hasPlayButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuPlayButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Play Animation",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Replay Button")]
        [ShowIf("hasRestartButton || playButtonIsRestartButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuReplayButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Restart Animation",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Pause Button")]
        [ShowIf("playButtonIsPauseButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuPauseButton;

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Transparency Toggle")]
        [ShowIf("hasTransparencyButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuTransparencyToggle = new CustomGeneralMenuModule_Toggle
        {

            tooltipText = "Toggle Transparency Mode",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Camera Dropdown")]
        [ShowIf("hasCameraPosButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuCameraDropdown = new CustomGeneralMenuModule_Dropdown
        {

            tooltipText = "",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Dragmode Toggle")]
        [ShowIf("hasDragModeButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuDragModeToggle = new CustomGeneralMenuModule_Toggle
        {

            tooltipText = "Toggle Drag Mode",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Back Button")]
        [ShowIf("hasResetCameraButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuBackButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Back",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [BoxGroup("General Menu Contents/Home Button")]
        [ShowIf("hasResetCameraButton")]
        [OdinSerialize]
        public CustomGeneralMenuModule generalMenuHomeButton = new CustomGeneralMenuModule_Button
        {

            tooltipText = "Return to Home-Position",
            isIndexed = false

        };

        [FoldoutGroup("General Menu Contents")]
        [ShowIf("subLevelHasGeneralMenu")]
        [OdinSerialize]
        public List<CustomGeneralMenuModule> CustomGeneralMenuModules = new List<CustomGeneralMenuModule>();

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

        [FoldoutGroup("Use Cases")][ShowIf("hasUseCases")][ReadOnly][SerializeField] public int useCaseIndex = -1;
        [FoldoutGroup("Use Cases")] public List<UseCase> useCases = new List<UseCase>();

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

        bool hasNoTransparentObjects()
        {
            if (transparentObjects.Count == 0)
                return true;
            else
                return false;
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
















        [FoldoutGroup("SSP"), InfoBox("You're using SSP Data and prefering it over the entered Data inside the Showroom Manager. This might not achieve the desired result!",
    InfoMessageType.Warning, "downloadSSPData")]

        [FoldoutGroup("SSP")]
        public bool downloadSSPData = false;

        [FoldoutGroup("SSP")]
        void GetSSPData()
        {

            ShowroomSSPDataHandler newHandler = new ShowroomSSPDataHandler();

            newHandler.GetFairtouchData();

        }

        [FoldoutGroup("SSP")]
        [Button]
        void ExportDataForSSP()
        {

            if (subLevelID == "" || string.IsNullOrEmpty(subLevelID))
                return;

            string filePath = Application.dataPath + $"/External/B12/Sublevel/{subLevelID}.csv";

            TextWriter writer = new StreamWriter(filePath, false);

            writer.WriteLine($"Sub-Level ID:; {subLevelID}");

            writer.Close();
            writer = new StreamWriter(filePath, true);

            writer.WriteLine($"Sub-Level Title:; {subLevelName}", true);
            writer.WriteLine($"Sub-Level Sub-Title:; {subLevelSubTitle}", true);

            writer.WriteLine("UseCases:", true);


            if (showroomManagerExtensions.Count > 0)
            {

                Debug.Log($"Writing... Showroom Manager Extensions ... to Sub-Level CSV file");

                for (int i = 0; i < showroomManagerExtensions.Count; i++)
                {

                    Debug.Log($"Writing... {showroomManagerExtensions[i]} ... to Sub-Level CSV file");


                    List<string> extensionData = showroomManagerExtensions[i].GetAllTextData();

                    for (int j = 0; j < extensionData.Count; j++)
                    {

                        writer.WriteLine($"{extensionData[j]}", true);

                    }

                }

            }

            for (int i = 0; i < useCases.Count; i++)
            {

                Debug.Log($"Writing... {useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonText} ... to Sub-Level CSV file");

                writer.WriteLine($"Use-Case-{i:00};{useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonText}", true);

                for (int j = 0; j < useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonSubButtons.Count; j++)
                {

                    Debug.Log($"Writing... {useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonSubButtons[j].sidebarButtonText} ... to Sub-Level CSV file");

                    writer.WriteLine($"    Use-Case-{i:00}-Sub-Button-{j:00};{useCases[i].useCaseSidebarHeaderButton.sidebarHeadButtonSubButtons[j].sidebarButtonText}", true);

                }

                writer.WriteLine("", true);

            }

            writer.Close();

            Debug.Log($"CSV file written to \"{filePath}\"");

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif

        }

    }

    public class UseCase
    {



    }

}