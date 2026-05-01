using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WallColider : MonoBehaviour
{

    Vector2[] PointOfsets;

    [SerializeField]
    LayerMask layerMask;

    EdgeCollider2D[] EdgeColiders;

    BoxCollider2D box;

    [SerializeField]
    LayerMask Exclude;


    Vector2 postion
    {
        get
        {
            return new(transform.position.x, transform.position.y);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EdgeColiders = new EdgeCollider2D[4];
        for (int i = 0; i < EdgeColiders.Length; i++)
        {
            EdgeColiders[i] = gameObject.AddComponent<EdgeCollider2D>();
            EdgeColiders[i].excludeLayers = Exclude;
        }
        box = GetComponent<BoxCollider2D>();
        PointOfsets = new Vector2[4];
        PointOfsets[0] = new Vector2(box.size.x/2, -box.size.y/2) * transform.lossyScale;
        PointOfsets[1] = new Vector2(-box.size.x / 2, -box.size.y / 2) * transform.lossyScale;
        PointOfsets[2] = new Vector2(-box.size.x/2, box.size.y/2) * transform.lossyScale;
        PointOfsets[3] = new Vector2(box.size.x/2, box.size.y/2) * transform.lossyScale;
    }

    // Update is called once per frame
    Vector2[] Points = new Vector2[4];
    void Update()
    {
        
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = new Vector2(transform.position.x, transform.position.y) + PointOfsets[i];
        }

        CaculateEdgeColider(Points[0], Points[1], 0);
        CaculateEdgeColider(Points[1], Points[2], 1);
        CaculateEdgeColider(Points[2], Points[3], 2);
        CaculateEdgeColider(Points[3], Points[0], 3);


    }
    

    
    void CaculateEdgeColider(Vector2 a, Vector2 b, int index)
    {
        if (index > EdgeColiders.Length-1)
        {
            Debug.LogError("Missing EdgeColiders");
            return;
        }
        EdgeCollider2D edgeCollider = EdgeColiders[index];
        Vector2[] colPoints = new Vector2[2];

        Vector2 dir = (b - a).normalized;
        float maxLength = Vector2.Distance(a, b);

        RaycastHit2D hit = Physics2D.Raycast(a, dir, maxLength, layerMask);


        if (hit)
        {
            colPoints[0] = (hit.point-postion)/transform.lossyScale ;
        }
        else
        {
            edgeCollider.enabled = false;
            return;
        }

        RaycastHit2D hit2 = Physics2D.Raycast(b, -dir, maxLength, layerMask);

        if (hit2)
        {
            colPoints[1] = (hit2.point-postion)/transform.lossyScale;
        }
        else
        {
            edgeCollider.enabled = false;
            return;
        }
        edgeCollider.enabled = true;
        edgeCollider.points = colPoints;
        Debug.DrawLine(colPoints[0], colPoints[1], Color.azure);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.coral;
        foreach (var item in Points)
        {
            Gizmos.DrawSphere(item, 0.1f);
        }
        
    }

}
