using UnityEngine;
using TMPro;
namespace Shooter
{
    public class PointsManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pointsText;
        public int points;

        private void OnEnable()
        {
            EnemyHealth.onPoint += AddPoints;
        }

        private void OnDisable()
        {
            EnemyHealth.onPoint -= AddPoints;
        }

        private void AddPoints(int point)
        {
            points += point;
            pointsText.text = points.ToString();
        }
    }
}
