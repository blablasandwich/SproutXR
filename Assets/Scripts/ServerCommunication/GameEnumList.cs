using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnumList : MonoBehaviour
{
    
    public enum GameList
    {
        none,
        MedievalMath,
        WalkingSoles,
        TeachAR,
        VRMath
    }
    
    //All Assetpacks contained in each of these games
    public enum MedievalMathLevels
    {
        AB_MedievalMath
    }
    public enum WalkingSolesLevels
    {
        AB_WalkingSoles
    }

    //Add additional new game variables below here and then modify them in GameEnumProperties
    public GameList gameList;
    public MedievalMathLevels medMathLevels = MedievalMathLevels.AB_MedievalMath;
    public WalkingSolesLevels walkingSolesLevels = WalkingSolesLevels.AB_WalkingSoles;

    public string selectedLevel;

    public void SetGameList(GameEnumList.GameList gL)
    {
        this.gameList = gL;
    }
}
