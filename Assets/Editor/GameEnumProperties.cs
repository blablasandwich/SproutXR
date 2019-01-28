using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEnumList)), CanEditMultipleObjects]
public class GameEnumProperties : Editor
{
    //This script can also be used to load scenes if necessary
    public SerializedProperty
        gameProp,
        mainLevel,
        mmLevel,
        mwLevel;

    void OnEnable()
    {
        // Setup the SerializedProperties
        gameProp = serializedObject.FindProperty("gameList");
        mainLevel = serializedObject.FindProperty("selectedLevel");
        mmLevel = serializedObject.FindProperty("medMathLevels");
        mwLevel = serializedObject.FindProperty("misswaysLevels");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gameProp);

        GameEnumList.GameList gL = (GameEnumList.GameList)gameProp.enumValueIndex;
        
        //Edit this list as the game list grows
        switch (gL)
        {
            case GameEnumList.GameList.none:
                GameEnumList.GameList temp1 = (GameEnumList.GameList)gameProp.enumValueIndex;
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp1.ToString();
                break;

            case GameEnumList.GameList.MedievalMath:
                GameEnumList.MedievalMathLevels temp2 = (GameEnumList.MedievalMathLevels)mmLevel.enumValueIndex;
                EditorGUILayout.PropertyField(mmLevel, new GUIContent("medMathLevels"));
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp2.ToString();
                break;
            case GameEnumList.GameList.MissWays:
                GameEnumList.MissWaysLevels temp3 = (GameEnumList.MissWaysLevels)mwLevel.enumValueIndex;
                EditorGUILayout.PropertyField(mwLevel, new GUIContent("misswaysLevels"));
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp3.ToString();
                break;
            default:
                Debug.LogError("Check if game is registered on this file where this error is from. Also change it in the GameEnumList.");
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}