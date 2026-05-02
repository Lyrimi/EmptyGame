using UnityEngine;

[ExecuteInEditMode]
public class DeathLine : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
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
        if (Player.transform.position.y < transform.position.y)
        {

        }

    }
    
    void ResetPlayer()
    {
        
    }
}
