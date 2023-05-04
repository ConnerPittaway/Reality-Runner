using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : Object
{
    PlayerController player;
    public float screenLeft;

    public override void SetStartPosition(Transform newSpawnedBuildingTransform, SpawnBuilding newSpawnedBuildingData, BoxCollider2D newSpawnedBuildingCollider)
    {
        float spawnWidth = newSpawnedBuildingCollider.size.x / 2 - 1;
        float leftSide = newSpawnedBuildingTransform.position.x - spawnWidth;
        float rightSide = newSpawnedBuildingTransform.position.x + spawnWidth;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        float x = Random.Range(leftSide, rightSide);
        float y = newSpawnedBuildingData.buildingHeight + (boxCollider.size.y / 2);
        Vector2 boxPosition = new Vector2(x, y);
        transform.position = boxPosition;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;

        //Sub to Portal Event
        EventManager.PortalOpened += EventManager_OnPortal;
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
            if(player.activeItem != PlayerController.ItemTypes.SHIELD)
            {
                player.velocity.x *= 0.8f;
            }
            player.obstaclesHit++;
            Destroy(gameObject);
        }
    }

    private void EventManager_OnPortal()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventManager.PortalOpened -= EventManager_OnPortal;
    }
}
