using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip explosion;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    
    AudioSource audioSource;
    bool isTransitioning = false;
    bool areCollisionsDisabled = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            areCollisionsDisabled = !areCollisionsDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    { 
        if(isTransitioning || areCollisionsDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friend, you'll not lose");
                break;
            case "Finish":
                StartSequence("LoadNextLevel", success, successParticles);
                break;
            default:
                StartSequence("ReloadScene", explosion, explosionParticles);
                break;
        }
    }
    
    void StartSequence(string sceneTransitionProcess, AudioClip audioToExec, ParticleSystem particleSystemToExec)
    {
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(audioToExec);
            particleSystemToExec.Play();
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
