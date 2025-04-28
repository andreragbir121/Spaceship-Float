using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Class responsible for destroying obstacles after passing player and avoid to much obstacles within the game which can slow down the game
public class DestroyObstacles : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision collision){
        Destroy(collision.gameObject);
    }
}
