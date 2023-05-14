using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    private bool isCollided;

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

        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * strength;
        }

        rb.velocity += Vector2.up * gravity * Time.deltaTime;

        float angle = Mathf.Lerp(0f, -tilt, rb.velocity.y / 3f);
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"GO: {collision.gameObject.name}");
        if (!isCollided)
        {
            isCollided = true;
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Player has collided with the ground component
            Debug.Log("Player collided with the ground!");
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
