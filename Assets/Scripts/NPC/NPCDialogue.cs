using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public string[] dialogue;
    public DialogueSettings dialogueSettings;

    private bool isPlayerInRange;
    private List<string> dialogues = new List<string>();

    void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isPlayerInRange)
        {
            DialogControl.instance.Speech(dialogues.ToArray());
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    private void GetNPCInfo()
    {
        for (int i = 0; i < dialogueSettings.dialogues.Count; i++)
        {
            dialogues.Add(dialogueSettings.dialogues[i].language.portuguese);
        }
    }

    private void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        if (hit != null)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            DialogControl.instance.dialoguePanel.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
