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

    public abstract float GetHeight(float xPos, float zPos);
    public abstract float GetFriction(float xPos, float zPos);
    //public abstract Vector3 GetEastPoint();
    //public abstract Vector3 GetWestPoint();
    //public abstract Vector3 GetNorthPoint();
    //public abstract Vector3 GetSouthPoint();
    public Vector3 GetEastPoint()
    {
        float sizeX = transform.localScale.x/2;
        Vector3 directionX = transform.right * sizeX + transform.position;
        directionX = new Vector3(directionX.x, GetHeight(directionX.x, transform.position.z), transform.position.z);
        return directionX;
        
    }
    public Vector3 GetWestPoint()
    {
        float sizeX = transform.localScale.x / 2;
        Vector3 directionX = -transform.right * sizeX + transform.position;
        directionX = new Vector3(directionX.x, GetHeight(directionX.x, transform.position.z), transform.position.z);
        return directionX;

    }
    public Vector3 GetNorthPoint()
    {
        float sizeZ = transform.localScale.z / 2;
        Vector3 direction = transform.forward * sizeZ + transform.position;
        direction = new Vector3(transform.position.x, GetHeight(direction.x, transform.position.z), direction.z);
        return direction;

    }
    public Vector3 GetSouthPoint()
    {
        float sizeZ = transform.localScale.z / 2;
        Vector3 direction = -transform.forward * sizeZ + transform.position;
        direction = new Vector3(transform.position.x, GetHeight(direction.x, transform.position.z), direction.z);
        return direction;

    }
    public Vector3 GetCenterPoint()
    {
        return new Vector3(transform.position.x, GetHeight(transform.position.x, transform.position.z), transform.position.z);
    }

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
