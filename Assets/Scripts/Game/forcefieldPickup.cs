using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcefieldPickup : MonoBehaviour
{
    PlayerController player;
    public float screenLeft;

    private void Start()
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + 1f, 1f).setLoopPingPong().setEase(LeanTweenType.easeInSine);
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
    }

    private void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;

            pos.x -= player.velocity.x * Time.fixedDeltaTime;

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
            player.heldItem = PlayerController.ItemTypes.SHIELD;
            player.shieldsCollected++;
            Destroy(gameObject);
        }
    }
}
