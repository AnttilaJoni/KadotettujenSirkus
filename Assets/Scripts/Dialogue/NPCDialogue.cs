using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewNPCDialogue", menuName ="NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string playerName = "Emma";
    public string npcName;
    public Sprite[] npcPortrait;
    public string[] dialogueLines;
    public bool[] isPlayerLine;
    public bool[] isScreenShakeLine;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip[] voiceSounds_player;
    public AudioClip[] voiceSounds_npc;
    public float voicePitch = 1f;
}