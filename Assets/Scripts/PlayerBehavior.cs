using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float thrust = 250.0f;

    public float rotationSpeed = 100f;

    Rigidbody rb;
    [SerializeField] AudioClip engine;
    [SerializeField] AudioClip losing;
    [SerializeField] AudioClip winning;

    AudioSource soundSrc;
    ParticleSystem rocketFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketFire = GetComponentInChildren<ParticleSystem>();
        soundSrc = GetComponent<AudioSource>();
        soundSrc.clip = engine;
    }

    // Update is called once per frame
    void Update()
    {
       HandleMovement();
    }

    private void OnCollisionEnter(Collision other) {
        var otherObjectTag = other.gameObject.tag;

        // this.enabled = false;

        if(otherObjectTag == "UntouchableWall")
        {
            soundSrc.PlayOneShot(losing);
            Debug.Log("Game Over, hit wall that was not supposed to touch");      
            Invoke(nameof(ReloadScene), 2f);
        }
        else if(otherObjectTag == "EndingArea")
        {
            soundSrc.PlayOneShot(winning);
            // TODO: Implement a way to win only when the player vehicle is completely inside ending area
            Debug.Log("You won!!! Congratulations you have reached the final of the map");
            Invoke(nameof(LoadNextLevel), 2f);
        }
    }

    void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if(sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;
        }
        
        SceneManager.LoadScene(sceneIndex);
    }

    void ReloadScene()
    {
        Debug.Log("Reloading scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void HandleMovement()
    {   
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rocketFire.Play();
            soundSrc.Play();
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
        // Debug.Log("RigidBodyConstrains after rotation: " + rb.constraints);
    }
}
