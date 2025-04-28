using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is targetted to the obstacles. where obstacles move directly towards the player to destroy the player object
/// </summary>

public class ObstacleFollowPlayer : MonoBehaviour
{
    public float forwardSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}
