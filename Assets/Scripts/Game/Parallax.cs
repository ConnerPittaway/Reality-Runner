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
    public PlayerController player;
    public float parallaxAmount;
    public SpriteRenderer parentSpriteRenderer;
    public float parentBoundsFull;
    public float parentBoundsHalf;
    public float screenLeft;

    // Start is called before the first frame update
    void Start()
    {
        parentBoundsFull = parentSpriteRenderer.bounds.size.x;
        parentBoundsHalf = parentSpriteRenderer.bounds.size.x / 2;
        Debug.Log("Full " + parentBoundsFull + " Half " + parentBoundsHalf);
        right = transform.position.x + (parentBoundsFull);
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
    }

    private void Update()
    {
        if (!player.isPaused)
        {
            Vector2 pos = transform.position;
            float rightCopy = transform.position.x + (parentBoundsHalf);
            float speed = (player.velocity.x * parallaxAmount) * Time.deltaTime;
            if (rightCopy <= (screenLeft))
            {
                if (child)
                {
                    right = parentObj.transform.position.x + parentBoundsFull - speed;
                    pos.x = right;
                }
                else
                {
                    right = childObj.transform.position.x + parentBoundsFull - speed;
                    pos.x = right;
                }
            }

            pos.x -= speed;
            transform.position = pos;
        }
    }
}
