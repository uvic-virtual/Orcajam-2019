using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float FollowRadius = 0.2f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private int DamageAmount = 10;

    private static GameObject Player;
    private static HealthManager PlayerHealth;

    private CharacterController Controller;

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerHealth = Player.GetComponentInChildren<HealthManager>();
        }
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        FacePlayer();
        if (Vector3.Distance(transform.position, Player.transform.position) < FollowRadius)
        {
            PlayerHealth.Health -= DamageAmount;
        }
        else
        {
            Controller.SimpleMove(transform.TransformDirection(Vector3.forward) * Speed);
        }
    }

    private void FacePlayer()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
    }

}
