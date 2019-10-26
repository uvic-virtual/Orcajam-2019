using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;

    private static GameObject Player;
    private CharacterController Controller;

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        FacePlayer();
        Controller.SimpleMove(transform.TransformDirection(Vector3.forward) * Speed);
    }

    private void FacePlayer()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
    }

}
