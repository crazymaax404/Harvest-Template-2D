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
            switch (DialogControl.instance.currentIdiom)
            {
                case DialogControl.idiom.pt:
                    dialogues.Add(dialogueSettings.dialogues[i].language.portuguese);
                    break;
                case DialogControl.idiom.en:
                    dialogues.Add(dialogueSettings.dialogues[i].language.english);
                    break;
                case DialogControl.idiom.es:
                    dialogues.Add(dialogueSettings.dialogues[i].language.spanish);
                    break;
            }
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
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
