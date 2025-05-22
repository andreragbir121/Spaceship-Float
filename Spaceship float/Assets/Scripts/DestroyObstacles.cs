using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Class responsible for destroying obstacles after passing player and avoid to much obstacles within the game which can slow down the game
public class DestroyObstacles : MonoBehaviour
{
   private Vector3 position = new Vector3(-118, 100, 0);
    // The scale of the cube
    public Vector3 cubeScale = new Vector3(800, 800, 0);
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = position;
        transform.localScale = cubeScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

    }

    void FixedUpdate(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision collision){
        Destroy(collision.gameObject);
    }
}
