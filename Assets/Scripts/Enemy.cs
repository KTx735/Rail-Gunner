using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnParticleCollision(GameObject other) 
    {
        Debug.Log($"{this.name} Hit by {other.gameObject.name}");
        Destroy(gameObject);
    }
}
