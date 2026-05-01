using UnityEngine;

public class colisonFuckery : MonoBehaviour
{
    [SerializeField]
    int ColidersX;
    [SerializeField]
    int Colidersy;
    GameObject child;
    BoxCollider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [ExecuteInEditMode]
    void Awake()
    {
        print("Ran");
        col = GetComponent<BoxCollider2D>();
        Vector2 size = new(col.size.x/ColidersX,col.size.y/Colidersy);
        child = transform.GetChild(0).gameObject;
        for (int x = 0; x < ColidersX; x++)
        {
            for (int y = 0; y < Colidersy; y++)
            {
                BoxCollider2D childcol = child.AddComponent<BoxCollider2D>();
                childcol.size = size;
                Vector2 ofset = new(childcol.size.x * x, childcol.size.y * y);
                childcol.offset = new(ofset.x - col.size.x / 2 + childcol.size.x / 2, ofset.y - col.size.y / 2 + childcol.size.y / 2);
            }
        }
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
