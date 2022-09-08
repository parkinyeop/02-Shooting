using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    bool isShow = false;
    // Start is called before the first frame update
    private void Awake()
    {
        canvasGroup= GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.onLifeChange += OnGameOver;

    }

    // Update is called once per frame
    void Update()
    {
     if(isShow)
        {
            canvasGroup.alpha += Time.deltaTime;
        }
    }

    void OnGameOver(int lifeCount)
    {
        if(lifeCount <= 0)
        {
            StartCoroutine(GameOverDelay());
        }
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1.0f);
        isShow = true;
    }
}
