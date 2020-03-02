using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAir : BoardSpace
{
    public override float GetFriction(float xPos, float zPos)
    {
        throw new System.NotImplementedException();
    }

    public override float GetHeight(float xPos, float zPos)
    {
        throw new System.NotImplementedException();
    }

    //public override Vector3 GetEastPoint()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override Vector3 GetNorthPoint()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override Vector3 GetSouthPoint()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override Vector3 GetWestPoint()
    //{
    //    throw new System.NotImplementedException();
    //}

    public override void HandleMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void myCollision(Collision collision)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
