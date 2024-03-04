using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            LoadLevel(sceneNumber);
    }
    void LoadLevel(int value)
    {
        SceneManager.LoadScene(value);
    }
}
