using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PieceMove/Walk")]
public class Walk : PieceMove
{
    public float WalkingFactor = 1;
    public override void InitializeMoveType(bool isLastSpace)
    {
        moveSpace = moveSpace.GetSpaceByDirection(Direction);
        Vector3 spaceStart = moveSpace.GetPointStartByDirection(Direction);
        if (myPiece.transform.position.y < spaceStart.y) EndLoc = spaceStart;
        else if (isLastSpace) EndLoc = moveSpace.GetCenterPoint();
        else if (Direction == Board.Direction.East) EndLoc = moveSpace.GetEastPoint();
        else if (Direction == Board.Direction.West) EndLoc = moveSpace.GetWestPoint();
        else if (Direction == Board.Direction.North) EndLoc = moveSpace.GetNorthPoint();
        else EndLoc = moveSpace.GetSouthPoint();
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
        myPiece.currentBoardLoc = moveSpace;
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
}
