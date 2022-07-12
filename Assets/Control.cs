using UnityEngine;

public class Control : MonoBehaviour
{
    public float Speed = 2;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) rb.velocity += Vector2.left * Speed;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) rb.velocity += Vector2.right * Speed;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) rb.velocity += Vector2.up * Speed;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) rb.velocity += Vector2.down * Speed;
    }
}