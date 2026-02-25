using UnityEngine;

public class PickaxeMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D playerRB;
    private bool thrown = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !thrown)
        {
            float range = rb.position.x - 3;
            do
            {
                Throw(range);
            } while (thrown);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    private void Throw(float range)
    {
        thrown = true;
        float xpos = rb.position.x;
        if (xpos > range)
        {
            horizontal = -1f;
        } else
        {
            Retrieve();
        }
    }

    private void Retrieve()
    {
        while (rb.position.x != playerRB.position.x - 0.9 && rb.position.y != playerRB.position.y) {
            if (playerRB.position.x < rb.position.x)
            {
                horizontal = -1f;
            }
            else
            {
                horizontal = 1f;
            }
            if (playerRB.position.y < rb.position.y)
            {
                vertical = -1f;
            }
            else
            {
                vertical = 1f;
            }
        }
        horizontal = 0f;
        vertical = 0f;
        thrown = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && thrown)
        {
            
        }
    }
}
