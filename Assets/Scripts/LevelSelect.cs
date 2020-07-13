using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private List<string> _levelNames = new List<string>();
    [SerializeField] private GameObject _levelPrefab = null;
    [SerializeField] private SceneSwapper _sceneSwapper = null;

    private void Awake()
    {
        BuildLevelSelect();
    }

    void BuildLevelSelect()
    {
        for (int i = 0; i < _levelNames.Count; i++)
        {
            var levelGO = Instantiate(_levelPrefab, transform);
            levelGO.GetComponentInChildren<Text>().text = (i + 1).ToString();

            string levelName = _levelNames[i];
            levelGO.GetComponent<Button>().onClick.AddListener(() => _sceneSwapper.StartLoadScene(levelName));
        }
    }
}
