using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnumList : MonoBehaviour
{
    public enum GameList
    {
        none,
        MedievalMath,
        MissWays,
        TeachAR,
        VRMath
    }
    
    //All Assetpacks contained in each of these games
    public enum MedievalMathLevels
    {
        medmathscene //TODO: rename this asset pack to kells later
    }

    public enum MissWaysLevels
    {
        Planet_Selection
    }

    //TODO: Add walking soles, etc.
    public GameList gameList;
    public MedievalMathLevels medMathLevels = MedievalMathLevels.medmathscene;
    public MissWaysLevels misswaysLevels = MissWaysLevels.Planet_Selection;
    public string selectedLevel;

    public void SetGameList(GameEnumList.GameList gL)
    {
        this.gameList = gL;
    }
}
