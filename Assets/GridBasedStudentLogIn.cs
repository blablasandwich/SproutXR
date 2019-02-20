using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBasedStudentLogIn : MonoBehaviour
{
    public GameObject studentProfilePicPrefab;
    public GameObject studentCardHolder;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 5; y++)
        {
            GameObject currentStudent = Instantiate(studentProfilePicPrefab, studentCardHolder.transform);
            GridStudentButtonInfo info = currentStudent.GetComponent<GridStudentButtonInfo>();


            Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            info.pic.color = c;

            string fakeName = "";

            for (int x = 0; x < 5; x++)
            {
                char n = (char)('A' + Random.Range(0, 26));
                fakeName += n;
            }

            info.studentName.text = fakeName;
            info.name = fakeName;
        }
    }

 

}
