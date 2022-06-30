using UnityEngine;

public class Screen : MonoBehaviour
{
    private static Screen _current;
    public bool init;
    [SerializeField] private GameObject _camera;

    public void Awake()
    {
        if (init)
        {
            _current = this;
        }
    }

    public void Show()
    {
        if (_current != this)
        {
            GameUtils.SetAllRenderers(gameObject, true);
            _camera.SetActive(true);
            if (_current != null)
            {
                GameUtils.SetAllRenderers(_current.gameObject, false);
                _current._camera.SetActive(false);
            }

            _current = this;
        }
    }
}