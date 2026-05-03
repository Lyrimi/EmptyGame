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

    static public DeathLine Instance;

    bool reseting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.left * 100, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * 100, Color.red);

        if (!Application.IsPlaying(gameObject)) return;
        if (Player.transform.position.y < transform.position.y)
        {
            Reset();
        }

    }

    public void Reset()
    {
        if (reseting) return;
        reseting = true;
        StartCoroutine(ResetPlayer());
        //Banish the player to the shadow realm
        Player.transform.position = new Vector2(0, -100);
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
