using UnityEngine;
using TMPro;
public static class StaticVars 
{
    public static GameObject UI;
    public static TextMeshPro UIHeaderText;
    public static int NumOfQuestions = 0;
    public const int NULL = 50;
    public enum Planet
    { Mercury = 0,
      Venus   = 1,
      Earth   = 2,
      Mars    = 3,
      Jupiter = 4,
      Saturn  = 5,
      Uranus  = 6,
      Neptune = 7 };
    public static Planet planet;
    public static int CurrentTarget = NULL;
    public static float TimeSpentOnTarget = 0f;

    public static void ImgTargetPost()
    {
       //CurrentTarget
       //timTimeSpentOnTarget
       //send to backend
    }
}
