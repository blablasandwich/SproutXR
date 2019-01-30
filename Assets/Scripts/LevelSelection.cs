using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void LevelSelect(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
