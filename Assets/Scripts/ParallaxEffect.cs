using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public bool repeat;
    public float parallaxX = 1.0f;
    public float parallaxY = 1.0f;
    public GameObject background;
    public GameObject cam;

    private Vector2 spriteSize;
    private Vector3 startPosition;

    private void Start()
    {
        if (repeat)
        {
            spriteSize = background.GetComponent<SpriteRenderer>().size;
        }
        startPosition = background.transform.position;
    }

    private void Update()
    {
        var dist = new Vector3(cam.transform.position.x * parallaxX, cam.transform.position.y * parallaxY, 0);
        background.transform.position = startPosition + dist;

        if (!repeat) return;
        
        while (background.transform.position.x < cam.transform.position.x - spriteSize.x) background.transform.position += new Vector3(spriteSize.x, 0, 0);
        while (background.transform.position.x > cam.transform.position.x + spriteSize.x) background.transform.position -= new Vector3(spriteSize.x, 0, 0);
    }
}
