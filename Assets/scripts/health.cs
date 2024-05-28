using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class healthManager : MonoBehaviour
{
    [SerializeField] float base_health = 10f;
    [SerializeField] float velocity_resistance = -10f;
    [SerializeField] CharacterController controller;
    [SerializeField] UnityEngine.UI.Slider healthbar;
    [SerializeField] public GameObject canva;

    private float health = 0;
    private float last_velocity = 0;

    private void Start()
    {
        health = base_health;
        healthbar.value = base_health;
        canva.SetActive(false);
    }

    private void Update()
    {
        isTakingFallDamage();
    }

    public void update_health(float heal)
    {
        health += heal;
        if (health > base_health)
        {
            health = base_health;
        }
        else if (health <= 0) 
        {
            Die();
        }
        healthbar.value = health;
    }

    private void isTakingFallDamage()
    {
        float velo = controller.velocity.y;
        if (velo > last_velocity && velo < 0)
        {
            if (velo < velocity_resistance)
            {
                update_health(velo/10);
            }
        }
        last_velocity = velo;
    }

    private void Die()
    {
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        canva.SetActive(true);
    }
}
