using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    public List<PieceMove> Moves;
    private int movesIndex = 0;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Moves.Count; i += 1)
        {
            //Moves[i] = Moves[i].GetCopy();
        }

    }
    public bool StartMoves(BoardPiece cat, BoardSpace startSpace, Board.Direction direction)
    {
        if (!isActive)
        {
            movesIndex = 0;
            isActive = true;

            initializeMove(cat.transform.position, cat, startSpace, direction, 0);
            return true;
        }
        else
        {
            return false;
        }

    }
    private void initializeMove(Vector3 StartLoc, BoardPiece cat, BoardSpace moveSpace, Board.Direction direction, int moveIndex)
    {
        bool lastMove = Moves.Count - 1 == moveIndex || Moves[moveIndex + 1].GetMyType() != "Walk" ;
        Moves[moveIndex].InitializeMove(StartLoc, cat, moveSpace, direction, lastMove);
    }
    public PieceMove GetMove()
    {
        return isActive? Moves[movesIndex] : null;
    }
    public void GetMoveFlipped(Board.Direction direction, BoardSpace newSpace)
    {
        PieceMove move = Moves[movesIndex];
        initializeMove(move.myPiece.transform.position, move.myPiece, newSpace,direction, movesIndex);
    }
    public bool HasNextMove()
    {
        if (movesIndex < Moves.Count - 1) return true;
        return false;
    }
    public PieceMove GetNextMove(Vector3 StartLoc, BoardPiece cat, BoardSpace moveSpace, Board.Direction direction)
    {
        //movesIndex += 1;
        if (movesIndex + 1 >= Moves.Count)
        {
            isActive = false;
            return null;
        }
        else
        {
            movesIndex += 1;
            initializeMove(StartLoc, cat, moveSpace, direction, movesIndex);
            return GetMove();
        }
    }
}
