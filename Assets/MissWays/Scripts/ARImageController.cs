//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    public class ARImageController : MonoBehaviour
    {
        /// <summary>
        /// A prefab for visualizing an AugmentedImage.
        /// </summary>
        public OnDetectImage PlanetsPrefab;
        public OnDetectImage TelevisionPrefab;
        public OnDetectImage TelevisionPrefab2;
        public OnDetectImage TelevisionPrefab3;
        public OnDetectImage MoonPrefab;
        public OnDetectImage HousePrefab;
     
        /// <summary>
        /// The overlay containing the fit to scan user guide.
        /// </summary>
        public GameObject FitToScanOverlay;

        private Dictionary<int, OnDetectImage> m_Visualizers
            = new Dictionary<int, OnDetectImage>();

        private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();



        private void Start()
        {
            FitToScanOverlay = StaticVars.UI.transform.GetChild(0).gameObject;
        }



        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Check that motion tracking is tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.

            //**************************************************
            //***********************READ ME********************
            // img targets 1-7 are planets 8+ are other things
            //**************************************************
            //**************************************************
            foreach (var image in m_TempAugmentedImages)
            {
                OnDetectImage visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose);

                    if (image.DatabaseIndex == 8)
                    {
                        visualizer = (OnDetectImage)Instantiate(TelevisionPrefab, anchor.transform);
                        visualizer.Image = image;
                        m_Visualizers.Add(image.DatabaseIndex, visualizer);
                        StartCoroutine(TrackTimeOnTarget(8));
                    }
                    else if (image.DatabaseIndex == 9)
                    {
                        visualizer = (OnDetectImage)Instantiate(MoonPrefab, anchor.transform);
                        visualizer.Image = image;
                        m_Visualizers.Add(image.DatabaseIndex, visualizer);
                        StartCoroutine(TrackTimeOnTarget(9));
                    }
                    else if (image.DatabaseIndex == 10)
                    {
                        visualizer = (OnDetectImage)Instantiate(HousePrefab, anchor.transform);
                        visualizer.Image = image;
                        m_Visualizers.Add(image.DatabaseIndex, visualizer);
                    }
                    else
                    {
                        switch (image.DatabaseIndex)
                        {
                            case 0:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                //StaticVars.planet = StaticVars.Planet.Mercury;
                                StaticVars.planet = StaticVars.Planet.Earth;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(0));
                                break;
                            case 1:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Venus;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(1));

                                break;
                            case 2:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Earth;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(2));
                                break;
                            case 3:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Mars;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(3));
                                break;
                            case 4:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Jupiter;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(4));
                                break;
                            case 5:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Saturn;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(5));
                                break;
                            case 6:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Uranus;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(6));
                                break;
                            case 7:
                                visualizer = (OnDetectImage)Instantiate(PlanetsPrefab, anchor.transform);
                                visualizer.Image = image;
                                StaticVars.planet = StaticVars.Planet.Neptune;
                                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                                StartCoroutine(TrackTimeOnTarget(7));
                                break;
                        }
                    }
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {
                    m_Visualizers.Remove(image.DatabaseIndex);
                    GameObject.Destroy(visualizer.gameObject);

                    //send img target analytics
                    StopCoroutine("TrackTimeOnTarget");
                    StaticVars.ImgTargetPost();
                    //reset for next analytics
                    StaticVars.TimeSpentOnTarget = 0;
                    StaticVars.CurrentTarget = StaticVars.NULL;
                }
            }

            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var visualizer in m_Visualizers.Values)
            {
                if (visualizer.Image.TrackingState == TrackingState.Tracking)
                {
                    FitToScanOverlay.SetActive(false);
                    //StaticVars.UI.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Close");
                    return;
                }
            }

            FitToScanOverlay.SetActive(true);
        }

        IEnumerator TrackTimeOnTarget(int target)
        {
            while (StaticVars.CurrentTarget!=StaticVars.NULL)
            {
                StaticVars.TimeSpentOnTarget++;
                print(StaticVars.TimeSpentOnTarget);
                yield return new WaitForSeconds(1f);
            }

            print("Stoped looking at the target.");

            yield return new WaitForSeconds(1f);

            print("MyCoroutine is now finished.");
        }
    }
}

