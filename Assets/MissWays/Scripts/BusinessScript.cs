using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomArrayExtensions;

namespace CustomArrayExtensions
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}

public class BusinessScript : MonoBehaviour
{
    [SerializeField] public GameObject[] myPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject selectedPrefab = myPrefabs.GetRandom();

        Instantiate(selectedPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
