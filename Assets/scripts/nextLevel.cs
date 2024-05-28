using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    [SerializeField] string scene;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaa");
        SceneManager.LoadScene(scene);
    }
}
