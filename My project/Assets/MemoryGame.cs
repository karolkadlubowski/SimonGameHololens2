using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    public float highlightDuration = 1.0f;
    public Color highlightColor = Color.white;
    public float delayBetweenCubes = 0.5f;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioClip winClip;
    public AudioClip loseClip;

    private List<int> sequence = new List<int>();
    private List<int> playerSequence = new List<int>();
    private Color[] originalColors;
    private bool playerTurn = false;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        originalColors = new Color[cubes.Count];
        for (int i = 0; i < cubes.Count; i++)
        {
            originalColors[i] = cubes[i].GetComponent<Renderer>().material.color;
        }

        StartNewRound();
    }

    public void StartNewRound()
    {
        playerTurn = false;
        playerSequence.Clear();
        AddToSequence();
        StartCoroutine(ShowSequence());
    }

    private void AddToSequence()
    {
        sequence.Add(Random.Range(0, cubes.Count));
    }

    private IEnumerator ShowSequence()
    {
        yield return new WaitForSeconds(1.0f);

        foreach (int index in sequence)
        {
            cubes[index].GetComponent<Renderer>().material.color = highlightColor;
            audioSource.PlayOneShot(audioClips[index]);
            yield return new WaitForSeconds(highlightDuration);
            cubes[index].GetComponent<Renderer>().material.color = originalColors[index];
            yield return new WaitForSeconds(delayBetweenCubes);
        }

        playerTurn = true;
    }

    public void CubeTouched(int index)
    {
        if (playerTurn)
        {
            playerSequence.Add(index);
            CheckPlayerSequence();
        }
    }

    private void CheckPlayerSequence()
    {
        if (playerSequence.Count > sequence.Count)
        {
            GameOver();
            return;
        }

        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != sequence[i])
            {
                audioSource.PlayOneShot(loseClip);
                GameOver();
                return;
            }
        }

        if (playerSequence.Count == sequence.Count)
        {
            // Player successfully completed the sequence
            // You can give feedback here if needed, and start a new round
            audioSource.PlayOneShot(winClip);

            StartNewRound();
        }
    }

    private void GameOver()
    {
        // Logic for when the player makes a mistake.
        Debug.Log("Game Over! Sequence length: " + sequence.Count);

        // If you want to reset the game:
        sequence.Clear();
        StartNewRound();
    }
}
