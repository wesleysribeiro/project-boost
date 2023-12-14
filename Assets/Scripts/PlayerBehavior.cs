using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float thrust = 250.0f;

    public float rotationSpeed = 100f;

    Rigidbody rb;
    AudioSource boostSrc;
    AudioSource losingSrc;
    AudioSource winningSrc;

    ParticleSystem rocketFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostSrc = GetComponentInChildren<AudioSource>();
        losingSrc = GameObject.FindWithTag("LosingAudio").GetComponent<AudioSource>();
        winningSrc = GameObject.FindWithTag("WinningAudio").GetComponent<AudioSource>();
        rocketFire = GetComponentInChildren<ParticleSystem>();
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
            Debug.Log("Game Over, hit wall that was not supposed to touch");      
        }
        else if(otherObjectTag == "EndingArea")
        {
            winningSrc.Play();
            // TODO: Implement a way to win only when the player vehicle is completely inside ending area
            Debug.Log("You won!!! Congratulations you have reached the final of the map");
        }

        // TODO: Somehow this does not prevent rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Invoke(nameof(reloadScene), 2f);
    }

    void reloadScene()
    {
        Debug.Log("Reloading scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void handleMovement()
    {   
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rocketFire.Play();
            boostSrc.PlayDelayed(0.2f);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            rocketFire.Stop();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(thrust * Time.deltaTime * Vector3.up);
        }

        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(-rotationSpeed * rotation * Time.deltaTime * Vector3.forward);
        Debug.Log("RigidBodyConstrains after rotation: " + rb.constraints);
    }
}
