using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSpace : MonoBehaviour
{
    public float x, y, z;
    public Vector3 top;
    public BoardSpace PosX, PosZ, NegX, NegZ;
    public BoardSpaceElevations PosXElev, PosZElev, NegXElev, NegZElev;
    public enum BoardSpaceElevations
    {

        Level,
        Up,
        Down
    }
    private void Awake()
    {
        x = transform.position.x;
        z = transform.position.z;
        y = transform.position.y + transform.localScale.y / 2;
        top = new Vector3(x, y, z);
    }
    public abstract void HandleMovement();
    public abstract void myCollision(Collision collision);

    public void OnCollisionEnter(Collision collision)
    {
        
    }
    public bool GetNextPosX(BoardSpace target)
    {
        if (target == this) return true;
        else if (!PosX) return false;
        else return PosX.GetNextPosX(target); 
    }

    public bool GetNextPosZ(BoardSpace target)
    {
        if (target == this) return true;
        else if (!PosZ) return false;
        else return PosZ.GetNextPosZ(target);
    }
    public bool GetNextNegX(BoardSpace target)
    {
        if (target == this) return true;
        else if (!NegX) return false;
        else return NegX.GetNextNegX(target);
    }

    public bool GetNextNegZ(BoardSpace target)
    {
        if (target == this) return true;
        else if (!NegZ) return false;
        else return NegZ.GetNextNegZ(target);
    }
}
