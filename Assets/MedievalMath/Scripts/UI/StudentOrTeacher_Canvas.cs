using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StudentOrTeacher_Canvas : CanvasNavigation
{
#pragma warning disable
    [Header("UI References")]
    [SerializeField]
    private Button studentButton;
    [SerializeField]
    private Button teacherButton;
    [SerializeField]
    private Button parentButton;
    [SerializeField]
    private GameObject parentCanvas;
    MathController mController;

    private void Start()
    {
        if (teacherButton) teacherButton.onClick.AddListener(SchoolSelected);
        if (studentButton) studentButton.onClick.AddListener(HomePressed);
        if (parentButton) parentButton.onClick.AddListener(ParentPressed);

    }

    void SchoolSelected()
    {
        Application.OpenURL("http://dashboard.sproutxr.com/signup/teacher");
    }
    void HomePressed()
    {
        GoToNextCanvas();
    }
    void ParentPressed()
    {
        GoToNextCanvas(parentCanvas);
    }
}
