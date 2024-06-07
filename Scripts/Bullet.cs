using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 0.1f;
    Vector3 direction;
    PlayerController playerController;
    [SerializeField] private int bulletLife;

    private void Start()
    {

        playerController = FindObjectOfType<PlayerController>();
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed += 1f;

        Collider[] targets = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var item in targets)
        {
            if (item.tag == "Enemy")
            {
                playerController.victoryCount++;
                Destroy(item.gameObject);
                StartCoroutine(DestroyBulllet());
            }
            if (item.tag != "Enemy") 
            {
                StartCoroutine(DestroyBulllet());
                Debug.Log("colisiones con otra cosa no enemigo");
            }
        }
    }


    IEnumerator DestroyBulllet() 
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }
}
