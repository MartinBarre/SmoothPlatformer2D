using UnityEngine;

public class TakeFeatherGold : MonoBehaviour
{

    private bool activate = true;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!activate || !collider.CompareTag("Player")) return;
        
        activate = false;
        gameManager.nbFeatherGold++;
        Destroy(gameObject);
    }

}
