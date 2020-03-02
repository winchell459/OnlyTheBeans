using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardPiece : MonoBehaviour
{
    public Vector3 PosOffset;
    public float Height, Width;
    public bool inAir = false;
    public float Speed = 2;
    public float FallSpeed = 1;
    public float JumpSpeed = 1;

    public enum PieceStats
    {
        idle,
        isMoving,
        isFalling,
        isRising,
        isAttacking
    }
    public PieceStats PieceStat;
    public int MoveCount;

    public BoardSpace moveToBoard;
    public BoardSpace currentBoardLoc;
}
