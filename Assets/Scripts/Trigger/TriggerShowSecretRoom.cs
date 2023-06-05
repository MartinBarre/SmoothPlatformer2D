using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerShowSecretRoom : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;

    private void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        
        tilemapRenderer.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        
        tilemapRenderer.enabled = true;
    }
}
