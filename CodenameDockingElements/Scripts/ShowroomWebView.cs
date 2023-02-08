using System.Collections;
using System.Collections.Generic;
using B12Touch.SSPContentExtension;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;
using Vuplex.WebView.Demos;
using Vuplex.WebView.Internal;

namespace Showroom.UI
{

    public class ShowroomWebView : MonoBehaviour
    {

        #region Singleton

        public static ShowroomWebView Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        private CanvasWebViewPrefab canvasWebViewPrefab;

        public CanvasGroup mainCanvasGroup;

        public CanvasGroup canvasWebView;

        bool keyboardAct = true;
        HardwareKeyboardListener _hardwareKeyboardListener;

        public delegate void ClosingEventHandler();
        public event ClosingEventHandler Closing;
        public List<ButtonBehavior> closeButtons = new List<ButtonBehavior>();



        // Start is called before the first frame update
        void Start()
        {
            canvasWebViewPrefab = (CanvasWebViewPrefab)GameObject.FindObjectOfType(typeof(CanvasWebViewPrefab));

            canvasWebView.alpha = 0;

            canvasWebView.blocksRaycasts = false;
            canvasWebView.interactable = false;

            mainCanvasGroup.alpha = 0;

            mainCanvasGroup.blocksRaycasts = false;
            mainCanvasGroup.interactable = false;

            //for (int i = 0; i < closeButtons.Count; i++)
            //{
            //    closeButtons[i].interactable = false;
            //}

            //LoadWebPage("https://d2vdrxov3sh01c.cloudfront.net/webplayer/index.html?device=HM22We_0&uid=hrswn5almozhpg2xcl26o7xc52tearreou6oy&lccc=de-DE&langswitch=false");

        }

        public async void LoadWebPage(string url)
        {
            canvasWebViewPrefab = (CanvasWebViewPrefab)GameObject.FindObjectOfType(typeof(CanvasWebViewPrefab));
            canvasWebViewPrefab.gameObject.SetActive(true);

            if (keyboardAct)
            {
                //SetUpHardwareKeyboard ();
                keyboardAct = false;
            }

            await canvasWebViewPrefab.WaitUntilInitialized();
            canvasWebViewPrefab.WebView.LoadUrl(url);

            CodenameDockingElements.Instance.gameObject.GetComponent<CanvasGroup>().DOFade(0f, .5f)
                .OnComplete(() => {

                    canvasWebView.blocksRaycasts = false;
                    canvasWebView.interactable = false;

                });

            mainCanvasGroup.DOFade(1.0f, .5f).SetDelay(.5f)
                .OnComplete(() => {

                mainCanvasGroup.blocksRaycasts = true;
                mainCanvasGroup.interactable = true;

            });

            canvasWebView.DOFade(1.0f, .5f).SetDelay(2.5f)
                .OnComplete(() => {

                canvasWebView.blocksRaycasts = true;
                canvasWebView.interactable = true;

            });
        }

        public void CloseWebPage()
        {
            Closing?.Invoke();

            CodenameDockingElements.Instance.gameObject.GetComponent<CanvasGroup>().DOFade(1f, .5f)
                .SetDelay(.5f)
                .OnComplete(() => {

                    canvasWebView.blocksRaycasts = true;
                    canvasWebView.interactable = true;

                });

            canvasWebView.DOFade(1.0f, .5f).OnComplete(() => {

                canvasWebView.blocksRaycasts = false;
                canvasWebView.interactable = false;

            });

            mainCanvasGroup.DOFade(0.0f, 0.5f)
                .OnComplete(() => {
                    mainCanvasGroup.blocksRaycasts = false;
                    mainCanvasGroup.interactable = false;
                    canvasWebView.alpha = 0f;

                    LoadEmptyPage();

                });
        }

        public async void LoadEmptyPage()
        {

            await canvasWebViewPrefab.WaitUntilInitialized();
            canvasWebViewPrefab.WebView.LoadUrl("About:blank");
        }

        void SetUpHardwareKeyboard()
        {

            // Send keys from the hardware (USB or Bluetooth) keyboard to the webview.
            // Use separate KeyDown() and KeyUp() methods if the webview supports
            // it, otherwise just use IWebView.SendKey().
            // https://developer.vuplex.com/webview/IWithKeyDownAndUp
            _hardwareKeyboardListener = HardwareKeyboardListener.Instantiate();
            _hardwareKeyboardListener.KeyDownReceived += (sender, eventArgs) =>
            {
                var webViewWithKeyDown = canvasWebViewPrefab.WebView as IWithKeyDownAndUp;
                if (webViewWithKeyDown != null)
                {
                    webViewWithKeyDown.KeyDown(eventArgs.Value, eventArgs.Modifiers);
                }
                else
                {
                    canvasWebViewPrefab.WebView.SendKey(eventArgs.Value);
                }
            };
            _hardwareKeyboardListener.KeyUpReceived += (sender, eventArgs) =>
            {
                var webViewWithKeyUp = canvasWebViewPrefab.WebView as IWithKeyDownAndUp;
                webViewWithKeyUp?.KeyUp(eventArgs.Value, eventArgs.Modifiers);
            };
        }

    }

}