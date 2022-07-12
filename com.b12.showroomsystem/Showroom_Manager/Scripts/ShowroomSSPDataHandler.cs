using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;
using B12Touch.SSPContentExtension;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Showroom.UI;
using System;

namespace Showroom
{

    public class ShowroomSSPDataHandler : MonoBehaviour
    {

        #region Singleton

        public static ShowroomSSPDataHandler Instance;

        private void Awake()
        {

            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

        }

        #endregion

        public string uid;
        public string posNumber;
        public string subLevelName;
        public string subLevelSubTitle;

        public SSPContentVO tempContentVO;

        public List<SSPContentVO> sidebarTopLevelButtons = new List<SSPContentVO>();

        public List<BulletPoint> subLevelBulletPoints = new List<BulletPoint>();

        public List<ShowroomSSPDataHandlerUseCaseData> subLevelUseCases = new List<ShowroomSSPDataHandlerUseCaseData>();

        [ReadOnly]
        public bool isFinished = false;

        string path;


        public List<SSPContentVO> levels = new List<SSPContentVO>();
        public List<SSPContentVO> fLinks = new List<SSPContentVO>();
        public List<SSPContentVO> menuFolie = new List<SSPContentVO>();

        public string urlStroyApp;
        public string urlVideoPlayer;
        public string urlWebPlayer;




        public void StartDownloadingFairtouchData()
        {

            Invoke("GetFairtouchData", 2f);

        }

        public void GetFairtouchData()
        {

            uid = "ok2acav52ziachb26frthq77krumksdz3fedo";

            //if (Application.isPlaying && ShowroomManager.Instance.showDebugMessages)
            //    Debug.Log("Downloading and assigning Fairtouch Data");


            SSPContentVO startNodeVO = SSPContentExt.Instance.GetContentRootModel();

            tempContentVO = SSPContentExt.Instance.GetContentModel(uid);

            posNumber = tempContentVO.asModel().GetString("position");
            subLevelName = tempContentVO.asModel().GetString("headline");
            subLevelSubTitle = tempContentVO.asModel().GetString("subline");

            Debug.Log("Pos: " + posNumber + " | Name: " + subLevelName + " | Sub-Headline: " + subLevelSubTitle);












            sidebarTopLevelButtons = tempContentVO.asModel().GetRefList("ref_menu_item");

            for (int i = 0; i < sidebarTopLevelButtons.Count; i++)
            {

                CreateSSPHandlerUseCaseData(i);

            }

        }

        void CreateSSPHandlerUseCaseData(int useCaseIndex)
        {

            ShowroomSSPDataHandlerUseCaseData newUseCaseData = new ShowroomSSPDataHandlerUseCaseData();

            newUseCaseData.useCaseButtonText = sidebarTopLevelButtons[useCaseIndex].asModel().GetString("name");

            Debug.Log("Use-Case Button Text (" + useCaseIndex + "):" + newUseCaseData.useCaseButtonText);


            newUseCaseData.useCaseSubButtons = sidebarTopLevelButtons[useCaseIndex].asModel().GetRefList("subitems");


            for(int i = 0; i < newUseCaseData.useCaseSubButtons.Count; i++)
            {

                Debug.Log("Use Case (" + newUseCaseData.useCaseButtonText + ") Sidebar Sub Button: " + newUseCaseData.useCaseSubButtons[i].asModel().GetString("name"));

            }





            subLevelUseCases.Add(newUseCaseData);

        }

        public void StartSettingData()
        {

            Invoke("SetFairtouchData", 2f);

        }

