using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed = 10f;
    [SerializeField] float m_maxDistance = 15f;
    [SerializeField] AudioClip hitSound; // 衝突時の音
    private AudioSource audioSource;

    Rigidbody2D m_rb;
    Transform playerTransform;
    SpriteRenderer playerSprite;
    Vector3 startPosition;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
            playerSprite = player.GetComponent<SpriteRenderer>();
        }

        m_rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();  // AudioSourceの取得

        Vector2 direction;
        if (playerSprite.flipX)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }

        startPosition = transform.position;
        Vector3 velocity = direction.normalized * m_bulletSpeed;
        m_rb.velocity = velocity;
    }

    void Update()
    {
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled > m_maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // 弾が衝突したときの処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);  // 効果音を再生
        }
        Destroy(gameObject);  // 衝突後に弾を破壊
    }
}
