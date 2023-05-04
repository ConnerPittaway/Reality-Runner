using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : Object
{
    PlayerController player;
    public float screenLeft;
    public float screenRight;
    public bool routineStarted;
    public Vector2 positionToMoveTo;
    public float targetValue;
    public float screenTop;

    public override void SetStartPosition(Transform newSpawnedBuildingTransform, SpawnBuilding newSpawnedBuildingData, BoxCollider2D newSpawnedBuildingCollider)
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        targetValue = newSpawnedBuildingData.buildingHeight + (boxCollider.size.y / 2);
        float rightSideChild = newSpawnedBuildingTransform.position.x + newSpawnedBuildingCollider.size.x / 2;
        float x = Random.Range(newSpawnedBuildingTransform.position.x - (newSpawnedBuildingCollider.size.x / 4), rightSideChild);
        float y = screenTop;
        Vector2 fallingObjectPosition = new Vector2(x, y);
        transform.position = fallingObjectPosition;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1f, 0f)).y;

        //Sub to Portal Event
        EventManager.PortalOpened += EventManager_OnPortal;
    }

    // Start is called before the first frame update


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
            if (player.activeItem != PlayerController.ItemTypes.SHIELD)
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
