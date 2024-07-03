using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
            SceneManager.LoadScene(_sceneName);
    }
}
