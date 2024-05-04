using UnityEngine;

public enum LeverState { Open, Closed }

public class Lever : MonoBehaviour
{
    public Door door;
    public KeyCode leverKeyCode = KeyCode.E;
    public LeverState currentState = LeverState.Closed;
    public bool playerNearby = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsOpened", false);
    }

    void Update()
    {

        if (playerNearby && Input.GetKeyDown(leverKeyCode))
        {

            if (currentState == LeverState.Open)
            {
                door.Close();
                animator.SetBool("IsOpened", false);
                currentState = LeverState.Closed;

            }
            else
            {
                door.Open();
                animator.SetBool("IsOpened", true);
                currentState = LeverState.Open;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
