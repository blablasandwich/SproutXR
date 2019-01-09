using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StudentOrTeacher_Canvas : CanvasNavigation 
{
#pragma warning disable
    [Header("UI References")]
    [SerializeField] private Button studentButton;
    [SerializeField] private Button teacherButton;
    [SerializeField] private Button parentButton;
    public GameObject studentProfileCanvas;
    public GameObject teacherProfileCanvas;
    public GameObject parentProfileCanvas;

    MathController mController;

	private void Start()
	{
        if (teacherButton) teacherButton.onClick.AddListener(TeacherSelected);
        if (studentButton) studentButton.onClick.AddListener(StudentSelected);
        if (parentButton) parentButton.onClick.AddListener(ParentSelected);
    }


    void TeacherSelected()
    {
        //TODO: Add button selected analytics
        GoToCustomCanvas("teacher");
    }

    void ParentSelected()
    {
        //TODO: Add button selected analytics
        GoToCustomCanvas("parent");
    }

    void StudentSelected()
    {
        //TODO: Add button selected analytics
        GoToCustomCanvas("student");
    }

    void GoToCustomCanvas(string canvasType)
    {
        switch (canvasType)
        {
            case "teacher":
                SpawnCanvas(teacherProfileCanvas);
                break;
            case "parent":
                SpawnCanvas(parentProfileCanvas);
                break;
            case "student":
                SpawnCanvas(studentProfileCanvas);
                break;
            default:
                break;
        }

    }

    void SpawnCanvas(GameObject canvas)
    {
        GameObject canvasToSpawn = null;

        canvasToSpawn = canvas;
        print("spawned canvas: " + canvasToSpawn);
        if (canvasToSpawn)
            canvasToSpawn = Instantiate(canvasToSpawn);

        Destroy(this.gameObject);
    }
}

