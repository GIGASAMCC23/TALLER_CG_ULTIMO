using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastCheckpoint = transform.position; // posición inicial
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
        Debug.Log("Nuevo checkpoint guardado en: " + newCheckpoint);
    }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = lastCheckpoint;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            GameManager.Instance.AddFall();
            Respawn();
        }
    }
}

