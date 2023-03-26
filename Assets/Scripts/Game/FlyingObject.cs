using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    PlayerController player;
    public float screenLeft;
    public float screenRight;
    public float speed = 2.0f;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0f, 0f)).x;
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = transform.position;
        int debugInt = Random.Range(1, 4);
        pos.y += debugInt;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;

            pos.x -= (speed + player.velocity.x) * Time.fixedDeltaTime;
            //pos.y += 1f * Time.fixedDeltaTime;
            if (pos.x < screenLeft)
            {
                Destroy(gameObject);
            }

            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.velocity.x *= 0.8f;
            player.obstaclesHit++;
            Destroy(gameObject);
        }
    }
}
