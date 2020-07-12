using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneration : MonoBehaviour
{

    public PlayerHandler player;

    public float asteroidCount;

    public List<GameObject> asteroidPrefabs;

    public float randomSizeOffset;

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
            Vector2 pos;
            while (true){
                pos = new Vector2(Random.Range(widthBounds.x, widthBounds.y), Random.Range(heightBounds.x, heightBounds.y));
                if(Vector2.Distance(pos, player.transform.position) > 100f){
                    break;
                }
            }

            GameObject go = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)], pos, Quaternion.identity);
            //calculate random size
            float temp = Random.Range(-randomSizeOffset, randomSizeOffset);
            float newx = go.transform.localScale.y + temp;
            float newy = go.transform.localScale.y + temp;

            go.transform.localScale = new Vector3(newx, newy);
        }
    }
}
