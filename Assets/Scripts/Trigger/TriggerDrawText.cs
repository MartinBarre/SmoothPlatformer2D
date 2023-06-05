using TMPro;
using UnityEngine;

public class TriggerDrawText : MonoBehaviour
{
    public GameObject[] textObjects;

    private TMP_Text[] textComponents;

    private void Start()
    {
        textComponents = new TMP_Text[textObjects.Length];
        for (var i = 0; i < textObjects.Length; i++)
        {
            textComponents[i] = textObjects[i].GetComponent<TMP_Text>();
            textComponents[i].enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        
        foreach (var text in textComponents)
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        
        foreach (var text in textComponents)
        {
            text.enabled = false;
        }
    }
}
