using System;
using System.Collections;
using UnityEngine;

public class Quiet : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject emptySpace;
    [SerializeField]
    PlayerControler playerControler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void End()
    {
        emptySpace.SetActive(false);
        playerControler.enabled = false;
        anim.SetTrigger("End");
    }
}
