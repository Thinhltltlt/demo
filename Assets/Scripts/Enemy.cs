using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Enemy OnTriggerEnter {other.gameObject.name}");
        //if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
            Destroy(gameObject);
    }
}
