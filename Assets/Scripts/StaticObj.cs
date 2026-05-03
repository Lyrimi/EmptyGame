using System.Collections;
using UnityEngine;

public class StaticObj : MonoBehaviour
{
    Color StaticColor;
    SpriteRenderer spriteRenderer;
    public delegate void Activate(Color color);
    public static Activate activate;

    ParticleSystem particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activate += Remove;
        particle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StaticColor = spriteRenderer.color;
        particle.Stop();
        var pmain = particle.main;
        pmain.startColor = StaticColor;
    }

    
    // Update is called once per frame
    void Update()
    {

    }

    void Remove(Color color)
    {
        if (color == StaticColor)
        {
            particle.Play();
            spriteRenderer.enabled = false;
            StartCoroutine(AnimatedDestroy());
        }
    }

    IEnumerator AnimatedDestroy()
    {
        while (particle.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
        Destroy(this);
    }
}
