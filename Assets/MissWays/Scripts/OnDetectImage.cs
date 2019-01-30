//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class OnDetectImage : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        public GameObject Mercury;
        public GameObject Venus;
        public GameObject Earth;
        public GameObject Mars;
        public GameObject Jupiter;
        public GameObject Saturn;
        public GameObject Uranus;
        public GameObject Neptune;
        public GameObject Center;
        public bool planets = false;

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                Center.SetActive(false);
                return;
            }

            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;

            if (planets)
            {
                Testimg();
            }
            Center.transform.localPosition = new Vector3(halfWidth, 0f , halfHeight);
            Center.SetActive(true);
        }



        void Testimg()
        {
            switch (StaticVars.planet)
            {
                case StaticVars.Planet.Neptune:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(true);
                    break;
                case StaticVars.Planet.Uranus:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(true);
                    Neptune.SetActive(false);
                    break;
                case StaticVars.Planet.Saturn:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(true);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    break;
                case StaticVars.Planet.Jupiter:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(true);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    break;
                case StaticVars.Planet.Mars:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(true);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    break;
                case StaticVars.Planet.Earth:
                    Mercury.SetActive(false);
                    Venus.SetActive(false);
                    Earth.SetActive(true);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    //StaticVars.UIHeaderText.text = "You Scanned Earth!";
                    break;
                case StaticVars.Planet.Venus:
                    Mercury.SetActive(false);
                    Venus.SetActive(true);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    break;
                case StaticVars.Planet.Mercury:
                    Mercury.SetActive(true);
                    Venus.SetActive(false);
                    Earth.SetActive(false);
                    Mars.SetActive(false);
                    Jupiter.SetActive(false);
                    Saturn.SetActive(false);
                    Uranus.SetActive(false);
                    Neptune.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
