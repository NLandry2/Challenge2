using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpforce;

    public GameObject player;
    public GameObject winTextObject;
    public Text score;
    public TextMeshProUGUI livesText;
    public GameObject loseTextObject;

    Animator anim;

    private int lives = 3;
    private int level = 1;
    private int scoreValue = 0;

    void Start()
    {
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        WinScore();
        
        SetLivesText();

        anim = GetComponent<Animator>();

        scoreValue = 0;
        lives = 3;
        level = 1;
      

        if (Input.GetKeyDown(KeyCode.Escape))
          {
             Application.Quit();
          } 
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
    }
    void update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
          {
             Application.Quit();
          } 
    }

    void WinScore()
    {
        if(scoreValue == 8)
        {
            winTextObject.SetActive(true);
            player.SetActive(false);
        }
    }

    void Teleport()
    {
        
        {
            transform.position = new Vector2(115, 3);

            level = level + 1;
        }
    } 
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if(lives == 0)
        {
            loseTextObject.SetActive(true);
            player.SetActive(false);
            
        }
    }
    

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0){
            characterScale.x = -1;
        }
        if (Input.GetAxis("Horizontal") > 0){
            characterScale.x = 1;
        }
        transform.localScale = characterScale;

        anim.SetBool(name:"IsInAir",value:vertMovement !=0);
    
        anim.SetBool(name:"IsWalking",value:hozMovement !=0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
            WinScore();
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesText.text = "Lives:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
            SetLivesText();
        }
        else if (scoreValue == 4 && level == 1)
        {
            Teleport();
            lives = 3;
            SetLivesText();
        }
        

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W)) 
            {
                rd2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse); 
            }
        }
    }
    
}