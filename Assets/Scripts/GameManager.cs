using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player playerReference;

    private int Lives = 3;
    private float respawnTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        Lives--;

        if (Lives >= 0)
        {
            Invoke(nameof(Respawn), respawnTime);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //TODO
    }

    private void Respawn()
    {
        this.playerReference.transform.position = Vector3.zero;
        this.playerReference.gameObject.SetActive(true);
    }
}
