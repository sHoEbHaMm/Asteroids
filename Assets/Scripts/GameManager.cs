using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player playerReference;
    [SerializeField]
    private ParticleSystem ps_Explosion;
    [SerializeField]
    private GameObject burstPowerBar;
    [SerializeField]
    private TMP_Text scoreText;

    private int Lives = 3;
    private float respawnTime = 3.0f;

    
    public int Score = 0; 
    // Start is called before the first frame update
    void Start()
    {
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        Lives--;

        ps_Explosion.transform.position = playerReference.transform.position;
        ps_Explosion.Play();

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
        this.Lives = 3;
        this.Score = 0;
        scoreText.text = Score.ToString();
        SceneManager.LoadScene(0); //MainMenu
    }

    private void Respawn()
    {
        this.playerReference.transform.position = Vector3.zero;
        this.playerReference.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.playerReference.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), 3.0f);
    }

    private void TurnOnCollision()
    {
        this.playerReference.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void AnimateBar()
    {
        LeanTween.scaleX(burstPowerBar, 1, 15).setOnComplete(SetBurst);
    }

    public void ResetBar()
    {
        LeanTween.scaleX(burstPowerBar, 0, 15).setOnComplete(UnsetBurst);
 
    }

    private void SetBurst()
    {
        this.playerReference.bCanBurst = true;
    }

    private void UnsetBurst()
    {
        this.playerReference.bCanBurst = false;
        this.playerReference.isBursting = false;
        this.playerReference.scoreMultiplier = 1;
        AnimateBar();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        ps_Explosion.transform.position = asteroid.transform.position;
        ps_Explosion.Play();

        if (this.playerReference.isBursting == true)
        {
            if (asteroid.size < 0.75f)
            {
                this.Score += 100;
            }
            else if (asteroid.size < 1.25f)
            {
                this.Score += 50;
            }
            else
            {
                this.Score += 25;
            }
        }
        else
        {
            if (asteroid.size < 0.75f)
            {
                this.Score += 200;
            }
            else if (asteroid.size < 1.25f)
            {
                this.Score += 100;
            }
            else
            {
                this.Score += 50;
            }
        }


        scoreText.SetText(this.Score.ToString());
    }
}
