using UnityEngine;
using UnityEngine.UI;

public class Scaler : MonoBehaviour
{
    public Image portraitImage_NPC;
    void Update()
    {
        portraitImage_NPC.SetNativeSize();
    }
}
