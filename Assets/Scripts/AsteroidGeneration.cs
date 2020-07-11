using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneration : MonoBehaviour
{

    public float asteroidCount;

    public GameObject asteroidPrefab;

    public Vector2 heightBounds;
    public Vector2 widthBounds;

    // Start is called before the first frame update
    void Start()
    {
        GenerateAsteroids();
    }

    // Update is called once per frame
    void GenerateAsteroids()
    {
        for(int i = 0; i < asteroidCount; ++i) {
            Vector2 pos = new Vector2(Random.Range(widthBounds.x, widthBounds.y), Random.Range(heightBounds.x, heightBounds.y));
            
            GameObject go = Instantiate(asteroidPrefab, pos, Quaternion.identity);
            Debug.Log("yewot");
            //go.transform.localScale = new Vector2()
        }
    }
}
