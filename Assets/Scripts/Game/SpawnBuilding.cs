using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    //Obstacle Types 
    public enum Obstacles
    {
        BOX,
        FALLINGGBOX,
        FLYINGBOX
    };

    //Radial Progress (check if portal can be spawned)
    public RadialProgress rp;

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
    private float buildingXReduction = 0.6f;
    private float buildingYReduction = 0.6f;

    //Objects
    public box boxTemplate;
    public Portal portalTemplate;
    public FallingObject fallingObjectTemplate;
    public FlyingObject flyingObjectTemplate;
    public List<FlyingObject> flyingObjects;
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
        Debug.Log("Spawned");

        //Set Height
        buildingHeight = transform.position.y + (collider.size.y / 2); //Top of the building

        //Set Screen Variables
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1f, 0f)).y;

        //Max and Min Heights
        maximumHeight = maximumBuildingHeight.position.y;
        minimumHeight = minimumBuildingHeight.position.y;

        //Set Sprite

        //Sub To Portal Event
    }

    // Start is called before the first frame update
    void Start()
    {

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
        // s = vt
        //v = u + at -> v = 0 -> t = -30/a
        float timeToPeak = player.jumpVelocity / (-Physics2D.gravity.y * RB.gravityScale); //Time for velocity to be 0 from jump with gravity applied

        //s = vt - 1/2a * t^2
        float distanceWithGravity = (player.jumpVelocity * player.currentMaximumJumpTime) - (0.5f * (Physics2D.gravity.y * RB.gravityScale)) * (player.currentMaximumJumpTime * player.currentMaximumJumpTime);

        float maximumY = distanceWithGravity * buildingYReduction;
        maximumY += buildingHeight; //Add maximum jump height to current building height which gives the maximum height of the new building

        float minimumY = minimumHeight;
        if(maximumY > maximumHeight)
        {
            maximumY = maximumHeight;
        }
        float buildingY = Random.Range(minimumY, maximumY);

        //Time to top of jump
        float t1 = timeToPeak + player.currentMaximumJumpTime;
        //Time to fall from top y -> v(0) = 0 -> t = Sqrt(2d/a) 
        float t2 = Mathf.Sqrt((2.0f * (maximumY - buildingY)) / -(Physics2D.gravity.y * RB.gravityScale));
        float totalTime = t1 + t2; //Time in air + Time to Fall

        float maxX = totalTime * player.velocity.x;
        maxX *= buildingXReduction;
        maxX += buildingRightSide;

        float minX = buildingRightSide + 10.0f; 

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
        //To:Do Abstract Further

        //Check for Portal Spawn
        if (rp.canSpawnPortal)
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
            rp.canSpawnPortal = false;
        }

        //Spawn Obstacles
        int boxSpawnObject = Random.Range(0, 2);
        if (boxSpawnObject == 1)
        {
            int boxNumber = Random.Range(2, 3);
            createObstacle(Obstacles.BOX, boxNumber);
        }

        int spawnFallingObject = Random.Range(0, 4);
        if (spawnFallingObject == 1)
        {
            createObstacle(Obstacles.FALLINGGBOX);
        }


        int spawnFlyingObject = Random.Range(0, 5);
        if (spawnFlyingObject == 1)
        {
            createObstacle(Obstacles.FLYINGBOX);
        }

        int spawnForcefield;
        if (player.hasPowerup)
        {
            spawnForcefield = 0;
        }
        else
        {
            spawnForcefield = 1;
        }
        if (spawnForcefield == 1)
        {
            //Debug.Log("Force Box");
            GameObject forceField = Instantiate(forceObjectTemplate.gameObject);
            BoxCollider2D boxCollider = forceField.GetComponent<BoxCollider2D>();
            float y = (newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2) * forceField.transform.localScale.y) + 1.0f;
            float x = newSpawnBuildingTransform.position.x;
            Vector2 forcePosition = new Vector2(x, y);
            forceField.transform.position = forcePosition;
        }
    }

    //Factory
    private void createObstacle(Obstacles obstacle, int numberToSpawn = 1)
    {
        float x, y;
        GameObject obstacleToSpawn = null;
        BoxCollider2D boxCollider = null;
        switch (obstacle)
        {
            case Obstacles.BOX:
                float spawnWidth = newSpawnBuildingCollider.size.x / 2 - 1;
                float leftSide = newSpawnBuildingTransform.position.x - spawnWidth;
                float rightSide = newSpawnBuildingTransform.position.x + spawnWidth;
                for (int i = 0; i < numberToSpawn; i++)
                {
                    obstacleToSpawn = Instantiate(boxTemplate.gameObject);
                    boxCollider = obstacleToSpawn.GetComponent<BoxCollider2D>();
                    x = Random.Range(leftSide, rightSide);
                    y = newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2);
                    Vector2 boxPosition = new Vector2(x, y);
                    obstacleToSpawn.transform.position = boxPosition;
                }
                break;
            case Obstacles.FALLINGGBOX:
                obstacleToSpawn = Instantiate(fallingObjectTemplate.gameObject);
                FallingObject fallingObject = obstacleToSpawn.GetComponent<FallingObject>();
                boxCollider = obstacleToSpawn.GetComponent<BoxCollider2D>();
                fallingObject.targetValue = newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2);
                float rightSideChild = newSpawnBuildingTransform.position.x + newSpawnBuildingCollider.size.x / 2;
                x = Random.Range(newSpawnBuildingTransform.position.x - (newSpawnBuildingCollider.size.x / 4), rightSideChild);
                y = screenTop;
                Vector2 fallingObjectPosition = new Vector2(x, y);
                obstacleToSpawn.transform.position = fallingObjectPosition;
                break;
            case Obstacles.FLYINGBOX:
                int spawnFlyingObjectType = ((int)player.currentWorld);
                obstacleToSpawn = Instantiate(flyingObjects[spawnFlyingObjectType].gameObject);
                x = newSpawnBuildingData.buildingRightSide;
                y = newSpawnBuildingData.buildingHeight;// + 2.5f;
                Vector2 flyingObjectPosition = new Vector2(x, y);
                obstacleToSpawn.transform.position = flyingObjectPosition;
                break;
        }
    }
}
