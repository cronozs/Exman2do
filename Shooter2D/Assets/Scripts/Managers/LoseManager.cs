using UnityEngine;
using TMPro;

namespace Shooter
{
    public class LoseManager : MonoBehaviour
    {
        private OleadaManager oleadaManager;
        private PointsManager pointsManager;
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI oleada;
        private void Start()
        {
            oleadaManager = FindObjectOfType<OleadaManager>();
            pointsManager = FindObjectOfType<PointsManager>();
        }
        private void OnEnable()
        {
            PlayerHP.loseAct += UpdateCanvas;
        }

        private void OnDisable()
        {
            PlayerHP.loseAct -= UpdateCanvas;
        }

        private void UpdateCanvas()
        {
            oleada.text = oleadaManager.oleada.ToString();
            points.text = pointsManager.points.ToString();
            Time.timeScale = 0;
        }

        private void BackToMenu()
        {
            Time.timeScale = 1;
        }
    }
}
