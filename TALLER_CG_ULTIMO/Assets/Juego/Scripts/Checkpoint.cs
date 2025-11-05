using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                // Actualiza el checkpoint actual del jugador
                playerRespawn.UpdateCheckpoint(transform.position);

                // Cambia el color visual del checkpoint activo
                if (rend != null)
                    rend.material.color = Color.green;
            }
        }
    }
}
