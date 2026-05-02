using UnityEngine;

public class ColorInvert : MonoBehaviour
{
    [SerializeField]
    Material material;
    [SerializeField]
    Camera MainCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // I hate shaders so thats why this code is basicly writen by chat :D 

        // 1. Get center in viewport space
        Vector3 centerVP = MainCam.WorldToViewportPoint(transform.position);

        // 2. Get size in world space
        Vector3 scale = transform.lossyScale;

        // 3. Convert size to viewport size
        // We offset in world space and measure the difference
        Vector3 right = transform.right * scale.x * 0.5f;
        Vector3 up = transform.up * scale.y * 0.5f;

        Vector3 p1 = MainCam.WorldToViewportPoint(transform.position - right - up);
        Vector3 p2 = MainCam.WorldToViewportPoint(transform.position + right + up);

        Vector2 sizeVP = new Vector2(
            Mathf.Abs(p2.x - p1.x) * 0.5f,
            Mathf.Abs(p2.y - p1.y) * 0.5f
        );

        // 4. Send to shader
        material.SetVector("_Center", new Vector2(centerVP.x, centerVP.y));
        material.SetVector("_Size", sizeVP);
    }

    void OnDisable()
    {
        material.SetVector("_Center", Vector2.zero);
        material.SetVector("_Size", Vector2.zero);
    }
}
