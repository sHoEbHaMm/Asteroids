using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    private float lifeTime = 30.0f;

    public Sprite[] sprites;

    public float size = 1.0f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D asteroidRBD;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        asteroidRBD = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value* 360.0f);
        this.transform.localScale = Vector3.one* this.size;
        asteroidRBD.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        asteroidRBD.AddForce(direction * this.moveSpeed);
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if(this.size * 0.5f >= this.minSize)
            {
                Split();
                Split();
            }

            Destroy(this.gameObject);
        }
    }

    private void Split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.moveSpeed);
    }
}
