using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    PlayerController player;
    public float screenLeft;
    public float screenRight;
    public bool routineStarted;
    public Vector2 positionToMoveTo;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
    }

    // Start is called before the first frame update
    public float targetValue;

    void Start()
    {
        routineStarted = false;
    }

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = transform.position.y;
        while (time < duration)
        {
            if (!player.isPaused)
            {
                Vector2 pos = transform.position;
                pos.y = Mathf.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                if (time > duration)
                {
                    pos.y = endValue;
                }
                transform.position = pos;
            }
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;

            pos.x -= player.velocity.x * Time.fixedDeltaTime;

            if (pos.x < screenRight && !routineStarted)
            {
                StartCoroutine(LerpFunction(targetValue, 1));
                routineStarted = true;
            }

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
