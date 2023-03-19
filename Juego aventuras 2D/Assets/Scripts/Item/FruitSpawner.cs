using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    float timer;
    public GameObject fruitPrefab;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2f)
        {
            timer = 0;
            float x = Random.Range(-5f,5f);
            Vector3 position = new Vector3(x, 0, 0);
            Quaternion rotation = new Quaternion();
            Instantiate(fruitPrefab,position,rotation);
        }
    }
}
