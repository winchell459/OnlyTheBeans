using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> AllSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject sprite in AllSprites)
        {
            Vector3 direction = (sprite.transform.position - transform.position).normalized;
            sprite.transform.forward = new Vector3(direction.x, sprite.transform.forward.y,direction.z);
        }
    }
}
