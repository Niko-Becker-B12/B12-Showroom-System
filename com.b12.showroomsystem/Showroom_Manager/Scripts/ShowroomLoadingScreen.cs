using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace Showroom.UI
{

    public class ShowroomLoadingScreen : MonoBehaviour
    {

        public static ShowroomLoadingScreen Instance;

        private void Awake()
        {
            
            if(Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

        }

        public CanvasGroup loadingScreenCanvasGroup;

        public Camera loadingScreenCamera;

        public Image loadingScreenBackground;
        public Image loadingScreenLogo;
        public Slider loadingScreenLoadingBar;

        public string sceneToLoad;

        public BaseUISkin baseUISkin;


        private void Start()
        {

            ApplyBaseSkin();

        }

        void ApplyBaseSkin()
        {

            loadingScreenBackground.sprite = baseUISkin.loadingScreenBackground;
            loadingScreenBackground.color = baseUISkin.loadingScreenBackgroundColor;

            loadingScreenLogo.sprite = baseUISkin.loadingScreenLogoSprite;

            loadingScreenLoadingBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = baseUISkin.loadingScreenLoadingBarColor;

        }

        float totalProgress = 0;

        public void StartSceneLoad()
        {

            StartCoroutine(LoadScene());

        }

        IEnumerator LoadScene()
        {

            Debug.Log(string.Format("Started Loading Scene {0}", sceneToLoad));

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                totalProgress = Mathf.Clamp01(asyncLoad.progress / .9f);

                loadingScreenLoadingBar.value = totalProgress;

                yield return null;
            }

            if (asyncLoad.isDone)//&& Showroom.ShowroomManager.Instance != null && Showroom.ShowroomSSPDataHandler.Instance.isFinished)
            {


                Showroom.ShowroomSSPDataHandler.Instance.StartDownloadingFairtouchData();

                loadingScreenLoadingBar.value = 1f;

                loadingScreenCamera.gameObject.SetActive(false);

                Showroom.ShowroomSSPDataHandler.Instance.StartSettingData();

            }

        }


        public void StartFadingLoadingScreen()
        {

            loadingScreenCanvasGroup.DOFade(0f, .5f)//.SetDelay(.25f)
            .OnComplete(()=>
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));

                loadingScreenCanvasGroup.gameObject.SetActive(false);

                loadingScreenCanvasGroup.interactable = false;
                loadingScreenCanvasGroup.blocksRaycasts = false;

            });

        }
    }

}