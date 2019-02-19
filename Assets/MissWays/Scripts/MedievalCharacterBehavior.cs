using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedievalCharacterBehavior : MonoBehaviour
{
    private float muhSpeed = .1f;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        StartCoroutine(DelayUntilDeath());
    }

    void SetKinematic (bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
            rb.useGravity = !newValue;

            rb.drag = 5;
            rb.angularDrag = 5;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
            transform.Translate(Vector3.forward * (muhSpeed) * Time.deltaTime);
    }

    private IEnumerator DelayUntilDeath()
    {
        yield return new WaitForSeconds(.5f);

        SetKinematic(false);

        yield return new WaitForSeconds(5f);

        isDead = true;
       // SetKinematic(false);

        GetComponent<Animator>().enabled = false;

        Destroy(gameObject, 2);
    }
}
