using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCardManager : MonoBehaviour
{
    public GameObject CurrentActivePlanet;
    public GameObject[] Planets;

    public Button planetButton, infoButton;

    public GameObject infoBox;
    public GameObject planetButtons;
    private Renderer infoRender;

    private int chosenPlanet;

    // Start is called before the first frame update
    void Start()
    {
        infoRender = infoBox.GetComponent<Renderer>();

        planetButton.onClick.AddListener(ClickPlanet);
        infoButton.onClick.AddListener(GetInfo);
    }

    void PlanetSelected()
    {
        CurrentActivePlanet.SetActive(false);

        Planets[chosenPlanet] = CurrentActivePlanet;
    }

    void ClickPlanet()
    {
        if (planetButtons.activeInHierarchy)
        {
            planetButtons.SetActive(true);
        }
        planetButtons.SetActive(false);
    }

    void GetInfo()
    {
        if (infoRender.isVisible == true)
        {
            infoRender.enabled = false;
        }
        else
            infoRender.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
