using UnityEngine;

public class EndChecker : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeadAndDisableCollider(collision.gameObject);
            player.isAlive = false;
        }
    }

    private void PlayerDeadAndDisableCollider(GameObject playerObject)
    {
        player.PlayerDead();
        BoxCollider2D playerCollider = playerObject.GetComponent<BoxCollider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
    }
}
