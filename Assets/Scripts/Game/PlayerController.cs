using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;

public class PlayerController : MonoBehaviour
{
    //Jump Variables
    public float gravity = -170f;
    public Vector2 velocity;
    public float buildingHeight = -2.5f;
    public bool onRoof = false;
    public float jumpVelocity = 35;

    //Holding Jump Variables
    public bool isJumping = false;
    public float currentMaximumJumpTime = 0.0f;
    public float maximumJumpTime = 0.4f;
    public float currentJumpTime = 0.0f;

    //Threshold to prevent need for frame perfect jumps
    public float jumpThreshold = 0.5f;

    //Speed/Movement Variables
    public float maximumAcceleration = 5.0f;
    public float acceleration = 2.0f;
    public float maxSpeed = 70.0f;
    Rigidbody2D RB;
    private RigidbodyConstraints2D originalConstraints;

    //Distance
    public float distance = 0.0f;
    public float distanceDampener = 0.6f;

    //Pause Control
    public bool isPaused;
    public Animator playerAnimator;

    //Death
    public bool isDead = false;

    public BoxCollider2D playerCollider;

    //Background Controller
    public backgrounds Backgrounds;
    public backgrounds.Worlds currentWorld;

    //Item Control
    //public bool hasPowerup;
    public ItemRadial itemRadial;
    public enum ItemTypes
    {
        NONE,
        SHIELD
    }
    public ItemTypes heldItem;
    public ItemTypes activeItem;

    //UI Elements
    public GameObject mainUI;

    //Screen Variables
    private float screenBottom;

    //Current Run Stats (Other Than Distance Which Is Above)
    public int numberOfRealities;
    public int coinsEarned;
    public int obstaclesHit;
    public int shieldsCollected;

    // Start is called before the first frame update
    void Start()
    {
        //Start Position
        float screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
        Vector2 pos = transform.position;
        pos.x = screenLeft - (Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x / 8.0f);
        transform.position = pos;

        Physics2D.gravity = new Vector2(0, gravity);
        isPaused = false;

        currentWorld = backgrounds.Worlds.FUTURISTIC;
        heldItem = ItemTypes.NONE;
        activeItem = ItemTypes.NONE;
    }

    public void Awake()
    {
        
        RB = GetComponent<Rigidbody2D>();
        originalConstraints = RB.constraints;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0f)).y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;
        float distanceToRoof = Mathf.Abs(currentPos.y - buildingHeight);
