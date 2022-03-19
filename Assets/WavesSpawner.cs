using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesSpawner : MonoBehaviour
{
    public Transform enemyPreFabCommon;
    public Transform enemyPreFabRare;   //ND
    public Transform enemyPreFabEpic;   //ND
    public Transform enemyPreFabLegendary;  //ND
    public Transform enemyPreFabGeneric;    //ND

    //Create an array with all enemies' type        ND
    //private Transform[] ennemies = {enemyPreFabCommon, enemyPreFabRare, enemyPreFabEpic, enemyPreFabLegendary, enemyPreFabGeneric};
    private int number;

    public Transform spawnPoint;    //For the position of the enemy

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Text waveCountDownText;

    private int waveIndex = 0;
    private Text waveIndexText; //ND

    void Update() {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountDownText.text = Mathf.Floor(countdown).ToString();
    }

    IEnumerator SpawnWave (){
        waveIndex++;

        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy() {     //ND
        number = Random.Range(0, 100); //returns random number between 0-99
        if (number < 50) {
            Instantiate(enemyPreFabCommon, spawnPoint.position, spawnPoint.rotation);   //Spawn Common Ennemy
        }
        else if (number >= 50 && number < 80) {
            Instantiate(enemyPreFabRare, spawnPoint.position, spawnPoint.rotation);   //Spawn Rare Ennemy
        }
        else if (number >= 80 && number < 92) {
            Instantiate(enemyPreFabEpic, spawnPoint.position, spawnPoint.rotation);   //Spawn Epic Ennemy
        }
        else if (number >= 92 && number < 98) {
            Instantiate(enemyPreFabLegendary, spawnPoint.position, spawnPoint.rotation);   //Spawn Legendary Ennemy
        }
        else {
            Instantiate(enemyPreFabGeneric, spawnPoint.position, spawnPoint.rotation);   //Spawn Generic Ennemy
        }      
    }
}
