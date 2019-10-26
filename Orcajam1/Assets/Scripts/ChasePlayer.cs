using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
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
        Controller.SimpleMove(transform.TransformDirection(Vector3.forward) * Speed);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.Equals(Player))
        {
            PlayerHealth.Health -= DamageAmount;
            //Debug.Log(PlayerHealth.Health);
        }
    }

    private void FacePlayer()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
    }

}
