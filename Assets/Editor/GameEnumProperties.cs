using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEnumList)), CanEditMultipleObjects]
public class GameEnumProperties : Editor
{
    //This script can also be used to load scenes if necessary
    public SerializedProperty
        gameProp,
        mainLevel,
        MMLevel,
        WSLevel;

    void OnEnable()
    {
        // Setup the SerializedProperties
        // Note: These variable names are the same as the ones from GameEnumList, please list them properly.
        gameProp = serializedObject.FindProperty("gameList");
        mainLevel = serializedObject.FindProperty("SelectedLevel");
        MMLevel = serializedObject.FindProperty("medMathLevels");
        WSLevel = serializedObject.FindProperty("walkingSolesLevels");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gameProp);

        GameEnumList.GameList gL = (GameEnumList.GameList)gameProp.enumValueIndex;
        
        //Edit this list as the game list grows. This will set the current level of the Asset Bundle being loaded
        //and will make debugging download properties easier to debug
        switch (gL)
        {
            case GameEnumList.GameList.none:
                GameEnumList.GameList temp = (GameEnumList.GameList)gameProp.enumValueIndex;
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp.ToString();
                break;
            case GameEnumList.GameList.MedievalMath:
                GameEnumList.MedievalMathLevels temp2 = (GameEnumList.MedievalMathLevels)MMLevel.enumValueIndex;
                EditorGUILayout.PropertyField(MMLevel, new GUIContent("medMathLevels"));
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp2.ToString();
                break;
            case GameEnumList.GameList.WalkingSoles:
                GameEnumList.WalkingSolesLevels temp3 = (GameEnumList.WalkingSolesLevels)WSLevel.enumValueIndex;
                EditorGUILayout.PropertyField(WSLevel, new GUIContent("walkingSolesLevels"));
                Selection.activeGameObject.GetComponent<GameEnumList>().selectedLevel = temp3.ToString();
                break;
            default:
                Debug.LogError("Check if game is registered on this file where this error is from. Also change it in the GameEnumList.");
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}