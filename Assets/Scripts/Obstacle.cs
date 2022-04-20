using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float _speed, _posY, _posX;

    [SerializeField]
    private Sprite _yellow, _black, _red;

    private int type;
    private int score;

    private void Start()
    {
        type = Random.Range(1, 11);
        type = type < 8 ? 0 : type < 10 ? 1 : 2;
        GetComponent<SpriteRenderer>().sprite = type == 0 ? _yellow : type == 1 ? _black : _red;
        score = type == 0 ? 1 : type == 1 ? 0 : 10;
        Vector3 pos = transform.position;
        pos.x = Random.Range(-_posX, _posX);
        pos.y = _posY;
        transform.position = pos;
    }

    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(type == 0 ||  type == 2)
            {
                GameManager.instance.UpdateScore(score);
            }
            else
            {
                GameManager.instance.GameOver();
            }
            Destroy(gameObject);
        }

        if(collision.CompareTag("Finish"))
        {
            if(type == 0)
            {
                GameManager.instance.GameOver();
            }
            Destroy(gameObject);
        }
    }
}
