using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public float playerHealth = 300;
    float savedHealth;
    public bool playerDie = false;

    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Project tiles"))
        {
            
            playerHealth -= 10f;
            if (GameManager.instance.sceneIndex == 1)
                PlayerPrefs.SetFloat("savedHealth", playerHealth);
        }
    }

}
