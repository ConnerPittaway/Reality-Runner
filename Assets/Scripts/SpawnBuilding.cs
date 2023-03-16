using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    public RadialProgress rp;
    public PlayerController player;
    public float buildingHeight;
    public float buildingRightSide;
    public float screenRight;
    public float screenLeft;
    public float screenTop;
    public BoxCollider2D collider;
    public float buildingXReduction = 0.6f;
    public float buildingYReduction = 0.6f;
    bool newBuildingGenerated;
    public Rigidbody2D RB;
    public box boxTemplate;
    public Portal portalTemplate;
    public FallingObject fallingObjectTemplate;
    public FlyingObject flyingObjectTemplate;
    public List<FlyingObject> flyingObjects;
    public forcefieldPickup forceObjectTemplate;
    public backgrounds Backgrounds;
    public int currentBackground;
    public GameObject maximumBuildingHeight;
    public float maxmimumHeight;

    //New Building Data
    private SpawnBuilding newSpawnBuildingData;
    private Transform newSpawnBuildingTransform;
    private BoxCollider2D newSpawnBuildingCollider;

    //Set Variables on Instance
    private void Awake()
    {
        buildingHeight = transform.position.y + (collider.size.y / 2); //Top of the building
        //screenRight = Camera.main.transform.position.x * 2;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1f, 0f)).y;
        currentBackground = 1;
        maxmimumHeight = maximumBuildingHeight.transform.position.y;
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

            if (!newBuildingGenerated) //Ensure only one building is spawned
            {
                if (buildingRightSide < screenRight) //If building has passed right side of the screen
                {
                    newBuildingGenerated = true; //A new building has been generated
                    generateNewBuilding(); //Generate a new building
                    generateObjects();
                }
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

        float minimumY = -8f;
        if(maximumY > maxmimumHeight)
        {
            maximumY = maxmimumHeight;
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

        if (player.firstBuildingSpawned == false)
        {
            //minX -= 5;
            player.firstBuildingSpawned = true;
        }
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
            for (int i = 0; i < boxNumber; i++)
            {
                //Debug.Log("Box");
                GameObject box = Instantiate(boxTemplate.gameObject);
                BoxCollider2D boxCollider = box.GetComponent<BoxCollider2D>();
                float y = newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2);
                float width = newSpawnBuildingCollider.size.x / 2 - 1;
                float leftSide = newSpawnBuildingTransform.position.x - width;
                float rightSide = newSpawnBuildingTransform.position.x + width;
                float x = Random.Range(leftSide, rightSide);
                Vector2 boxPosition = new Vector2(x, y);
                box.transform.position = boxPosition;
            }
        }

        int spawnFallingObject = Random.Range(0, 4);
        if (spawnFallingObject == 1)
        {
            // Debug.Log("Falling Object");
            GameObject box = Instantiate(fallingObjectTemplate.gameObject);
            FallingObject fallingObject = box.GetComponent<FallingObject>();
            BoxCollider2D boxCollider = box.GetComponent<BoxCollider2D>();
            fallingObject.targetValue = newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2);
            float rightSideChild = newSpawnBuildingTransform.position.x + newSpawnBuildingCollider.size.x / 2;
            float fallPositionX = Random.Range(newSpawnBuildingTransform.position.x - (newSpawnBuildingCollider.size.x / 4), rightSideChild);
            float fallPositionY = screenTop;
            Vector2 fallingObjectPosition = new Vector2(fallPositionX, fallPositionY);
            box.transform.position = fallingObjectPosition;
        }

        int spawnFlyingObjectType = 1;
        int spawnFlyingObject = Random.Range(0, 5);
        if (spawnFlyingObject == 1)
        {
            GameObject box = null;
            if (spawnFlyingObjectType == 1)
            {
                box = Instantiate(flyingObjects[1].gameObject);
            }
            float flyPositionX = newSpawnBuildingData.buildingRightSide;
            float flyPositionY = newSpawnBuildingData.buildingHeight;// + 2.5f;
            Vector2 fallingObjectPosition = new Vector2(flyPositionX, flyPositionY);
            box.transform.position = fallingObjectPosition;
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
            float y = newSpawnBuildingData.buildingHeight + (boxCollider.size.y / 2) * forceField.transform.localScale.y;
            float x = newSpawnBuildingTransform.position.x;
            Vector2 forcePosition = new Vector2(x, y);
            forceField.transform.position = forcePosition;
        }
    }
}
