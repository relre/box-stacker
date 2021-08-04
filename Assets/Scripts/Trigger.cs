using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    CrateSpawner crateSpawner;
    BoxCollider boxCollider;
   
    void Start()
    {
        crateSpawner = GameObject.Find("GameManager").GetComponent<CrateSpawner>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        crateSpawner.yHeight++;
        crateSpawner.ghostSpeed = (crateSpawner.ghostSpeed * 7) / 10;
        
       
    }
}
