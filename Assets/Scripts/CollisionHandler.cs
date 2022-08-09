using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Delay time when the level restart")]
    [SerializeField] float levelLoadDelay = 1f;
    [Header("VFX")]
    [Tooltip("VFX for when an explosion occurs")]
    [SerializeField] ParticleSystem explosion;

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
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false; //Disable Box Collider when we crash
        GetComponent<PlayerControls>().enabled = false; //Disable player controls
        explosion.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }
}
