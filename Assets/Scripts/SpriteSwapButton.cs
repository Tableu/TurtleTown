using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;

    public void OnClick()
    {
        _image.sprite = _image.sprite.Equals(_sprite1) ? _sprite2 : _sprite1;
    }
}
