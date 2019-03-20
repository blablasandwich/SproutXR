using UnityEngine;
using UnityEngine.SceneManagement;
public class InputControl : MonoBehaviour
{    
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.name == "Barrel_TriPlatesS")
                {
                    raycastHit.collider.gameObject.GetComponent<VocabBehavior>();
                }
            }
        }
    }
}
