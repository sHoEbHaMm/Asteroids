using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private Bullet bulletPrefab;

    private bool bIsThrusting; //to check if player is trying to move forward
    private float turnDirection; //to check if player is trying to rotate
    private Rigidbody2D playerRBD;

    // Start is called before the first frame update
    void Start()
    {
        playerRBD = GetComponent<Rigidbody2D>();
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
}
