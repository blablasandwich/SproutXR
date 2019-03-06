using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static Animator anim;
    private static string scene;

    private static GameObject DeactiveObj;
    private static GameObject ActiveObj;

   void Awake()
    {
        SceneChanger[] objs = GameObject.FindObjectsOfType<SceneChanger>();
        if (objs.Length > 1)
      {
          Destroy(this.gameObject);
      }

     DontDestroyOnLoad(this.gameObject);
    } 

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void LoadLevel(string sceneToLoad)
    {
        scene = sceneToLoad;
        anim.SetTrigger("FadeOut");
    }

    public static void FadeGameObj(GameObject hide, GameObject show)
    {
        DeactiveObj = hide;
        ActiveObj = show;
    }

    public void OnFadeGameObjEnd()
    {
        DeactiveObj.SetActive(false);
        ActiveObj.SetActive(true);
    }

    public void OnFadeEnd()
    {
        SceneManager.LoadScene(scene);
    }

    public void SetDeactiveObj(GameObject hide)
    {
        DeactiveObj = hide;
    }

    public void SetActiveObj(GameObject show)
    {
        ActiveObj = show;
    }

    public void StartObjFade()
    {
        anim.SetTrigger("FadeObj");
    }
}
