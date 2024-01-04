using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    [SerializeField] KeyCode quitKey = KeyCode.Escape;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(quitKey))
        {
            Application.Quit();
            Debug.Log("Quitting game");
        }
    }
}
