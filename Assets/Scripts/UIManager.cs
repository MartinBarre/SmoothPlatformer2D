using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject panelFeatherSilver;
    public GameObject featherSilverImage;
    public GameObject featherGoldImage;
    public TMP_Text textScore;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        foreach (Transform child in panelFeatherSilver.transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < gameManager.nbFeatherSilver; i++)
        {
            var newImage = Instantiate(featherSilverImage, Vector3.zero, Quaternion.identity);
            newImage.transform.SetParent(panelFeatherSilver.transform);
        }

        for (var i = 0; i < gameManager.nbFeatherGold; i++)
        {
            var newImage = Instantiate(featherGoldImage, Vector3.zero, Quaternion.identity);
            newImage.transform.SetParent(panelFeatherSilver.transform);
        }

        textScore.text = gameManager.nbStrawberry + "";
    }

}
