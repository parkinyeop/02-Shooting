using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifePanel : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI lifeText;
    private void Awake()
    {
        lifeText= transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //GameObject.Find();
        //GameObject.FindGameObjectsWithTag();
        //GameObject.FindObjectOfType<>();
        Player player = FindObjectOfType<Player>();
        player.onLifeChnage += Refreah;
    }

    void Refreah(int life)
    {
        lifeText.text = life.ToString();
    }
}
