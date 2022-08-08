using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Delay time when the level restart")]
    [SerializeField] float levelLoadDelay = 1f;

    void OnCollisionEnter(Collision other) {
        Debug.Log(this.name + " collided with " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        //String Interpolation
        Debug.Log($"{this.name} triggered by {other.gameObject.name}");
        StartCrashSequence();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartCrashSequence()
    {
        //Disable player controls
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
}
