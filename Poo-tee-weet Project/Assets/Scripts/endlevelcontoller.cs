using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class endlevelcontoller : MonoBehaviour
{
    public GameObject wordAnim;
    private Animator _animator;
    

    private void Start()
    {
        _animator = wordAnim.GetComponent<Animator>();
    }

    public void startEndingLevel()
    {
        _animator.enabled = true;
    }
}
