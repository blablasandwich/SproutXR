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
    //Used when buttons are clicked and will set the game list enums to these variables.
    //TODO: Unity doesn't support enums in button click functions on editor, so until that ever happens, we do this...
    public void SetGame(string name)
    {
        switch(name)
        {
            case "MedievalMath":
                game.SetGameList(GameEnumList.GameList.MedievalMath);
                game.medMathLevels = GameEnumList.MedievalMathLevels.AB_MedievalMath;
                game.selectedLevel = game.medMathLevels.ToString();
                break;
            case "WalkingSoles":
                game.SetGameList(GameEnumList.GameList.WalkingSoles);
                game.walkingSolesLevels = GameEnumList.WalkingSolesLevels.AB_WalkingSoles;
                game.selectedLevel = game.walkingSolesLevels.ToString();
                break;
            default:
                break;
        }
    }
}
