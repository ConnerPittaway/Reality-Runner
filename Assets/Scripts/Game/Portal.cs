using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController player;
    public float screenLeft;

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
            EventManager.OnPortalOpened();
            player.numberOfRealities++;
            AudioManager.Instance.PlaySFX("Portal");;
            Destroy(gameObject);

            //Swap Reality
            backgrounds.Worlds randomWorld = GenerateRandom(player.currentWorld);
            AudioManager.Instance.SwapSong(randomWorld);
            player.Backgrounds.SwitchBackgrounds(randomWorld);
            player.currentWorld = randomWorld;
        }
    }

    private backgrounds.Worlds GenerateRandom(backgrounds.Worlds playerCurrentWorld)
    {
        //New Array Of Enums
        var data = Enum
        .GetValues(typeof(backgrounds.Worlds))
        .Cast<backgrounds.Worlds>()
        .Where(item => item != playerCurrentWorld)
        .ToArray();

        int randIndex = UnityEngine.Random.Range(0, data.Length);

        return data[randIndex];
    }
}
