using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessScript : MonoBehaviour
{
    public GameObject[] myPrefabs;

    int index;
    public float Timer = 2;
    public GameObject castle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= 1 * Time.deltaTime;
        if (Timer <= 0)
        {
            if (castle.GetComponent<MeshRenderer>().enabled)
            {
                index = Random.Range(0, myPrefabs.Length);

                GameObject selectedPrefab = myPrefabs[index];

                GameObject character = Instantiate(selectedPrefab, transform.position, transform.rotation);

                character.transform.parent = GameObject.Find("Castle").transform;
            }
            Timer = .5f;
        }
    }

    /*
    private IEnumerator startLate()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(SpawnDudes());
    }

    private IEnumerator SpawnDudes()
    {
        Debug.Log("It's WORKING!");

        index = Random.Range(0, myPrefabs.Length);

        GameObject selectedPrefab = myPrefabs[index];

        yield return new WaitForSeconds(3f);

        Instantiate(selectedPrefab, transform.position, transform.rotation);
    }
    */
}