        public void SetFairtouchData()
        {

            if (ShowroomManager.Instance.downloadFairtouchData)
            {

                ShowroomManager.Instance.subLevelName = subLevelName;
                ShowroomManager.Instance.subLevelSubTitle = subLevelSubTitle;

                for (int i = 0; i < subLevelBulletPoints.Count; i++)
                {

                    BulletPoint newBulletPoint = (BulletPoint)ScriptableObject.CreateInstance(typeof(BulletPoint));//Needs to go into the GetData Function

                    ShowroomManager.Instance.bulletPoints[i].bulletPointTitle = newBulletPoint.bulletPointTitle;
                    ShowroomManager.Instance.bulletPoints[i].bulletPointSubTitle = newBulletPoint.bulletPointSubTitle;
                    ShowroomManager.Instance.bulletPoints[i].bulletPointText = newBulletPoint.bulletPointText;

                }

                for (int i = 0; i < subLevelUseCases.Count; i++)
                {

                    //ShowroomSSPDataHandlerUseCaseData newUseCaseData = new ShowroomSSPDataHandlerUseCaseData();//Needs to go into the GetData Function

                    ShowroomManager.Instance.useCases[i].useCaseTopLevelButton.useCaseButtonName = subLevelUseCases[i].useCaseButtonText;

                    for (int j = 0; j < subLevelUseCases[i].useCaseSubButtons.Count; j++)
                    {
                    
                        ShowroomManager.Instance.useCases[i].useCaseButtons[j].useCaseButtonName = subLevelUseCases[i].useCaseSubButtons[j].asModel().GetString("name");

                    }

                    //for (int j = 0; j < subLevelUseCases[i].useCaseBulletPoints.Count; j++)
                    //{
                    //
                    //    ShowroomManager.Instance.useCases[i].bulletPoints[i].bulletPointTitle = subLevelUseCases[i].useCaseBulletPoints[j].bulletPointTitle;
                    //    ShowroomManager.Instance.useCases[i].bulletPoints[i].bulletPointSubTitle = subLevelUseCases[i].useCaseBulletPoints[j].bulletPointSubTitle;
                    //    ShowroomManager.Instance.useCases[i].bulletPoints[i].bulletPointText = subLevelUseCases[i].useCaseBulletPoints[j].bulletPointText;
                    //
                    //}

                }

                isFinished = true;

                //Showroom.ShowroomManager.Instance.showroomUI.GetNeededInfo();
                //Showroom.ShowroomManager.Instance.showroomUI.UpdateUI(true);

                ShowroomLoadingScreen loadingScreen = FindObjectOfType(typeof(ShowroomLoadingScreen)) as ShowroomLoadingScreen;
                
                loadingScreen.StartFadingLoadingScreen();

            }
            else
            {

                isFinished = true;

                //ShowroomLoadingScreen loadingScreen = FindObjectOfType(typeof(ShowroomLoadingScreen)) as ShowroomLoadingScreen;
                //
                //loadingScreen.StartFadingLoadingScreen();
                //
                //
                //float loadingScreenTimer = 0;
                //
                //DOTween.To(() => loadingScreenTimer, x => loadingScreenTimer = x, 1, .5f)
                //.OnComplete(() =>
                //{
                //
                //    Showroom.UI.CodenameDockingElements.Instance.FinishedGettingData();
                //
                //});

                StartCoroutine(WaitForLevelToFinish());


                return;

            }

            Debug.Log("Finished Setting Data");

        }


        IEnumerator WaitForLevelToFinish()
        {

            //CodenameDockingElements.Instance.GetData();

            while(!CodenameDockingElements.Instance.GetData())
            {
                yield return new WaitForSecondsRealtime(.1f);
            }

            ShowroomLoadingScreen loadingScreen = FindObjectOfType(typeof(ShowroomLoadingScreen)) as ShowroomLoadingScreen;

            loadingScreen.StartFadingLoadingScreen();

            Showroom.UI.CodenameDockingElements.Instance.FinishedGettingData();

            Showroom.ShowroomManager.Instance.StartLevel();


            yield return null;

        }








        void FindSSPData()
        {

            path = Application.dataPath + "\\CmsData";

            if (Directory.Exists(path))
            {

                if(ShowroomManager.Instance.showDebugMessages)
                    Debug.Log("Content folder exists in build");

            }
            else
            {

                // AppData/Roaming
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\B12-Updater\\default";
                if (Directory.Exists(path))
                {
                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log("Content folder exists in Roaming");

                }
                else
                {

                    // AppData/Local
                    path = Path.Combine(Application.persistentDataPath, "CmsData");

                    if (ShowroomManager.Instance.showDebugMessages)
                        Debug.Log("Content folder exists in LocalLow");

                }
            }

            SSPContentExt.Instance.SetFolders(path);
            SSPContentExt.Instance.LoadSync();

            //levels.AddRange(SSPContentExt.Instance.GetContentModelswithContentType("Level"));
            //
            //fLinks.AddRange(SSPContentExt.Instance.GetContentModelswithContentType("F-Link-Storyapp"));
            //fLinks.AddRange(SSPContentExt.Instance.GetContentModelswithContentType("F-Link-Webplayer"));
            //fLinks.AddRange(SSPContentExt.Instance.GetContentModelswithContentType("F-Link-Videoplayer"));
            //
            //menuFolie.AddRange(SSPContentExt.Instance.GetContentModelswithContentType("F-Link-Menuefolie"));

        }



    }

    public class ShowroomSSPDataHandlerUseCaseData
    {

        public string useCaseButtonText;

        public List<BulletPoint> useCaseBulletPoints = new List<BulletPoint>();

        public List<SSPContentVO> useCaseSubButtons = new List<SSPContentVO>();

    }

}