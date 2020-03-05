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

    public MoveSet moveSet;

    public enum PieceStats
    {
        idle,
        isStarting,
        isMoving,
        isFalling,
        isRising,
        isAttacking
    }
    public PieceStats PieceStat;
    public int MoveCount;

    public BoardSpace moveToBoard;
    public BoardSpace currentBoardLoc;
    public Board.Direction pieceDirection;

    /// <summary>
    /// Initialize MoveSet
    /// </summary>
    /// <returns><c>true</c>, if piece was moved, <c>false</c> otherwise.</returns>
    /// <param name="moveSet">Move set.</param>
    /// <param name="direction">Direction.</param>
    /// 
    public bool MovePiece(MoveSet moveSet, Board.Direction direction)
    {
        InitializeMoves(direction, moveSet);
        PieceStat = PieceStats.isMoving;
        return MovePiece();
    }
    //public void InitializeMoving()
    //{

    //}
    public bool MovePiece()//bool isStarting, bool forward, Board.Direction direction, MoveSet moveSet)
    {
        //if (isStarting) InitializeMoves(direction, moveSet);
        PieceMove move = moveSet.GetMove();
        Vector3 moveVector = move.ForwardVector();
        float friction = currentBoardLoc.GetFriction(transform.position.x, transform.position.z);
        moveVector = friction * Speed * Time.deltaTime * moveVector;
        float remainingDistance = Vector3.Distance(transform.position, move.GetEndLoc());
        if (remainingDistance > moveVector.magnitude)
        {
            transform.position += moveVector;
        }
        else
        {
            PieceMove nextMove = moveSet.GetNextMove(transform.position, this, currentBoardLoc, pieceDirection);
            if (moveSet.HasNextMove())
            {
                BoardSpace nextSpace = currentBoardLoc.GetSpaceByDirection(pieceDirection);
                if ((nextMove.GetMyType() == "Walk" ) && nextSpace.GetPointStartByDirection(pieceDirection).y > transform.position.y)
                {
                    pieceDirection = Board.FlipDirection(pieceDirection);
                    moveSet.GetMoveFlipped(pieceDirection, nextSpace);
                }
                else
                {
                    //currentBoardLoc = nextSpace;
                    move = nextMove;
                    float deltaTime = Time.deltaTime * moveVector.magnitude / remainingDistance;
                    moveVector = move.ForwardVector();
                    friction = 1;  //currentBoardLoc.GetFriction()
                    moveVector = friction * Speed * deltaTime * moveVector;
                }

            }
            else
            {

                transform.position = move.GetEndLoc();

                PieceStat = PieceStats.idle;
                return false;
            }




        }

        return true;
    }
    private void MovePiece(float deltaTime)
    {

    }
    public void InitializeMoves()
    {
        MovePiece(moveSet, pieceDirection);
    }
    public void InitializeMoves(Board.Direction direction, MoveSet moveSet)
    {
        this.moveSet = moveSet;
        moveSet.StartMoves(this, currentBoardLoc, direction);

    }
}
