using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text scoreText;
    public Transform TeleportGoal;

    private Rigidbody rb;
    private int count;
    private int score;
    private Transform PlayerTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        SetScoreText ();
        SetCountText ();
        winText.text = "";
        PlayerTransform = GameObject.Find("Player").transform;
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        if (Input.GetKey("escape"))
            Application.Quit ();

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            SetCountText ();
            SetScoreText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score - 1;
            SetCountText();
            SetScoreText();
        }
        if (score == 12)
        {
            PlayerTransform.position = TeleportGoal.position;
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetScoreText ()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score == 20)
        {
            winText.text = "You Win!";
        }
    }
}
