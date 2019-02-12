using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Vuforia;

public class FlashLight : MonoBehaviour
{
    bool on = false;
    public void Toggle()
    {
        on = !on;
        CameraDevice.Instance.SetFlashTorchMode(on);
    }
    /*
        AndroidJavaObject nativeObject;

        /// <summary>
        /// check if the flash light turn on
        /// </summary>
        public bool isLightOn
        {
            get
            {
                return isFlashLightOn;
            }
        }


        bool isFlashLightOn = false;

        bool isBlinkRunning = false;

        /// <summary>
        /// check if mobile devices have flash features
        /// </summary>
        public bool isHasFlashLight
        {
            get
            {
                return isSupportFlash;
            }
        }

        bool isSupportFlash = false;

    #if UNITY_ANDROID
        void _openFlash()
        {
            nativeObject.Call("openFlash", new object[0]);
        }

        void _closeFlash()
        {
            nativeObject.Call("closeFlash", new object[0]);
        }

        bool _hasFlash()
        {
            return nativeObject.Call<bool>("hasFlash", new object[0]);
        }

    #elif UNITY_IOS
        [System.Runtime.InteropServices.DllImport( "__Internal" )]
        private static extern void _openFlash();

        [System.Runtime.InteropServices.DllImport( "__Internal" )]
        private static extern void _closeFlash();

        [System.Runtime.InteropServices.DllImport( "__Internal" )]
        private static extern bool _hasFlash();
    #endif

        // Use this for initialization
        void Awake () {
    #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.Wili.FlashLight.FlashLight");
            this.nativeObject = androidJavaClass.CallStatic<AndroidJavaObject>("GetInstance", new object[0]);
            isSupportFlash = isHasFlash();
    #endif

        }

        /// <summary>
        /// fun check if have flashlight feature
        /// </summary>
        /// <returns></returns>
        public bool isHasFlash()
        {
            #if (UNITY_ANDROID || UNITY_IOS)&& !UNITY_EDITOR
            return _hasFlash ();
            #endif
            return false;
        }


        public void toggleFlash()
        {
            if (!isFlashLightOn)
            {
                openFlash();
            }
            else
            {
                closeFlash();
            }

        }

        /// <summary>
        /// open flashlight
        /// </summary>
        public void openFlash()
        {
            if (isBlinkRunning)
            {
                closeFlash();
            }
            openFlashLight();
        }


        /// <summary>
        /// close flash light
        /// </summary>
        public void closeFlash()
        {
            closeFlashLight();
            if (isBlinkRunning)
            {
                StopCoroutine("bliking");
                StopCoroutine("sosBlink");
            }
            isBlinkRunning = false;
        }


        private void closeFlashLight()
        {
    #if (UNITY_ANDROID || UNITY_IOS)&& !UNITY_EDITOR
            _closeFlash();
    #endif
            isFlashLightOn = false;
        }

        private void openFlashLight()
        {
    #if (UNITY_ANDROID || UNITY_IOS)&& !UNITY_EDITOR
            _openFlash();
    #endif
            isFlashLightOn = true;
        }


        /// <summary>
        /// start blink feature 
        /// </summary>
        /// <param name="blinktime"></param>
        public void startBlink(float blinktime = 0.2f)
        {
            StartCoroutine("bliking", blinktime);
        }

        IEnumerator bliking(float t)
        {
            while (isBlinkRunning)
            {
                if (isFlashLightOn)
                {
                    closeFlashLight();
                }
                else
                {
                    openFlashLight();
                }
                yield return new WaitForSeconds(t);
            }
        }


        /// <summary>
        /// play sos symbols signal
        /// </summary>
        public void SOS()
        {
            isBlinkRunning = true;
            StartCoroutine("sosBlink");
        }

        /// <summary>
        ///  sos signal 010101010101
        /// </summary>
        /// <returns></returns>
        IEnumerator sosBlink()
        {
            int i = 0;
            float intervalTime = 0.2f;

            while (isBlinkRunning)
            {
                if (i < 7)
                {
                    intervalTime = 0.3f;
                }
                else if (i > 6 && i < 13)
                {
                    intervalTime = 0.8f;
                }

                i++;
                if (i == 13)
                {
                    i = 0;
                }

                if (isFlashLightOn)
                {
                    closeFlashLight();
                }
                else
                {
                    openFlashLight();
                }
                yield return new WaitForSeconds(intervalTime);
            }  
        }
    }
    */
}