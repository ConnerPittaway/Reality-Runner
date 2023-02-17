using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
   // public float left;
    public float right;
    //public float spawn;
    public bool child;

    public GameObject parentObj;
    public GameObject childObj;
    //public float currentRight;
    //public float currentLeft;
    //public float childRight;
    public PlayerController player;
    public float parallaxAmount;
    public SpriteRenderer parentSpriteRenderer;
    //public Parallax parentParallax;
    //public Parallax childParallax;
    //public float offset;
    public float parentBoundsFull;
    public float parentBoundsHalf;
    public float screenLeft;

    // Start is called before the first frame update
    void Start()
    {
        parentBoundsFull = parentSpriteRenderer.bounds.size.x;
        parentBoundsHalf = parentSpriteRenderer.bounds.size.x /2 ;
       // left = transform.position.x - (parentBoundsHalf);
        right = transform.position.x + (parentBoundsFull);
       // Debug.Log("Right is " + transform.position.x);
       // Debug.Log("parent is " + parentBoundsFull);
        /*if (parentParallax != null)
         {
             left = parentParallax.left;
             right = transform.position.x + (parentBoundsFull);
         }
         else
         {
             left = transform.position.x - (parentBoundsHalf);
             right = transform.position.x + (parentBoundsFull);
         }*/
        //spawn = transform.position.x;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;
            pos.x -= (player.velocity.x * Time.deltaTime) * parallaxAmount;
            transform.position = pos;
        }
        //
        //currentLeft = transform.position.x - (parentSpriteRenderer.bounds.size.x / 2);
        //transform.position += new Vector3((-player.velocity.x * parallaxAmount) * Time.fixedDeltaTime, 0);
        //offset = left - (transform.position.x + (parentBoundsHalf));
    }

    private void LateUpdate()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;
            float rightCopy = transform.position.x + (parentBoundsHalf);
            if (rightCopy <= (screenLeft))
            {
                if (child)
                {
                    //right = parentParallax.currentRight + (parentBoundsHalf);
                    right = parentObj.transform.position.x + parentBoundsFull;
                    //transform.position = new Vector3(right - offset, transform.position.y);
                    //transform.position = new Vector2(right, transform.position.y);
                    pos.x = right;
                }
                else
                {
                    //right = childParallax.childRight + (parentBoundsHalf);
                    right = childObj.transform.position.x + parentBoundsFull;
                    //transform.position = new Vector3(right - offset, transform.position.y);
                    // transform.position = new Vector2(right, transform.position.y);
                    pos.x = right;
                }
            }
            transform.position = pos;
            //transform.position += new Vector3((-player.velocity.x * parallaxAmount) * Time.fixedDeltaTime, 0);
            //offset = screenLeft - (transform.position.x + (parentBoundsHalf));
        }
    }
}
