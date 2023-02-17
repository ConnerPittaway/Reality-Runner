using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    //Speed Variables
    public float maximumAcceleration = 5.0f;
    public float acceleration = 2.0f;
    public float maxSpeed = 70.0f;
    public float distance = 0.0f;

    Rigidbody2D RB;
    public bool isDead = false;
    public bool firstBuildingSpawned = false;

    public BoxCollider2D playerCollider;

    public backgrounds Backgrounds;
    public int currentBackground;

    public float screenBottom;
    public float distanceDampener = 0.6f;
    public int numberOfRealities;
    public bool isPaused;
    public bool hasPowerup;

    private RigidbodyConstraints2D originalConstraints;

    public RadialTest2 itemRadial;
    public GameObject mainUI;

    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0, gravity);
        isPaused = false;
    }

    public void Awake()
    {
        
        RB = GetComponent<Rigidbody2D>();
        originalConstraints = RB.constraints;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0f)).y;
       // playerAnimator = GetComponent<Animator>();
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


        Vector2 currentPos = transform.position;

        
        if(currentPos.y < screenBottom)
        {
            isDead = true;
            velocity.x = 0;
            velocity.y = 0;
            mainUI.active = false;
            //Destroy(gameObject);
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
            //currentPos.y += velocity.y * Time.fixedDeltaTime;
            // * Time.fixedDeltaTime);
            if (!isJumping)
            {
                //velocity.y += (gravity) * Time.fixedDeltaTime;
                velocity.y += (Physics2D.gravity.y) * Time.fixedDeltaTime;
                //RB.velocity += Vector2.up * ((gravity*RB.gravityScale) * Time.fixedDeltaTime);
            }

            RB.velocity = Vector2.up * (velocity.y);

        }

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
        //transform.position = currentPos;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //On Collision Entry
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
                    //Debug.Log("Collision caused first:" + buildingHeight);
                    //Debug.Log("Building Height is :" + buildingHeight);
                    //Debug.Log("Player Y is: " + currentPos.y);
                }
                else
                {
                    onRoof = true;
                    velocity.y = 0;
                    //Debug.Log("Collision caused second:" + buildingHeight);
                    //Debug.Log("Building Height is :" + buildingHeight);
                    //Debug.Log("Player Y is: " + currentPos.y);
                    //currentPos.y = building.buildingHeight + 0.00001f;
                    //transform.position = currentPos;
                }
            }
            else
            {
                if(currentPos.y < building.buildingHeight && currentPos.x < building.buildingRightSide)
                {
                    velocity.x = 0;
                    velocity.y = 0;
                    //Debug.Log("Collision caused :" + buildingHeight);
                    //Debug.Log("Building Height is :" + buildingHeight);
                    //Debug.Log("Player Y is: " + currentPos.y);
                }
            }
        }

        if (collision.gameObject.CompareTag("StaticObject"))
        {
            //Vector2 currentPos = transform.position;
            box boxCollide = collision.gameObject.GetComponent<box>();
            velocity.x *= 0.8f;
            //currentPos.y = boxCollide.gameObject.transform.position.y;
            //transform.position = currentPos;
            //Debug.Log("Collision caused box");
            Destroy(boxCollide.gameObject);
        }

        if (collision.gameObject.CompareTag("FallingObject"))
        {
            //Vector2 currentPos = transform.position;
            FallingObject boxCollide = collision.gameObject.GetComponent<FallingObject>();
            velocity.x *= 0.8f;
            //currentPos.y = boxCollide.gameObject.transform.position.y;
            //transform.position = currentPos;
            //Debug.Log("Collision caused box");
            Destroy(boxCollide.gameObject);
        }

        if (collision.gameObject.CompareTag("FlyingObject"))
        {
            //Vector2 currentPos = transform.position;
            FlyingObject boxCollide = collision.gameObject.GetComponent<FlyingObject>();
            velocity.x *= 0.8f;
            //currentPos.y = boxCollide.gameObject.transform.position.y;
            //transform.position = currentPos;
            //Debug.Log("Collision caused box");
            Destroy(boxCollide.gameObject);
        }

        if (collision.gameObject.CompareTag("ForceFieldObject"))
        {
            //Vector2 currentPos = transform.position;
            forcefieldPickup boxCollide = collision.gameObject.GetComponent<forcefieldPickup>();
            hasPowerup = true;
            itemRadial.currentAmount = 100;
            //velocity.x *= 0.8f;
            //currentPos.y = boxCollide.gameObject.transform.position.y;
            //transform.position = currentPos;
            //Debug.Log("Collision caused box");
            Destroy(boxCollide.gameObject);
        }

        if (collision.gameObject.CompareTag("PortalObject"))
        {
            numberOfRealities++;
            AudioManager.Instance.PlaySFX("Portal");
            //Vector2 currentPos = transform.position;
            Portal portalCollide = collision.gameObject.GetComponent<Portal>();
            //currentPos.y = boxCollide.gameObject.transform.position.y;
            //transform.position = currentPos;
            //Debug.Log("Collision caused box");
            Destroy(portalCollide.gameObject);
            if (currentBackground == 0)
            {
                AudioManager.Instance.SwapSong("City Track");
                Backgrounds.SwitchBackgrounds(0);
                currentBackground = 1;
            }
            else
            {
                AudioManager.Instance.SwapSong("Forest Track");
                Backgrounds.SwitchBackgrounds(1);
                currentBackground = 0;
            }
        }
    }
}
