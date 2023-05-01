using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : Object
{
    PlayerController player;
    ObjectAnimators objectAnimators;
    public float screenLeft;
    public float screenRight;
    public float speed = 2.0f;

    public override void SetStartPosition(Transform newSpawnedBuildingTransform, SpawnBuilding newSpawnedBuildingData, BoxCollider2D newSpawnedBuildingCollider)
    {
        float x = newSpawnedBuildingData.buildingRightSide;
        float y = newSpawnedBuildingData.buildingHeight;
        Vector2 flyingObjectPosition = new Vector2(x, y);
        transform.position = flyingObjectPosition;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0f, 0f)).x;

        RuntimeAnimatorController animator = GetComponent<RuntimeAnimatorController>();
        objectAnimators = GameObject.Find("Animators").GetComponent<ObjectAnimators>();
        animator = objectAnimators.GetFlyingAnimator((int)player.currentWorld) as RuntimeAnimatorController;

        //Sub to Portal Event
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
            if (player.activeItem != PlayerController.ItemTypes.SHIELD)
            {
                player.velocity.x *= 0.8f;
            }
            player.obstaclesHit++;
            Destroy(gameObject);
        }
    }
}
