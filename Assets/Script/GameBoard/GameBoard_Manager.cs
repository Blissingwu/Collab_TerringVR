using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class GameObj
{
    [LabelText("GameObject")] public GameObject obj;
    [LabelText("Starting Position")] public Vector3 startingPos;
}

public class GameBoard_Manager : MonoBehaviour
{
    [LabelText("Board Cam")] public GameBoard_Cam gameBoard_Cam;
    [LabelText("All Objs")] public List<GameObj> objsInBoard;

    private void Start()
    {
        gameBoard_Cam = GetComponent<GameBoard_Cam>();

        for (int i = 0; i < objsInBoard.Count; i++)
        {
            objsInBoard[i].startingPos = objsInBoard[i].obj.transform.position;
        }
    }

    public void OnReset()
    {
        for (int i = 0; i < objsInBoard.Count; i++)
        {
            objsInBoard[i].obj.transform.position = objsInBoard[i].startingPos;
        }
    }
}
