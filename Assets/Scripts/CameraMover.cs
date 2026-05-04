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
    PlayerControler controller;

    [SerializeField]
    float HorisontalMoveInterval = 17.5f;

    [SerializeField]

    float VerticalMoveInterval = 10f;

    [SerializeField]
    float VerticalMoveOfset = -1f;
    [SerializeField]
    float MoveTime;

    [SerializeField]
    GameObject Particles;
    [SerializeField]
    GameObject ParticlesOther;

    bool hasParticles
    {
        get
        {
            if (!Particles.activeSelf && !ParticlesOther.activeSelf)
            {
                return false;
            }
            return ParticlesOther && Particles;
        }
    }
    Vector3 TargetPos;
    private void Start()
    {
        controller = Player.GetComponent<PlayerControler>();
        TargetPos = transform.position;
        if (hasParticles)
        {
            Particles.SetActive(true);
            ParticlesOther.SetActive(false);
        }
    }

    private void Update()
    {
        Vector3 difPos = TargetPos - Player.transform.position;

        if (math.abs(difPos.x) >= HorisontalMoveInterval / 2)
        {
            Vector2 movDir = new Vector2((Player.transform.position - TargetPos).x, 0).normalized;
            InitMovment(movDir);
        }
        if (math.abs(difPos.y + VerticalMoveOfset) >= VerticalMoveInterval / 2)
        {
            Vector2 movDir = new Vector2(0, (Player.transform.position - TargetPos).y).normalized;
            InitMovment(movDir);
        }
    }

    void InitMovment(Vector2 movDir)
    {
        if (controller.enabled == false)
        {
            return;
        }
        Vector2 temp = Vector2.zero;
        if (movDir.y == 0)
        {
            temp = movDir * HorisontalMoveInterval;
        }
        if (movDir.x == 0)
        {
            temp = movDir * VerticalMoveInterval;
        }

        TargetPos += new Vector3(temp.x, temp.y, 0);

        if (TargetPos.y < 0)
        {
            return;
        }



        print(TargetPos);
        StartCoroutine(StartMovment());
        if (hasParticles) ParticlesStart();
    }

    IEnumerator StartMovment()
    {
        Vector3 localTarget = TargetPos;
        Vector3 StartPos = transform.position;

        float time = 0;

        while (time < MoveTime)
        {
            if (localTarget != TargetPos)
            {
                yield break;
            }
            transform.position = Vector3.Lerp(StartPos, localTarget, time);
            time += Time.deltaTime;
            //Waits for next frame
            yield return null;
        }
        transform.position = localTarget;
        if (hasParticles) ParticlesEnd();


    }

    void ParticlesStart()
    {
        (Particles, ParticlesOther) = (ParticlesOther, Particles);
        Particles.SetActive(true);
        Particles.transform.position = new Vector3(TargetPos.x, TargetPos.y, Particles.transform.position.z);
    }

    void ParticlesEnd()
    {
        ParticlesOther.SetActive(false);
    }
}
