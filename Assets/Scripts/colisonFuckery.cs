using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class colisonFuckery : MonoBehaviour
{
    [SerializeField]
    int ColidersX;
    [SerializeField]
    int Colidersy;

    [SerializeField]
    BoxCollider2D otherCol;
[SerializeField]
    LayerMask layerMask;
    GameObject child;
    BoxCollider2D col;


    EdgeCollider2D[] EdgeColiders;
    Vector2[] PointOfsets;

    Vector2 postion
    {
        get
        {
            return new(transform.position.x, transform.position.y);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector2[] Points = new Vector2[4];
    void Start()
    {
        EdgeColiders = GetComponentsInChildren<EdgeCollider2D>();
        col = GetComponent<BoxCollider2D>();
        PointOfsets = new Vector2[4];
        PointOfsets[0] = new Vector2(col.size.x / 2, -col.size.y / 2) * transform.lossyScale;
        PointOfsets[1] = new Vector2(-col.size.x / 2, -col.size.y / 2) * transform.lossyScale;
        PointOfsets[2] = new Vector2(-col.size.x / 2, col.size.y / 2) * transform.lossyScale;
        PointOfsets[3] = new Vector2(col.size.x / 2, col.size.y / 2) * transform.lossyScale;
    }

    // Update is called once per frame


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

        edgeCollider.enabled = true;
        if (!otherCol.OverlapPoint(a) && !otherCol.OverlapPoint(b))
        {
            colPoints[0] = (a - postion) / transform.lossyScale;
            colPoints[1] = (b - postion) / transform.lossyScale;
            edgeCollider.points = colPoints;
            return;
        }
        if (otherCol.OverlapPoint(a) && otherCol.OverlapPoint(b))
        {
            edgeCollider.enabled = false;
            return;
        }

        if (otherCol.OverlapPoint(b))
        {
            RaycastHit2D hit = Physics2D.Raycast(a, dir, maxLength, layerMask);
            if (hit)
            {
                colPoints[0] = (a - postion) / transform.lossyScale;
                colPoints[1] = (hit.point - postion) / transform.lossyScale;
            }
            else { Debug.LogError("Something Went VERY WRONG"); }
        }

        if (otherCol.OverlapPoint(a))
        {
            RaycastHit2D hit = Physics2D.Raycast(b, -dir, maxLength, layerMask);
            if (hit)
            {
                colPoints[0] = (b - postion) / transform.lossyScale;
                colPoints[1] = (hit.point - postion) / transform.lossyScale;
            }
            else { Debug.LogError("Something Went VERY WRONG"); }
        }
        
        edgeCollider.points = colPoints;

       
        Debug.DrawLine(colPoints[0] + Vector2.right*2, colPoints[1] + Vector2.right*2, Color.azure);
    }

    
    void FixedUpdate()
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
}

