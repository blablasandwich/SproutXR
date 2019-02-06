using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEnumList))]
[RequireComponent(typeof(ServerDownload))]
public class DownloadManagerController : MonoBehaviour
{
    private GameEnumList game;
    private ServerDownload sD;

    private void Start()
    {
        game = GetComponent<GameEnumList>();
        sD = GetComponent<ServerDownload>();
    }
    //usually first or main menu is the first level of each game, so not going too much into detail
    public void SetGame(string name)
    {
        switch(name)
        {
            case "MedievalMath":
                game.SetGameList(GameEnumList.GameList.MedievalMath);
                game.medMathLevels = GameEnumList.MedievalMathLevels.medmathscene;
                break;
            case "MissWays":
                game.SetGameList(GameEnumList.GameList.MissWays);
                game.misswaysLevels = GameEnumList.MissWaysLevels.Planet_Selection;
                break;
            default:
                break;
        }
    }
}
