using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private Player Player;
    [SerializeField] private float _drag = 0.3f;

    private void Start()
    {
        Player = GetComponent<Player>();
    }

    public void Jump(float jumpForce)
    {
        if(Player.GroundCheck.IsGrounded())
        {
            Player.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}