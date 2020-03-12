using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PieceMove/Up")]
public class Up : PieceMove
{
    public float UpDistance = 1.42f;
    public float UpFactor = 1;
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
        Up newUp = new Up();
        newUp.UpDistance = UpDistance;
        newUp.UpFactor = UpFactor;
        CopyParentAttributes(newUp);
        return newUp;
    }
    public override string GetMyType()
    {
        return "Up";
    }
    public override void InitializeMoveType(bool isLastSpace)
    {
        Debug.Log("UpDistance " + UpDistance);
        EndLoc = StartLoc + new Vector3(0, 1, 0) * UpDistance;
        forwardVector = UpFactor * (EndLoc - StartLoc).normalized;
    }
    public override Vector3 GetEndLoc()
    {
        return EndLoc;
    }
}
