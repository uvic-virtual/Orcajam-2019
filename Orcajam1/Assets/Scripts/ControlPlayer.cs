using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    private CharacterController Controller;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float xTranslation = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float zTranslation = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        Controller.SimpleMove(new Vector3(xTranslation, 0, zTranslation) * Speed);
    }
}
