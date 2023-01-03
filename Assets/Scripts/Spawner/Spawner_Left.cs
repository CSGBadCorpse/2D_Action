using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Left : MonoBehaviour
{
    public GameObject carPrefab;
    private Vector2 spawnPoint;

    float times = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        times -= Time.deltaTime;
        if (times < 0)
        {
            Instantiate(carPrefab, spawnPoint, Quaternion.identity);
            times = Random.Range(5, 10);
        }
    }
}
