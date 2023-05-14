using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tiltUp = 155f;    // Tilt angle for upward movements
    public float tiltDown = 155f; // Increased tilt angle for downward movements
    public bool isCollided;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        rb.velocity = Vector2.zero;
        isCollided = false;
    }

    private void Update()
    {
        if (isCollided)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            rb.velocity = Vector2.up * strength;
            rb.rotation = tiltUp; // Set the rotation angle to tiltUp when looking upwards
        }
        else
        {
            rb.velocity += Vector2.up * gravity * Time.deltaTime;
            float angle = Mathf.Lerp(0f, tiltDown, rb.velocity.y / 3f);
            rb.rotation = angle - 10; // Rotate the bird downwards
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"GO: {collision.gameObject.name}");
        if (!isCollided)
        {
            isCollided = true;
        }
        
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Player has collided with the ground component
            Debug.Log("Player collided with the ground!");
            GroundMovement.Instance.StopMovement();
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }
}
