using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains_Manager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _animationEnded;
    public static Curtains_Manager Singletons;

    private void Awake()
    {
        if (Singletons == null) Singletons = this;
        else Destroy(this);
    }
    public IEnumerator OpenCurtainsRoutine() 
    {
        _animator.Play("OpenCurtains");
        yield return StartCoroutine(WaitForAnimationEnd());
    }

    public IEnumerator CloseCurtainsRoutine() 
    {
        _animator.Play("CloseCurtains");
        yield return StartCoroutine(WaitForAnimationEnd());
    }


    private IEnumerator WaitForAnimationEnd() 
    {
        yield return null;
        while (!_animationEnded) 
        {
            yield return null;
        }
        _animationEnded = false;
    }

    public void OnAnimationEnd() 
    {
        _animationEnded = true;
    }
}
