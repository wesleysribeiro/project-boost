using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float thrust = 250.0f;

    public float rotationSpeed = 100f;

    Rigidbody rb;
    AudioSource boostSrc;
    AudioSource losingSrc;
    AudioSource winningSrc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostSrc = GetComponentInChildren<AudioSource>();
        losingSrc = GameObject.FindWithTag("LosingAudio").GetComponent<AudioSource>();
        winningSrc = GameObject.FindWithTag("WinningAudio").GetComponent<AudioSource>();
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
            losingSrc.Play();
            Time.timeScale = 0;
            Debug.Log("Game Over, hit wall that was not supposed to touch");
        }
        else if(otherObjectTag == "EndingArea")
        {
            winningSrc.Play();
            // TODO: Implement a way to win only when the player vehicle is completely inside ending area
            Time.timeScale = 0;
            Debug.Log("You won!!! Congratulations you have reached the final of the map");
        }
    }

    void handleMovement()
    {   
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boostSrc.PlayDelayed(0.2f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(thrust * Time.deltaTime * Vector3.up);
        }

        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(-rotationSpeed * rotation * Time.deltaTime * Vector3.forward);
    }
}
