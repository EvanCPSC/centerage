using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float lastHorizontal;
    private float lastVertical;
    private bool isHit;
    private float invulFrames;
    public float throwDelay;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject pickaxePrefab;
    [SerializeField] private GameObject pearlPrefab;
    [SerializeField] private Transform firePoint;
    GameObject pickaxe = null;
    GameObject pearlProjectile = null;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public AudioManager audioManager;

    void Start()
    {
        isHit = false;
        anim = GetComponent<Animator>();
        lastHorizontal = 0;
        lastVertical = 0;
        invulFrames = 0f;
        throwDelay = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // change animation direction
        if (!GameManager.Instance.isPaused && !GameManager.Instance.isDebug) {
            if (horizontal != 0 || vertical != 0)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("InputX", horizontal);
                anim.SetFloat("InputY", vertical);
                lastHorizontal = horizontal;
                lastVertical = vertical;
            } else
            {
                anim.SetBool("isWalking", false);
                anim.SetFloat("LastDirX", lastHorizontal);
                anim.SetFloat("LastDirY", lastVertical);
            }

            if (isHit) // invul frames
            {
                spriteRenderer.color = new Color(1f, invulFrames, invulFrames, 1f);
                invulFrames += 0.05f;
                if (invulFrames >= 1f)
                {
                    invulFrames = 0f;
                    isHit = false;
                }
            }
        }

        // die
        if (PlayerStats.playerHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        // inputs
        if (Input.GetButtonDown("Cancel") && !GameManager.Instance.isDebug)
        {
            GameManager.Instance.PauseGame();
        }

        if (Input.GetButtonDown("Debug") && !GameManager.Instance.isPaused)
        {
            GameManager.Instance.DebugGame();
        }

        if (throwDelay <= 0) {
            if(Input.GetButtonDown("FireL") && pickaxe == null)
            {
                ThrowPickaxe('L');
                if (PlayerStats.pearl) ThrowPearl('L');
                throwDelay = 1f;
            }
            if(Input.GetButtonDown("FireR") && pickaxe == null)
            {
                ThrowPickaxe('R');
                if (PlayerStats.pearl) ThrowPearl('R');
                throwDelay = 1f;
            }
            if(Input.GetButtonDown("FireU") && pickaxe == null)
            {
                ThrowPickaxe('U');
                if (PlayerStats.pearl) ThrowPearl('U');
                throwDelay = 1f;
            }
            if(Input.GetButtonDown("FireD") && pickaxe == null)
            {
                ThrowPickaxe('D');
                if (PlayerStats.pearl) ThrowPearl('D');
                throwDelay = 1f;
            }
        } else
        {
            throwDelay -= 0.1f;
        }

        // dioptase trigger
        if (PlayerStats.dioptase && !PlayerStats.dioptaseUsed)
        {
            if (Input.GetButtonDown("Jump") && pickaxe != null)
            {
                rb.position = pickaxe.GetComponent<Rigidbody2D>().position;
                PlayerStats.dioptaseUsed = true; // once per room, resets when door touched
                Destroy(pickaxe);
                if (pearlProjectile != null) Destroy(pearlProjectile);
            }
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * PlayerStats.playerSpeed, vertical * PlayerStats.playerSpeed);
    }

    private void ThrowPickaxe(char dirc)
    {
        // From Pickaxe Projectile
        pickaxe = Instantiate(pickaxePrefab, firePoint.position, firePoint.rotation);
        PickaxeProjectile pickaxeScript = pickaxe.GetComponent<PickaxeProjectile>();
        if (pickaxeScript != null)
        {
            pickaxeScript.dir = dirc;
        }
    }
    
    private void ThrowPearl(char dirc)
    {
        pearlProjectile = Instantiate(pearlPrefab, firePoint.position, firePoint.rotation);
        ProjectilePearl pearlScript = pearlProjectile.GetComponent<ProjectilePearl>();
        if (pearlScript != null)
        {
            pearlScript.dir = dirc;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("DoorL"))
        {
            if (pickaxe != null) Destroy(pickaxe);
            if (pearlProjectile != null) Destroy(pearlProjectile);
            PlayerStats.dioptaseUsed = false;
            rb.position = new Vector2(rb.position.x - 4.2f, rb.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 18f, Camera.main.transform.position.y, -10f);
        }
        if (trigger.gameObject.CompareTag("DoorR"))
        {
            if (pickaxe != null) Destroy(pickaxe);
            if (pearlProjectile != null) Destroy(pearlProjectile);
            PlayerStats.dioptaseUsed = false;
            rb.position = new Vector2(rb.position.x + 4.2f, rb.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 18f, Camera.main.transform.position.y, -10f);
        }
        if (trigger.gameObject.CompareTag("DoorT"))
        {
            if (pickaxe != null) Destroy(pickaxe);
            if (pearlProjectile != null) Destroy(pearlProjectile);
            PlayerStats.dioptaseUsed = false;
            rb.position = new Vector2(rb.position.x, rb.position.y + 3.6f);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 10f, -10f);
        }
        if (trigger.gameObject.CompareTag("DoorB"))
        {
            if (pickaxe != null) Destroy(pickaxe);
            if (pearlProjectile != null) Destroy(pearlProjectile);
            PlayerStats.dioptaseUsed = false;
            rb.position = new Vector2(rb.position.x, rb.position.y - 3.6f);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y - 10f, -10f);
        }
        if (trigger.gameObject.CompareTag("Exit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Projectile")) && !isHit)
        {
            PlayerStats.playerHealth -= 1f;
            audioManager.PlaySFX(audioManager.sfxPlayerHit);
            isHit = true;
            if (PlayerStats.labradorite)
            {
                collision.gameObject.GetComponent<EnemyManager>().HitEnemy(PlayerStats.pickaxeDamage / 10f);
            }
        }
    }
}
