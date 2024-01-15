using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField]
    private Shield shieldPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawnShield), 7, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnShield()
    {
        Vector3 spawnLocation = Random.insideUnitCircle.normalized * 3;

        Shield shield = Instantiate(shieldPrefab, spawnLocation, this.transform.rotation);

        Destroy(this.gameObject, 10);
    }
}
