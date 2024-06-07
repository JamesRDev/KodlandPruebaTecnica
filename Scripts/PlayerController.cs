using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private Text HpText;


    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;
     public int victoryCount;
    [SerializeField] private int manyEnnemis;


    [SerializeField] float speed = 5f;
    CharacterController characterController;

    public float health = 0;

    void Start()
    {
        Application.targetFrameRate = 60;

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing!");
            enabled = false;
        }
        ChangeHealth(10);

        GameObject[] enemyTag = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject objEnemy in enemyTag)
        {
            Debug.Log("Objeto encontrado: " + objEnemy.name);
            manyEnnemis++;
        }

    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void counterWin()
    {
        victoryCount++;   
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveVector = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;
        characterController.Move(moveVector);


        if (victoryCount == manyEnnemis)
        {
            Win();
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(rifleStart.forward);
            buf.transform.rotation = transform.rotation;
        }
    }
}

