using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 500.0f;
    private float bulletLifeTime = 10.0f;

    private Rigidbody2D bulletRBD;

    // Start is called before the first frame update
    void Awake()
    {
        bulletRBD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Bullet physics
    public void Project(Vector2 direction)
    {
        if (bulletRBD)
        {
            bulletRBD.AddForce(direction * this.bulletSpeed);
            Destroy(this.gameObject, this.bulletLifeTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
