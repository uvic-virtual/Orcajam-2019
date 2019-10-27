using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public Image healthDisplay;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        healthDisplay.fillAmount = 1f;
    }

    private void Damage(float amount = 0.1f)
    {
        healthDisplay.fillAmount -= amount;
        if (healthDisplay.fillAmount <= 0)
        {
            player.GetComponent<CharacterController>().enabled = false;
        }

    }
}
