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
    [SerializeField] ParticleSystem rocketFire;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem winningEffect;

    bool isTransitioning;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundSrc = GetComponent<AudioSource>();
        soundSrc.clip = engine;
        isTransitioning = false;
        gameManager = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManager>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.HasGameStarted())
        {
            rb.useGravity = true;
        }
        HandleMovement();
    }

    void OnWinningGame()
    {
        soundSrc.PlayOneShot(winning);
        winningEffect.Play();
        Debug.Log("You won!!! Congratulations you have reached the final of the map");
        Invoke(nameof(LoadNextLevel), 2f);
    }

    void OnLosingGame()
    {
        soundSrc.PlayOneShot(losing);
        explosion.Play();
        Debug.Log("Game Over, hit wall that was not supposed to touch");
        rocketFire.Stop();
        Invoke(nameof(ReloadScene), 2f);
    }

    bool CanMove()
    {
        return !isTransitioning && gameManager.HasGameStarted();
    }

    void OnCollisionEnter(Collision other) {
        if(!CanMove())
            return;

        var otherObjectTag = other.gameObject.tag;

        if(otherObjectTag == Tags.Wall)
        {
            OnLosingGame();
        }

        isTransitioning = true;
    }

    void OnTriggerEnter(Collider other) {
        if(!CanMove())
            return;
        
        var otherObjectTag = other.gameObject.tag;

        if(otherObjectTag == Tags.EndingObject)
        {
            OnWinningGame();
        }

        isTransitioning = true;
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
        if(!CanMove())
            return;

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
