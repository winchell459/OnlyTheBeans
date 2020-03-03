using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PieceMove/Walk")]
public class Walk : PieceMove
{
    public float WalkingFactor = 1;
    public override void InitializeMoveType(bool lastSpace)
    {
        if (lastSpace) EndLoc = moveSpace.GetCenterPoint();
        else if (Direction.x > 0) EndLoc = moveSpace.GetEastPoint();
        else if (Direction.x < 0) EndLoc = moveSpace.GetWestPoint();
        else if (Direction.z > 0) EndLoc = moveSpace.GetNorthPoint();
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
        Vector3 direction = (new Vector3(EndLoc.x, myPiece.transform.position.y, EndLoc.z) - myPiece.transform.position).normalized;
        return direction * WalkingFactor;
    }
}
