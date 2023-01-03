using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Right : MonoBehaviour
{
    public GameObject carPrefab;
    private Vector2 spawnPoint;
    public GameObject carList;

    float times = 5f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        //InvokeRepeating("spawnCar",0,2f);
    }

    private void spawnCar()
    {
        Instantiate(carPrefab, spawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        times -= Time.deltaTime;
        if (times < 0)
        {
            GameObject spwanObj = Instantiate(carPrefab, spawnPoint, Quaternion.identity);
            spwanObj.transform.parent = carList.transform;
            times = Random.Range(2, 10);
        }
    }
}
