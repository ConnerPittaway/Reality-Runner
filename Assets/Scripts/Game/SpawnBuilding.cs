using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    //Obstacle Types 
    public enum Objects
    {
        BOX,
        FALLINGGBOX,
        FLYINGBOX
    };

    //Portal Progress (check if portal can be spawned)
    public PortalRadial portalRadial;

    //Player
    public PlayerController player;

    //Building Positional Data
    public float buildingHeight;
    public float buildingRightSide;

    //Collider and Rigidbody
    public BoxCollider2D collider;
    public Rigidbody2D RB;

    //Screen Variables
    private float screenRight;
    private float screenLeft;
    private float screenTop;

    //Building Calculation Limiters
    private float buildingXReduction = 0.5f;
    private float buildingYReduction = 0.5f;

    //Objects
    public Portal portalTemplate;
    public SerializableDictionary<backgrounds.Worlds, StaticObject> staticObjects;
    public SerializableDictionary<backgrounds.Worlds, FallingObject> fallingObjects;
    public SerializableDictionary<backgrounds.Worlds, FlyingObject> flyingObjects;
    public forcefieldPickup forceObjectTemplate;

    //Max and Min Heights
    public Transform maximumBuildingHeight;
    public Transform minimumBuildingHeight;
    public float maximumHeight;
    public float minimumHeight;

    //Building Spawned?
    private bool newBuildingGenerated;

    //New Building Data - Fly Weight
    private SpawnBuilding newSpawnBuildingData;
    private Transform newSpawnBuildingTransform;
    private BoxCollider2D newSpawnBuildingCollider;

    //Set Variables on Instance
    private void Awake()
    {
        //Set Height
        buildingHeight = transform.position.y + (collider.size.y / 2); //Top of the building

        //Set Screen Variables
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1f, 0f)).y;

        //Max and Min Heights
        maximumHeight = maximumBuildingHeight.position.y;
        minimumHeight = minimumBuildingHeight.position.y;

        //Sub To Portal Event
        EventManager.WorldChanged += EventManager_OnWorldChanged;
    }

    private void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;
            pos.x -= player.velocity.x * Time.fixedDeltaTime;


            buildingRightSide = transform.position.x + (collider.size.x / 2);

            if (buildingRightSide < screenLeft) //Building has gone past the left side of the screen
            {
                Destroy(gameObject);
                return;
            }

            if (buildingRightSide < screenRight && !newBuildingGenerated) //If building has passed right side of the screen
            {
                newBuildingGenerated = true; //Ensure only one building is spawned

                //Factory
                generateNewBuilding(); //Generate a new building
                generateObjects();
            }

            transform.position = pos;
        }
    }

    private void generateNewBuilding()
    {
        GameObject newBuilding = Instantiate(gameObject);
        Transform newBuildingChild = newBuilding.transform.GetChild(0);
        BoxCollider2D newCollider = newBuilding.GetComponent<BoxCollider2D>();
        //SpriteRenderer newSprite = buildingSetChildVariables.GetComponentInChildren<SpriteRenderer>();

        int buildingSize = Random.Range(0, 2);
        if (buildingSize == 0)
        {
            newBuildingChild.localScale = new Vector3(60, 30, 1);
            newCollider.size = new Vector2(60, 30);
        }
        else if (buildingSize == 1)
        {
            newBuildingChild.localScale = new Vector3(55, 30, 1);
            newCollider.size = new Vector2(55, 30);
        }
        else
        {
            newBuildingChild.localScale = new Vector3(50, 30, 1);
            newCollider.size = new Vector2(50, 30);
        }

        Vector2 pos;

        //Y Calculation for Building Height
        float timeToPeak = player.jumpVelocity / (-Physics2D.gravity.y * RB.gravityScale); //Time for velocity to be 0 from jump with gravity applied
        float distanceWithGravity = (player.jumpVelocity * player.currentMaximumJumpTime) - (0.5f * (Physics2D.gravity.y * RB.gravityScale)) * (player.currentMaximumJumpTime * player.currentMaximumJumpTime);

        float maximumY = distanceWithGravity * buildingYReduction;
        maximumY += buildingHeight; //Add maximum jump height to current building height which gives the maximum height of the new building

        float minimumY = minimumHeight;
        if(maximumY > maximumHeight)
        {
            maximumY = maximumHeight;
        }
        float buildingY = Random.Range(minimumY, maximumY);

        //X Calculation for Building Gap
        float t1 = timeToPeak + player.currentMaximumJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maximumY - buildingY)) / -(Physics2D.gravity.y * RB.gravityScale));
        float totalTime = t1 + t2; //Time in air + Time to Fall

        float maxX = totalTime * player.velocity.x;
        maxX *= buildingXReduction;
        float minX = buildingRightSide + 5.0f;
        maxX += minX;
        float buildingX = Random.Range(minX, maxX);

        //Center of the building
        pos.x = buildingX + newCollider.size.x / 2; 
        pos.y = buildingY - newCollider.size.y / 2; 
        newBuilding.transform.position = pos;

        //Set variables for the new spawned building
        SpawnBuilding newBuildingData = newBuilding.GetComponent<SpawnBuilding>();
        newBuildingData.buildingHeight = newBuilding.transform.position.y + (newCollider.size.y / 2); //Set the building height of the new child
        newBuildingData.buildingRightSide = newBuilding.transform.position.x + (newCollider.size.x / 2);

        //Set Internal
        newSpawnBuildingData = newBuildingData;
        newSpawnBuildingTransform = newBuilding.transform;
        newSpawnBuildingCollider = newCollider;
    }

    private void generateObjects()
    {
        //Check for Portal Spawn
        if (portalRadial.isSpawningPortal)
        {
            float y;
            GameObject portal = Instantiate(portalTemplate.gameObject);
            if (newSpawnBuildingData.buildingHeight > buildingHeight)
            {
                y = newSpawnBuildingData.buildingHeight + 2.5f;
            }
            else
            {
                y = buildingHeight + 5.0f;
            }
            float leftSide = newSpawnBuildingTransform.position.x - newSpawnBuildingCollider.size.x / 2;
            float gap = (leftSide - buildingRightSide) / 2;
            float x = leftSide - gap;
            Vector2 portalPosition = new Vector2(x, y);
            portal.transform.position = portalPosition;
            portalRadial.isSpawningPortal = false;
        }

        //Spawn Objects
        int boxSpawnObject = Random.Range(0, 2);
        if (boxSpawnObject == 1)
        {
            int boxNumber = Random.Range(1, 3);
            createObject(Objects.BOX, boxNumber);
        }

        int spawnFallingObject = Random.Range(0, 4);
        if (spawnFallingObject == 1)
        {
            createObject(Objects.FALLINGGBOX);
        }


        int spawnFlyingObject = Random.Range(0, 5);
        if (spawnFlyingObject == 1)
        {
            createObject(Objects.FLYINGBOX);
        }

        int spawnForcefield;
        if (player.activeItem != PlayerController.ItemTypes.NONE || player.heldItem != PlayerController.ItemTypes.NONE)
        {
            spawnForcefield = 0; // Prevent Spawning
        }
        else
        {
            spawnForcefield = Random.Range(1, 11); //-> 1-10 since 11 is excluded so 10% chance
        }

        if (spawnForcefield == 1)
        {
            GameObject forceField = Instantiate(forceObjectTemplate.gameObject);
            BoxCollider2D boxCollider = forceField.GetComponent<BoxCollider2D>();
            float y = (newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2) * forceField.transform.localScale.y) + 1.5f;
            float x = newSpawnBuildingTransform.position.x;
            Vector2 forcePosition = new Vector2(x, y);
            forceField.transform.position = forcePosition;
        }
    }

    //Factory
    private void createObject(Objects objectType, int numberToSpawn = 1)
    {
        GameObject obstacleToSpawn = null;
        for (int i = 0; i < numberToSpawn; i++)
        {
            switch (objectType)
            {
                case Objects.BOX:
                    obstacleToSpawn = Instantiate(staticObjects[player.currentWorld].gameObject);
                    break;
                case Objects.FALLINGGBOX:
                    obstacleToSpawn = Instantiate(fallingObjects[player.currentWorld].gameObject);
                    break;
                case Objects.FLYINGBOX:
                    obstacleToSpawn = Instantiate(flyingObjects[player.currentWorld].gameObject);
                    break;
            }
            obstacleToSpawn.GetComponent<Object>().SetStartPosition(newSpawnBuildingTransform, newSpawnBuildingData, newSpawnBuildingCollider);
        }
    }

    public void EventManager_OnWorldChanged()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Platforms/" + player.currentWorld.ToString());
    }

    private void OnDestroy()
    {
        EventManager.WorldChanged -= EventManager_OnWorldChanged;
    }
}
