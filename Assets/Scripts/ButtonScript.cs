using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject usedButton;
    public GameObject Button;
    public GameObject hiddenFloor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hiddenFloor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Button");
        Button.SetActive(false);
        usedButton.SetActive(true);
        hiddenFloor.SetActive(true);

    }
}
