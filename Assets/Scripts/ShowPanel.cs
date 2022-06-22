using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject Panel;

    public void Toggle()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
}