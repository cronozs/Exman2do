using UnityEngine;
using UnityEngine.SceneManagement;
namespace Shooter
{
    public class ManagerScenes : MonoBehaviour
    {
        public void SceneChanger(int scene)
        {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
        }
    }
}
