using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridStudentButtonInfo : MonoBehaviour
{
    public Image pic;
    public TextMeshProUGUI studentName;
    public Button btn;
    // Start is called before the first frame update


    public void PressBtn()
    {
        Debug.Log(studentName.text);
    }
}
