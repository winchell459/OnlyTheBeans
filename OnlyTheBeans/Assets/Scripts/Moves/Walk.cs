using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PieceMove/Walk")]
public class Walk : PieceMove
{
    public float WalkingFactor = 1;
    public override void InitializeMoveType(bool isLastSpace)
    {

        Vector3 nextSpaceStart = moveSpace.GetSpaceByDirection(Direction).GetPointStartByDirection(Direction);
        if (myPiece.transform.position.y < nextSpaceStart.y) EndLoc = nextSpaceStart;

        else EndLoc = moveSpace.GetSpaceByDirection(Direction).GetCenterPoint();
    }
    public override PieceMove GetCopy() {
        return new Walk();
    }
    public override string GetMyType()
    {

        return "Walk";
    }

    public override Vector3 ForwardVector()
    {
        return getDirection();
    }
    public override Vector3 BackwardVector()
    {
        return -getDirection();
    }
    private Vector3 getDirection()
    {
        //myPiece.currentBoardLoc = moveSpace;
        myPiece.currentBoardLoc = getPlayerBoardSpace();
        myPiece.pieceDirection = Direction;
        //Vector3 direction = (new Vector3(EndLoc.x, myPiece.transform.position.y, EndLoc.z) - myPiece.transform.position).normalized;
        //Vector3 direction = EndLoc - StartLoc;
        Vector3 direction = new Vector3(EndLoc.x, myPiece.transform.position.y, EndLoc.z) - myPiece.transform.position;
        direction = direction.normalized;
        return direction * WalkingFactor;
    }
    public override Vector3 GetEndLoc()
    {
        return new Vector3(EndLoc.x, myPiece.transform.position.y, EndLoc.z);
    }

    private BoardSpace getPlayerBoardSpace()
    {
        float x = myPiece.transform.position.x;
        float z = myPiece.transform.position.z;
        //float boardSize = moveSpace.transform.localScale
        if (Direction == Board.Direction.North || Direction == Board.Direction.South)
        {
            float boardSize = moveSpace.transform.localScale.z / 2;
            if (z < moveSpace.transform.position.z - boardSize) return moveSpace.NegZ;
            else if (z > moveSpace.transform.position.z + boardSize) return moveSpace.PosZ;
        }else if (Direction == Board.Direction.West || Direction == Board.Direction.East)
        {
            float boardSize = moveSpace.transform.localScale.x / 2;
            if (x < moveSpace.transform.position.x - boardSize) return moveSpace.NegX;
            else if (x > moveSpace.transform.position.x + boardSize) return moveSpace.PosX;
        }

        return moveSpace;
    }
}
