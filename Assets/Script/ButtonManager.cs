
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public void SinglePlayer()
    {
        SceneManager.LoadScene("MainGame",LoadSceneMode.Single);
    }
}
