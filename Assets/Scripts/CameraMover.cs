using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float MoveInterval = 17.5f;

    [SerializeField]
    float MoveTime;
    Coroutine curCoroutine;
    Vector3 TargetPos;
    private void Start()
    {
        TargetPos = transform.position;
    }

    private void Update()
    {
        Vector3 difPos = TargetPos-Player.transform.position;

        if (math.abs(difPos.x) >= MoveInterval/2)
        {
            Vector2 movDir = new Vector2((Player.transform.position - transform.position).x, 0).normalized;
            InitMovment(movDir);
        }
    }
    
    void InitMovment(Vector2 movDir)
    {
        
        Vector2 temp = movDir * MoveInterval;
        TargetPos += new Vector3(temp.x, temp.y, 0);
        print(temp);
        if (curCoroutine != null)
        {
           StopCoroutine(curCoroutine); 
        }
        curCoroutine = StartCoroutine(StartMovment());
    }

    IEnumerator StartMovment()
    {
        Vector3 StartPos = transform.position;

        float time = 0;

        while (time < MoveTime)
        {
            transform.position = Vector3.Lerp(StartPos, TargetPos, time);
            time += Time.deltaTime;
            //Waits for next frame
            yield return null;
        }
        transform.position = TargetPos;        
    }
}
