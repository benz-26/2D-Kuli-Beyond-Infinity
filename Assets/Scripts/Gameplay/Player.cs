using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraGameplay;
    public GameManager gameManager;
    public int score = 0;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private float throwInterval;
    [SerializeField] private float destroyRockTimer;
    private float throwTimer = 0f;

    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private float moveSpeed;
    private float currentVelocity = 0f;

    private Rigidbody2D rb;
    [SerializeField] private float playerFirstYPos;
    public bool isAlive;

    private void Awake()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
/*        playerFirstYPos = transform.position.y;*/
    }

    private void Update()
    {
        if (isAlive)
        {
            Movement();
            FlipCharacter();
            SetScore();

            throwTimer += Time.deltaTime;

            if (throwTimer >= throwInterval)
            {
                ThrowRock();
                throwTimer = 0f;
            }

        }

    }

    private void ThrowRock()
    {
        PlaySound(throwSound);
        GameObject rock = Instantiate(rockPrefab, spawnPoint.position, Quaternion.identity);
        Vector2 throwDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        rock.GetComponent<Rigidbody2D>().velocity = throwDirection * 8f;
        Destroy(rock, destroyRockTimer);
    }


    private void Movement()
    {
        int horizontalInput = GetHorizontalInput();

        // Calculate the target velocity based on input
        float targetVelocity = horizontalInput * moveSpeed;

        // Apply gradual acceleration and deceleration
        float smoothTime = 0.1f; // Adjust this value to control the smoothness
        currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref currentVelocity, smoothTime);

        // Apply the calculated velocity to the player's Rigidbody
        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
    }

    private int GetHorizontalInput()    
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
            return Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
#endif
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position.x < Screen.width / 2f ? -1 : 1;
        }
        return 0;
    }

    private void FlipCharacter()
    {
        if (GetHorizontalInput() < 0 && transform.localScale.x > 0)      // Left
            transform.localScale = new Vector3(-0.21f, 0.21f, 0.21f);
        else if (GetHorizontalInput() > 0 && transform.localScale.x < 0) // Right
            transform.localScale = new Vector3(0.21f, 0.21f, 0.21f);
    }

    public void PlayerDead()
    {
        PlaySound(fallSound);
        gameManager.GameOver(score);
    }

    public void PlayerJump()
    {
        PlaySound(jumpSound);
    }

    private void SetScore()
    {
        int newScore = Mathf.FloorToInt(transform.position.y - playerFirstYPos) + 1;
        score = Mathf.Max(score, newScore);
    }

    private void PlaySound(AudioClip clipToPlay)
    {
        myAudioSource.clip = clipToPlay;
        myAudioSource.Play();
    }

    public void IsMainstreamTabletRes()
    {
        moveSpeed = 2.8f;
    }

    public void IsAppleIpadRes()
    {
        moveSpeed = 3.2f;
    }

    public void IsMainstreamPhoneRes()
    {
        moveSpeed = 2f;
    }
}
