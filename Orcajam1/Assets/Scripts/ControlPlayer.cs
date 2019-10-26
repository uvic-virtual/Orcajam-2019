using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    private void Update()
    {
        float translation = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(translation, 0, 0);
    }
}
