using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PieceMove/Down")]
public class Down : PieceMove
{
    public float DownDistance = 1;
    public float DownFactor = 1;
    private Vector3 forwardVector;
    public override Vector3 BackwardVector()
    {
        return -forwardVector;
    }

    public override Vector3 ForwardVector()
    {
        return forwardVector;
    }

    public override PieceMove GetCopy()
    {
        return new Down();
    }
    public override string GetMyType()
    {
        return "Down";
    }

    public override void InitializeMoveType(bool lastSpace)
    {
        Vector3 spaceTop = moveSpace.GetCenterPoint();
        if (spaceTop.y > myPiece.transform.position.y - DownDistance)
        {
            EndLoc = spaceTop;
        }
        else
        {
            EndLoc = new Vector3(spaceTop.x, myPiece.transform.position.y - DownDistance, spaceTop.z);
        }
        forwardVector = DownFactor * (EndLoc - StartLoc).normalized;
    }
}
