using Systems.Save;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;

    private void Awake()
    {
        _saveManager.Load();
    }
}
