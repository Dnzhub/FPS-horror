using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform playerPosition;
    public GameObject enemyPrefab;

    private void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.DetectionAlert();
            Instantiate(enemyPrefab, playerPosition.position - playerPosition.forward * 5, playerPosition.rotation);           
            Destroy(gameObject);
        }
       

    }
}
