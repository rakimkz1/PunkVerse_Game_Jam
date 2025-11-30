using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologyScene : MonoBehaviour
{
    public void MoveToTheScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
