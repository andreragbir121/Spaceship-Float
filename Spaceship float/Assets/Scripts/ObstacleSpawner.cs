using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public float speed; //float variable to store the speed for the cube to be moving at
    public Vector3 position = new Vector3(-107.4f, 0f, 15f);
    


    // The scale of the cube
    public Vector3 cubeScale = new Vector3(800, 800, 0);

    // The array of asteroids to spawn
    public GameObject[] asteroids;

    // The number of asteroids to spawn per frame
    public float spawnRate = 1;

    // The minimum and maximum distance from the cube center to spawn asteroids
    public float minSpawnDistance = 400f;
    public float maxSpawnDistance = 800f;

    // The initial position of the cube

    // Start is called before the first frame update
    void Start()
    {   
 // Set the position and scale of the cube
        transform.position = position;
        transform.localScale = cubeScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    
      // Move the cube along the x axis

        transform.position += Vector3.right * speed * Time.deltaTime;

        // Spawn asteroids randomly on the cube
        for (int i = 0; i < spawnRate; i++)
        {
            // Pick a random asteroid from the array
            int index = Random.Range(0, asteroids.Length);
            GameObject asteroid = asteroids[index];

            // Pick a random position on the cube surface
            Vector3 position = Random.onUnitSphere * Random.Range(minSpawnDistance, maxSpawnDistance);
            position += transform.position;

            // Instantiate the asteroid at the position
            Instantiate(asteroid, position, Quaternion.identity);
    }
    }



    void FixedUpdate(){
        //making the spawner move forward at the same same that the player object will move
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


}

