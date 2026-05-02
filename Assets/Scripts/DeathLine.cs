using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class DeathLine : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    Transform ResetPos;

    [SerializeField]
    GameObject Camera;

    [SerializeField]
    Animator anim;

    bool reseting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.left * 100, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * 100, Color.red);

        if (!Application.IsPlaying(gameObject)) return;
        if (reseting) return;
        if (Player.transform.position.y < transform.position.y)
        {
            reseting = true;
            StartCoroutine(ResetPlayer());
        }

    }
    
    IEnumerator ResetPlayer()
    {
        PlayerControler controler = Player.GetComponent<PlayerControler>();
        BoxCollider2D box = Player.GetComponent<BoxCollider2D>();
        EdgeCollider2D[] Edges = Player.GetComponentsInChildren<EdgeCollider2D>();
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        colisonFuckery col = Player.GetComponent<colisonFuckery>();

        void setEnabled(bool value)
        {
            col.enabled = value;
            controler.enabled = value;
            box.enabled = value;
            foreach (var Edge in Edges)
            {
                Edge.enabled = value;
            }
        }

        setEnabled(false);

        anim.SetTrigger("Reset");
        yield return new WaitForSeconds(1.2f);
        rb.linearVelocity = new Vector2(0, -5);

        Player.transform.position = new Vector3(ResetPos.position.x,ResetPos.position.y,Player.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("End");
        //Check if player is inside GameArea

        while (Player.transform.position.y > Camera.transform.position.y)
        {
            yield return null;
        }
        setEnabled(true);
        reseting = false;
    }
}
