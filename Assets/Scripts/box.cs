using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    PlayerController player;
    public float screenLeft;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0f, 0f)).x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

            pos.x -= player.velocity.x * Time.fixedDeltaTime;

            if (pos.x < screenLeft)
            {
                Destroy(gameObject);
            }

            transform.position = pos;
        }
    }
}
