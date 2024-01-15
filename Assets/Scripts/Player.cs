using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Sprite powerUpBullet;
    [SerializeField]
    private Sprite normyBullet;
    [SerializeField]
    private ParticleSystem ps_BurstPower;
    [SerializeField]
    private GameObject Shield;

    private bool bIsThrusting; //to check if player is trying to move forward
    private float turnDirection; //to check if player is trying to rotate
    private Rigidbody2D playerRBD;

    [HideInInspector]
    public bool bCanBurst = false;
    [HideInInspector]
    public bool isBursting = false;
    [HideInInspector]
    public int scoreMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRBD = GetComponent<Rigidbody2D>();
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        Shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement check
        bIsThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        //Rotation check
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        //Shooting check
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if(bCanBurst == true && Input.GetKey(KeyCode.Space))
        {
            ps_BurstPower.transform.position = this.gameObject.transform.position;
            bulletPrefab.GetComponent<SpriteRenderer>().sprite = powerUpBullet;
            FindObjectOfType<GameManager>().ResetBar();
            this.isBursting = true;
            Invoke(nameof(ResetBullet), 15.0f);
        }
    }

    /*
     Optimal for physics updates because every machine has a different fps so cant update physics in Update function
    because it gets called every frame. But fixedupdate gets called after common intervals.
     */
    private void FixedUpdate()
    {
        //Player physics update
        if(playerRBD)
        {
            if(bIsThrusting)
            {
                playerRBD.AddForce(this.transform.up * moveSpeed);
            }

            if(turnDirection != 0.0f)
            {
                playerRBD.AddTorque(turnDirection * rotationSpeed);
            }
        }
    }

    //Shoot a bullet 
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            playerRBD.velocity = Vector3.zero;
            playerRBD.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();   
        }

        if(collision.gameObject.tag == "Shield")
        {
            Shield.SetActive(true);
            this.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
            Destroy(collision.gameObject);
            Invoke(nameof(DisableShield), 10);
        }
    }

    private void ResetBullet()
    {
        bulletPrefab.GetComponent<SpriteRenderer>().sprite = normyBullet;
        this.isBursting = false;
        scoreMultiplier = 1;
    }

    private void DisableShield()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
