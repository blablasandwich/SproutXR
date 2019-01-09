using UnityEngine;
using TMPro;

public class VarManager : MonoBehaviour
{
    void Awake()
    {
        StaticVars.UI = GameObject.Find("ScanOverlay");
        StaticVars.UIHeaderText = StaticVars.UI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshPro>();

    }
}
