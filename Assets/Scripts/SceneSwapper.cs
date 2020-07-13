using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwapper : MonoBehaviour
{
    [SerializeField] private Image _transitionImage = null;
    [SerializeField] private float _transitionTime = 1f;
    [SerializeField] private AnimationCurve _transitionCurve = null;

    private void Awake()
    {
        StartCoroutine(InTransition(null));
    }

    public void StartLoadScene(string sceneName)
    {
        StartCoroutine(OutTransition(() => SceneManager.LoadScene(sceneName)));
    }

    [ContextMenu("In Transition")]
    public void InTransitionTest()
    {
        StartCoroutine(InTransition(null));
    }

    [ContextMenu("Out Transition")]
    public void OutTransitionTest()
    {
        StartCoroutine(OutTransition(null));
    }

    IEnumerator InTransition(Action callback)
    {
        _transitionImage.enabled = true;

        for (float t = 0; t < 1; t += Time.deltaTime / _transitionTime)
        {
            _transitionImage.material.SetFloat("_Threshold", _transitionCurve.Evaluate(t));
            yield return null;
        }

        _transitionImage.material.SetFloat("_Threshold", 1);

        _transitionImage.enabled = false;

        callback?.Invoke();
    }

    IEnumerator OutTransition(Action callback)
    {
        _transitionImage.enabled = true;

        for (float t = 0; t < 1; t += Time.deltaTime / _transitionTime)
        {
            _transitionImage.material.SetFloat("_Threshold", 1 - _transitionCurve.Evaluate(t));
            yield return null;
        }

        _transitionImage.material.SetFloat("_Threshold", 0);

        callback?.Invoke();
    }
}
