using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeToggleButton : MonoBehaviour
{
    [SerializeField] private LevelController _levelController = null;
    [SerializeField] private Text _text = null;

    public void ToggleMode()
    {
        _levelController.Playing = !_levelController.Playing;

        _text.text = _levelController.Playing ? "stop" : "play";
    }
}
