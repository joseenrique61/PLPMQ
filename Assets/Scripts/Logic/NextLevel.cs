using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [HideInInspector]
    public bool selected = false;

    public void Selected()
    {
        selected = true;
    }

    public void Unselected()
    {
        selected = false;
    }

    public void Update()
    {
        if (selected && OVRInput.GetUp(OVRInput.RawButton.A))
        {
            PassToNextLevel();
        }
    }

    private void PassToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
