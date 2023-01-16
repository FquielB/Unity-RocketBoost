using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip explosion;
    
    AudioSource audioSource;
    bool isTransitioning = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    { 
        if(isTransitioning) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friend, you'll not lose");
                break;
            case "Finish":
                StartSequence("LoadNextLevel", success);
                break;
            default:
                StartSequence("ReloadScene", explosion);
                break;
        }
    }
    
    void StartSequence(string sceneTransitionProcess, AudioClip audioToExec)
    {
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(audioToExec);
            Invoke(sceneTransitionProcess, delay);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex+1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }
}
