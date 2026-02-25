using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed;
    private int health;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject pickaxePrefab;
    [SerializeField] private Transform firePoint;
    GameObject pickaxe = null;

    void Start()
    {
        speed = PlayerStats.playerSpeed;
        health = PlayerStats.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if(Input.GetButtonDown("FireL") && pickaxe == null)
        {
            // From Pickaxe Projectile
            pickaxe = Instantiate(pickaxePrefab, firePoint.position, firePoint.rotation);
            PickaxeProjectile pickaxeScript = pickaxe.GetComponent<PickaxeProjectile>();
            if (pickaxeScript != null)
            {
                pickaxeScript.dir = 'L';
            }
        }
        if(Input.GetButtonDown("FireR") && pickaxe == null)
        {
            // From Pickaxe Projectile
            pickaxe = Instantiate(pickaxePrefab, firePoint.position, firePoint.rotation);
            PickaxeProjectile pickaxeScript = pickaxe.GetComponent<PickaxeProjectile>();
            if (pickaxeScript != null)
            {
                pickaxeScript.dir = 'R';
            }
        }
        if(Input.GetButtonDown("FireU") && pickaxe == null)
        {
            // From Pickaxe Projectile
            pickaxe = Instantiate(pickaxePrefab, firePoint.position, firePoint.rotation);
            PickaxeProjectile pickaxeScript = pickaxe.GetComponent<PickaxeProjectile>();
            if (pickaxeScript != null)
            {
                pickaxeScript.dir = 'U';
            }
        }
        if(Input.GetButtonDown("FireD") && pickaxe == null)
        {
            // From Pickaxe Projectile
            pickaxe = Instantiate(pickaxePrefab, firePoint.position, firePoint.rotation);
            PickaxeProjectile pickaxeScript = pickaxe.GetComponent<PickaxeProjectile>();
            if (pickaxeScript != null)
            {
                pickaxeScript.dir = 'D';
            }
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
        }
    }
}
