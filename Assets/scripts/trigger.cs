using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField] GameObject player;

    private healthManager health;

    private void Start()
    {
        health = player.GetComponent<healthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            Destroy(other.gameObject);
            health.update_health(2);
        }
    }
}
