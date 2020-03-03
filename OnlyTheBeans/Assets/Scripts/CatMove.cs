using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    public List<PieceMove> Moves;
    private int movesIndex = 0;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Moves.Count; i += 1)
        {
            Moves[i] = Moves[i].GetCopy();
        }

    }
    public bool StartMoves(BoardPiece cat, BoardSpace startSpace, Vector3 direction)
    {
        if (!isActive)
        {
            isActive = true;
            initializeMove(cat.transform.position, cat, startSpace, direction, 0);
            return true;
        }
        else
        {
            return false;
        }

    }
    private void initializeMove(Vector3 StartLoc, BoardPiece cat, BoardSpace moveSpace, Vector3 direction, int moveIndex)
    {
        bool lastMove = Moves.Count - 1 == moveIndex;
        Moves[moveIndex].InitializeMove(StartLoc, cat, moveSpace, direction, lastMove);
    }
}
