using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private Asteroid asteroidPrefab;
    [SerializeField]
    private int spawnAmount = 1;
    [SerializeField]
    private float spawnDistance = 15.0f;
    [SerializeField]
    private float spawnRate = 10.0f; //Spawn asteroid every spawnRate secs
    [SerializeField]
    private float _variance = 15.0f; //angle


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance; //within a circle of radius = spawnDistance but at the edge

            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-_variance, _variance);
            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, spawnRotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}
