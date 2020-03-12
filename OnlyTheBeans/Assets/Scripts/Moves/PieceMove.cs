using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PieceMove : ScriptableObject
{
    public Vector3 StartLoc;
    public Vector3 EndLoc;
    //public Vector3 Direction;
    public Board.Direction Direction;
    public BoardSpace moveSpace;
    public BoardPiece myPiece;
    public float test = 0;
    public bool Initialized = false;
    //public abstract Vector3 GetTranslation();
    public abstract PieceMove GetCopy();
    public abstract string GetMyType();
    public abstract Vector3 ForwardVector();
    public abstract Vector3 BackwardVector();
    public void InitializeMove(Vector3 StartLoc, BoardPiece myPiece, BoardSpace moveSpace, Board.Direction Direction, bool lastSpace)
    {
        this.myPiece = myPiece;
        this.moveSpace = moveSpace;
        this.Direction = Direction;
        //StartLoc = myPiece.transform.position;
        this.StartLoc = StartLoc;
        InitializeMoveType(lastSpace);
    }
    public abstract void InitializeMoveType(bool isLastSpace);
    public abstract Vector3 GetEndLoc();
    public void CopyParentAttributes(PieceMove move)
    {
        move.StartLoc = StartLoc;
        move.EndLoc = EndLoc;
        move.Direction = Direction;
        move.moveSpace = moveSpace;
        move.myPiece = myPiece;
        move.Initialized = Initialized;

    }
}
