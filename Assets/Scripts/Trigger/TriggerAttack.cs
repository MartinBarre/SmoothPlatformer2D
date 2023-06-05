using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    public float power;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        collision.gameObject.GetComponent<PlayerMovement>().KnockBack(transform, power);
    }

}
