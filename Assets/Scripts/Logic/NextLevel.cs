using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [HideInInspector]
    public bool hovered = false;

    public void Hovered()
    {
        hovered = true;
    }

    public void Unhovered()
    {
        hovered = false;
    }

    public void Update()
    {
        if (hovered && OVRInput.GetUp(OVRInput.RawButton.A))
        {
            PassToNextLevel();
        }
    }

    private void PassToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
