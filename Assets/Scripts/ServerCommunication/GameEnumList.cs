using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnumList : MonoBehaviour
{
    public enum GameList
    {
        none,
        MedievalMath
    }
    
    //All scenes contained in each of these games
    public enum MedievalMathLevels
    {
        none,
        medmathscene
    }
    //TODO: Add walking soles, etc.
    public GameList gameList;
    public MedievalMathLevels medMathLevels;
    public string selectedLevel;

}
