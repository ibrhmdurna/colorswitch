using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallMechanics : MonoBehaviour
{
    [SerializeField] Transform MainCamera;
    [SerializeField] Color BallColor;
    [SerializeField] Color Cyan, Yellow, Pink, Purple;
    [SerializeField] float JumpSpeed = 15f;
    [SerializeField] GameObject Circle, DoubleCircle, ColorSwitch;
    [SerializeField] Text ScoreText;

    public static int Score = 0;

    private int ColorIndex = -1;
    private GameController Game;
    private static string CurrentColor;
    private AudioSource BallAudio;
    private CircleController circleController;

    private void Awake()
    {
        
        BallColor = GetComponent<SpriteRenderer>().color;
        RandomColor();
    }

    // Start is called before the first frame update
    void Start()
    {
        BallAudio = GetComponent<AudioSource>();
        Game = FindObjectOfType<GameController>();
        Write();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > MainCamera.position.y)
        {
            FollowBall();
        }

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if(Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
                Jump();
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Circle" || collision.tag == "DoubleCircle")
        {
            circleController = collision.gameObject.GetComponentInChildren<CircleController>();
        }

        if (collision.tag == "ColorSwitch")
        {
            RandomColor();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Star")
        {
            Score++;
            Write();
            CreateLevel();
            circleController.PlayStarAudio();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Dead Layer")
        {
            Dead();
        }
        else if (collision.tag != CurrentColor && collision.tag != "Circle" && collision.tag != "DoubleCircle")
        {
            Dead();
        }
    }

    void CreateLevel()
    {
        int index = Random.Range(0, 2);

        switch (index)
        {
            case 0:
                Instantiate(Circle, new Vector3(0f, transform.position.y + 7f, 0f), Quaternion.identity);
                Instantiate(ColorSwitch, new Vector3(0f, transform.position.y + 10.5f, 0f), Quaternion.identity);
                break;
            case 1:
                Instantiate(DoubleCircle, new Vector3(0f, transform.position.y + 6f, 0f), Quaternion.identity);
                Instantiate(ColorSwitch, new Vector3(0f, transform.position.y + 9.5f, 0f), Quaternion.identity);
                break;
        }
    }

    private void Dead()
    {
        Score = 0;
        Write();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Write()
    {
        ScoreText.text = Score.ToString();
    }

    void FollowBall()
    {
        MainCamera.position = new Vector3(MainCamera.position.x, transform.position.y, MainCamera.position.z);
    }

    void RandomColor()
    {
        int index;

        if(ColorIndex != -1)
        {
            do
            {
                index = Random.Range(0, 4);
            } while (ColorIndex == index);
            ColorIndex = index;
        }
        else
        {
            index = Random.Range(0, 4);
            ColorIndex = index;
        }
        

        switch (index)
        {
            case 0:
                CurrentColor = "Cyan";
                BallColor = Cyan;
                break;
            case 1:
                CurrentColor = "Yellow";
                BallColor = Yellow;
                break;
            case 2:
                CurrentColor = "Pink";
                BallColor = Pink;
                break;
            case 3:
                CurrentColor = "Purple";
                BallColor = Purple;
                break;
        }

        GetComponent<SpriteRenderer>().color = BallColor;

        Debug.Log(CurrentColor);
        Debug.Log(index);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpSpeed;
        BallAudio.Play();
    }
}