#if UNITY_EDITOR
        if(isPaused)
        {
            RB.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (!isPaused)
        {
            RB.constraints = originalConstraints;
            if (onRoof || distanceToRoof <= jumpThreshold)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AudioManager.Instance.PlaySFX("Jump");
                    onRoof = false;
                    velocity.y = jumpVelocity;
                    isJumping = true;
                    currentJumpTime = 0.0f;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
        }

#else
        if(isPaused)
        {
            RB.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

      if (!isPaused)
        {
        RB.constraints = originalConstraints;
      if (Input.touchCount > 0)
        {
            //if (IsPointerOverUIObject())
              //  return;
            //foreach (Touch touch in Input.touches)
            //{
            if (Input.GetTouch(0).phase == TouchPhase.Began && onRoof)
            {
                if (!IsPointerOverUIObject())
                {
                    AudioManager.Instance.PlaySFX("Jump");
                    onRoof = false;
                    velocity.y = jumpVelocity;
                    isJumping = true;
                    currentJumpTime = 0.0f;
                }
            }
            //}
            /*Touch touchData = Input.GetTouch(0);
            if (touchData.phase == TouchPhase.Began)
            {
               AudioManager.Instance.PlaySFX("Jump");
               onRoof = false;
               velocity.y = jumpVelocity;
               jumpHolding = true;
               currentJumpTime = 0.0f;
             }*/
        }

        if (Input.touchCount <= 0)
        {
            isJumping = false;
        }
        else
        {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    isJumping = false;
            //if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //  jumpHolding = false;
        }
        }
#endif



    }
    //Frame Rate Locked Update
    private void FixedUpdate()
    {
        if (isDead || isPaused)
        {
            return;
        }

        //Check for death
        Vector2 currentPos = transform.position;
        if(currentPos.y < screenBottom)
        {
            GameOver();
        }

        if(!onRoof)
        {
            if(isJumping)
            {
                currentJumpTime += Time.fixedDeltaTime;
                if(currentJumpTime >= currentMaximumJumpTime)
                {
                    isJumping = false;
                }
            }
            if (!isJumping)
            {
                velocity.y += (Physics2D.gravity.y) * Time.fixedDeltaTime;
            }
            RB.velocity = Vector2.up * (velocity.y);
        }

        //Calculate travelled distance
        distance += velocity.x * Time.fixedDeltaTime * distanceDampener;

        if (onRoof)
        {
            float velocityIncrease = velocity.x / maxSpeed;
            acceleration = maximumAcceleration * (1 - velocityIncrease);
            currentMaximumJumpTime = maximumJumpTime * velocityIncrease;
            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxSpeed)
            {
                velocity.x = maxSpeed;
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //Handle Collisions With Buildings
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building"))
        {
            SpawnBuilding building = collision.gameObject.GetComponent<SpawnBuilding>();
            Vector2 currentPos = transform.position;
            if (!onRoof)
            {
                if (currentPos.y < building.buildingHeight && currentPos.x < building.buildingRightSide)
                {
                    velocity.x = 0;
                    velocity.y = 0;
                }
                else
                {
                    onRoof = true;
                    velocity.y = 0;
                }
            }
            else
            {
                if(currentPos.y < building.buildingHeight && currentPos.x < building.buildingRightSide)
                {
                    velocity.x = 0;
                    velocity.y = 0;
                }
            }
        }
    }

    public void GameOver()
    {
        isDead = true;
        velocity.x = 0;
        velocity.y = 0;
        //mainUI.SetActive(false);

        //Update High Score
        if ((int)distance > GlobalDataManager.Instance.GetHighScore())
            GlobalDataManager.Instance.UpdateHighScore((int)distance);

        //Update High Score
        FirebaseManager.Instance.UploadHighScore();

        //Calculate Coins
        coinsEarned = (int)distance / 100;
        coinsEarned += numberOfRealities;
        if (GlobalDataManager.Instance.GetPremiumStatus())
        {
            coinsEarned += coinsEarned / 2;
        }

        GlobalDataManager.Instance.AlterCoins(coinsEarned);

        //Custom Event "distanceRan" (also includes number of realities)
        if (AnalyticsService.Instance != null)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>() { { "distance", distance }, { "realitiesExplored", numberOfRealities } };
                AnalyticsService.Instance.CustomData("distanceRan", parameters);
                AnalyticsService.Instance.Flush();
            }
            catch
            {
                Debug.LogError("Failed to upload data");
            }
        }

        UpdateStats();

        //Push Death Events
        EventManager.OnDeath();
    }

    public void UpdateStats()
    {
        if(GlobalStatsData.Instance != null)
        {
            GlobalStatsData.Instance.totalRuns++;
            GlobalStatsData.Instance.totalShieldsCollected += shieldsCollected;
            GlobalStatsData.Instance.totalObstaclesHit += obstaclesHit;
            GlobalStatsData.Instance.totalRealitiesExplored += numberOfRealities;
            GlobalStatsData.Instance.totalDistance += (int)distance;
            if (coinsEarned > GlobalStatsData.Instance.highestCoinsEarned)
            {
                GlobalStatsData.Instance.highestCoinsEarned = coinsEarned;
            }
            GlobalStatsData.Instance.totalCoinsEarned += coinsEarned;
            GlobalStatsData.Instance.SaveData();
        }
    }
}
