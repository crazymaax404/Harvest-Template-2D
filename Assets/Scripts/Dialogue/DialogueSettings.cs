using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string text;

    public List<Text> dialogues = new List<Text>();
}

[System.Serializable]
public class Text
{
    public string actorName;
    public Sprite profile;
    public Language language;

}

[System.Serializable]
public class Language
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class DialogueSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
       DrawDefaultInspector();

       DialogueSettings dialogueSettings = (DialogueSettings)target;
       Language language = new Language();
       language.portuguese = dialogueSettings.text;
       Text text = new Text();
       text.profile = dialogueSettings.speakerSprite;
       text.language = language;

       if (GUILayout.Button("Add Dialogue"))
       {
        if(dialogueSettings.text != "")
        {
            dialogueSettings.dialogues.Add(text);
            dialogueSettings.text = "";
            dialogueSettings.speakerSprite = null;
        }
       }
    }
}
#endif
