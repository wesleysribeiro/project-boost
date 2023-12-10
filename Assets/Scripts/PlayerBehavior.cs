using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public float goDownSpeed = 5.0f;

    public float rotationSpeed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting PlayerBehavior");
    }

    // Update is called once per frame
    void Update()
    {
       handleMovement();
    }

    private void OnCollisionEnter(Collision other) {
        var otherObjectTag = other.gameObject.tag;

        if(otherObjectTag == "UntouchableWall")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over, hit wall that was not supposed to touch");
        }
        else if(otherObjectTag == "EndingArea")
        {
            // TODO: Implement a way to win only when the player vehicle is completely inside ending area
            Time.timeScale = 0;
            Debug.Log("You won!!! Congratulations you have reached the final of the map");
        }
    }

    void handleMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // TODO: Needs work here
            transform.Translate(speed * Time.deltaTime * transform.up);
        }
        else
        {
            // Simulating gravity
            transform.Translate(0, -goDownSpeed * Time.deltaTime, 0);
        }

        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -rotationSpeed * rotation);
    }
}
