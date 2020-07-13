using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _firstSceneName = "MainMenu";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(_firstSceneName);
    }
}
