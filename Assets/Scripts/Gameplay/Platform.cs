using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float destroyDelay;
    [SerializeField] private bool isUnlimited;
    /*    [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;*/
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUnlimited)
        {
            if (collision.relativeVelocity.y <= 0f)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                    collision.gameObject.SendMessage("PlayerJump", SendMessageOptions.DontRequireReceiver);
                }
            }
        }else
        {
            Invoke("DestroyPlatform", destroyDelay);
            if (collision.relativeVelocity.y <= 0f)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                    collision.gameObject.SendMessage("PlayerJump", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }

    private void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
