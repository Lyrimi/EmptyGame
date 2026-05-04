using UnityEngine;
using System.Collections;

public class StaticRemover : MonoBehaviour
{
    Color StaticColor;

    SpriteRenderer spriteRenderer;
    ParticleSystem particle;
    [SerializeField]
    bool IsQuietTriger = false;
    [SerializeField]
    Quiet quiet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("EmptyBox")) return;

        Vector2 size = collision.transform.lossyScale;
        Vector2 pos = collision.transform.position;

        Vector2[] conors = new Vector2[2];
        conors[0] = new Vector2(
            transform.position.x - transform.lossyScale.x / 2, transform.position.y + transform.lossyScale.y / 2
        );

        conors[1] = new Vector2(
            transform.position.x + transform.lossyScale.x / 2, transform.position.y - transform.lossyScale.y / 2
        );

        if (collision.OverlapPoint(conors[0]) && collision.OverlapPoint(conors[1]))
        {
            Activate();
        }

    }

    void Activate()
    {
        if (IsQuietTriger)
        {
            quiet.End();
            return;
        }
        StaticObj.activate(StaticColor);
        Remove();
    }

    void Remove()
    {

        GetComponent<BoxCollider2D>().enabled = false;
        particle.Play();
        spriteRenderer.enabled = false;
        StartCoroutine(AnimatedDestroy());
    }

    IEnumerator AnimatedDestroy()
    {
        while (particle.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
