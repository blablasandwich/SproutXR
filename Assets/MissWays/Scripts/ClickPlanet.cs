using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickPlanet : MonoBehaviour
{
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.layer == 11)
                {
                    //StaticVars.UIHeaderText.SetText("ABC");
                    //StaticVars.UI.transform.GetChild(1).gameObject.SetActive(true);
                    SceneManager.LoadScene(1);
                    GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
                }
                else if(raycastHit.collider.CompareTag("Clickable")) {
                    SceneManager.LoadScene(1);
                    GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, 1);
                }

                if (raycastHit.collider.CompareTag("Floor"))
                {
                    StaticVars.UIHeaderText.SetText("Click the floor to place a portal!");
                    StaticVars.UI.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }
}
