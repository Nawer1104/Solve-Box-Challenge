using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody2D rb;

    private bool canMove = false;

    public GameObject vfx;

    private Vector3 startPos;

    public Vector2 direction;

    private int Id;

    private void Awake()
    {
        startPos = transform.position;

        Id = GetInstanceID();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove) return;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            GameObject vfxExplosin = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(vfxExplosin, 0.75f);
            gameObject.SetActive(false);

            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            GameManager.Instance.CheckLevelUp();
        }
    }
}