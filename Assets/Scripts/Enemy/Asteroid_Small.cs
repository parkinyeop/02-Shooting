using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Small : MonoBehaviour
{

    public float speed = 3.0f;
    public int score = 10;

    Action<int> onDead;

    private void Awake()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        int rand = UnityEngine.Random.Range(0, 4);
        sprite.flipX = ((rand & 0b_01) != 0);
        sprite.flipY = ((rand & 0b_10) != 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;
    }

    // Update is called once per frame
    void Update()
    {                
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            onDead.Invoke(score);
            Destroy(this.gameObject);
        }
    }
}
