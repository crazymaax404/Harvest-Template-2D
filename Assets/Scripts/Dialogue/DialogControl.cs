using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialoguePanel;
    public Image profileSprite;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private int index;
    private string[] dialogueLines;

    public static DialogControl instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {

    }

    IEnumerator TypeDialogue()
    {
        foreach (char letter in dialogueLines[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextDialogueLine()
    {
        index++;
        speechText.text = "";
        StartCoroutine(TypeDialogue());
    }

    public void Speech(string[] lines)
    {
        if (!isShowing)
        {
            isShowing = true;
            dialoguePanel.SetActive(true);
            dialogueLines = lines;
            StartCoroutine(TypeDialogue());
        }
    }
}
