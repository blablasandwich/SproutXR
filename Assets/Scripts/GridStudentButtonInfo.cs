using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridStudentButtonInfo : MonoBehaviour
{
    public Image pic;
    public TextMeshProUGUI studentName;
    public string password;
    public Button btn;
    public LoginFromAPI api;
    public GameObject cardUI;
    public GameObject Library;
    // Start is called before the first frame update

    public void Start()
    {
        api = GameObject.Find("InitialMenu_Canvas").GetComponent<LoginFromAPI>();
        cardUI = GameObject.Find("GridBasedStudentSelectionUI");
        Library = api.GetComponent<LoginUIManager>().libraryCanvas;
    }

    public void PressBtn()
    {
        api.LogInCard(studentName.text, password);
        cardUI.SetActive(false);
        api.gameObject.SetActive(false);
        Library.SetActive(true);

    }
}
