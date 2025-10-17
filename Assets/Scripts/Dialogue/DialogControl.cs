using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom { pt, en, es };
    public idiom currentIdiom;

    [Header("Components")]
    public GameObject dialoguePanel;
    public Image profileSprite;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private bool _isShowing;
    private int index;
    private string[] dialogueLines;

    public static DialogControl instance;

    public bool isShowing { get { return _isShowing; } set { _isShowing = value; } }

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
        if (speechText.text == dialogueLines[index])
        {
            if (index < dialogueLines.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeDialogue());
            }
            else
            {
                speechText.text = "";
                index = 0;
                isShowing = false;
                dialoguePanel.SetActive(false);
                dialogueLines = null;
            }
        }
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
