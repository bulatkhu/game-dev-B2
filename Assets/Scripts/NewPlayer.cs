using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    private Rigidbody2D rb;

    private SpriteRenderer sr;
    public Sprite[] sprites;
    private int spriteIndex;

    public static NewPlayer instance;
    
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tiltUp = 155f;    // Tilt angle for upward movements
    public float tiltDown = 155f; // Increased tilt angle for downward movements
    public bool isFuelDraining = false;
    public bool isCollided = false;
    
    [SerializeField] Transform spawnPosition;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float speed = 2f;
    [SerializeField] private Text restartText; 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (instance == null)
        {
            instance = this;
        }
    }
    
    // FixedUpdate is called at fixed timesteps instead of frames (default: 0.02s = 50fps)
    // private void FixedUpdate()
    // {
    //     // For continuous input you can use FixedUpdate
    //     if (Input.GetKey(KeyCode.A))
    //         rb.AddForce(speed * Vector2.left);
    //     if (Input.GetKey(KeyCode.D))
    //         rb.AddForce(speed * Vector2.right);
    // }
    
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (isFuelDraining && !AudioManager.instance.SFXSource.isPlaying && !isCollided)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.jump);
        }
        
        if (!FuelController.instance.HasFuel() || isCollided)
        {
            return;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y); // Set the constant horizontal velocity
    
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // rb.velocity = Vector2.up * strength;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.rotation = tiltUp; // Set the rotation angle to tiltUp when looking upwards
            isFuelDraining = true;
        }
        else
        {
            // rb.velocity += Vector2.up * gravity * Time.deltaTime;
            float angle = Mathf.Lerp(0f, tiltDown, rb.velocity.y / 3f);
            rb.rotation = angle - 10; // Rotate the bird downwards
            isFuelDraining = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.collision);
        }
        
        if (isCollided)
        {
            return;
        }
        
        GameOver();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        // Linear drag is kind of like air resistance and will make the object fall down slower
        // More drag means that the object will also need more force to be moved
        rb.drag = 5f;
        
        // To prevent a rigidbody from rotating, you can click on the checkbox in the Component or set this value
        // rb.freezeRotation = false;
        //
        // // This sets the rigidbody's physics material 2D
        // rb.sharedMaterial = bounceMaterial;
        //
        // // This sets the layer the GameObject is on. You can set which layers collide with each other in the Layer Collision Matrix
        // gameObject.layer = 3;
        // // To avoid using the integer, you can also use this instead. Of course the layer needs to be created first
        // gameObject.layer = LayerMask.NameToLayer("IgnoreObstacles");

        // On newer versions of Unity this sets the layers that should be ignored:
        // rb.excludeLayers = collisionLayers;
    }
    
    // This will be called every time Kirby hits a collider that has 'Is Trigger' checked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish") && !isCollided)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.goal);
            restartText.gameObject.SetActive(true);
        }
        
        if (collision.CompareTag("Obstacle"))
        {
            if (!isCollided)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.collision);
            }
            GameOver();
            return;
        }
        
        if (collision.gameObject.CompareTag("Fuel") && !isCollided)
        {
            FuelController.instance.FillFuel();
            AudioManager.instance.PlaySFX(AudioManager.instance.fuel);
            Destroy(collision.gameObject);
        }
        // A tag is used to differentiate between different objects. When 'Shredder' is hit, 
        // Kirby should be destroyed. Otherwise a copy of this Kirby should spawn
        // if (collision.CompareTag("Shredder"))
        //     Destroy(gameObject);
        // else
        //     Instantiate(gameObject, spawnPosition.position, Quaternion.identity);

        // Alternatively, you could put a script on different objects, like Shredder.cs or Spawner.cs
        // and let them handle instantiation and destruction, e.g. like this:
        // OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
    }

    private void AnimateSprite()
    {
        if (!FuelController.instance.HasFuel() || isCollided)
        {
            return;
        }
        
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            sr.sprite = sprites[spriteIndex];
        }
    }

    public void FuelRanOut()
    {
        rb.drag = 0;
    }

    public void GameOver()
    {
        
        if (isCollided)
        {
            return;
        }
        
        FuelRanOut();
        isCollided = true;
        rb.velocity = new Vector2(rb.velocity.x * 2, jumpForce * 4);
        
        restartText.gameObject.SetActive(true);
    }
}
