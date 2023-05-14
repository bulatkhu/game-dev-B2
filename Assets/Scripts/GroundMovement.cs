using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public static GroundMovement Instance; // Singleton instance
    
    public float speed = 5f; // Adjust this value to control the speed of the ground movement
    public float endOffset = 1f; // Adjust this value to set an additional offset from the end position

    private float endPosition;

    private void Start()
    {
        // Calculate the end position based on the size of the ground
        CalculateEndPosition();
    }
    
    private void Awake()
    {
        // Set up singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopMovement()
    {
        enabled = false;
    }

    private void Update()
    {
        // Move the ground to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the ground has reached the end position
        if (transform.position.x <= endPosition)
        {
            // Stop the ground movement
            StopMovement();
        }
    }

    private void CalculateEndPosition()
    {
        // Get all child renderers
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        // Initialize the total length
        float totalLength = 0f;

        // Iterate through each child renderer and accumulate the length
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer.CompareTag("Ground"))  
            {
                totalLength += renderer.bounds.size.x;
                Debug.Log($"Xes: {renderer.bounds.size.x}");
            }
        }

        Debug.Log($"Total length: {totalLength}");

        // Calculate the end position based on the total length and the offset
        endPosition = transform.position.x - totalLength - endOffset;
    }
}