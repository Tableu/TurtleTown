using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _icon;
    
    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
}
