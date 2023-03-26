using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private Animator checkpoint_animation;
    private bool isFlagRaised = false;
    [SerializeField] private AudioSource checkpointSound;

    private void Start()
    {
        checkpoint_animation = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !isFlagRaised)
        {
            checkpoint_animation.Play("Checkpoint");
            isFlagRaised = true;
            StartCoroutine(freezePlayer(collision.gameObject, 1f));
            Invoke("CompleteLevel", 3f);
        }
    }

    private void RiseFlag()
    {
        checkpoint_animation.SetTrigger("touched");
        checkpointSound.Play();
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator freezePlayer(GameObject player, float timer)
    {
        yield return new WaitForSeconds(timer);
        player.GetComponent<PlayerMovement>().setFreezePlayer(true);
    }
}
