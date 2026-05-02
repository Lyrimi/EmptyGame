using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera MainCamera;
    Rigidbody2D rb;
    public GameObject Player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Player.transform.position.x >= 17.5f/2)
        {
            MainCamera.transform.position = new Vector3 (17.5f,0,-10);
        }
        if (Player.transform.position.x <= 17.5f / 2 && Player.transform.position.x >= -17.5f / 2)
        {
            MainCamera.transform.position = new Vector3(0, 0, -10);
        }
        if (Player.transform.position.x <= -17.5f / 2)
        {
            MainCamera.transform.position = new Vector3(-17.5f, 0, -10);
        }
    }
}
