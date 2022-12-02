using UnityEngine;

namespace _Scripts {
    public class HealthBarFrame : MonoBehaviour {
        [SerializeField] private float radius;
        [SerializeField] private int maxPercent;
        private Vector3[] _defaultPoints;
        private LineRenderer _line;

        private void Start() {
            _line = GetComponent<LineRenderer>();
            GenerateLine();
        }
        
        private void GenerateLine() {
            _defaultPoints = new Vector3[maxPercent + 1];
            for (int i = 0; i <= maxPercent; i++) {
                var degree = 360f / maxPercent * i + 90f;
                _defaultPoints[i] = radius * Calc.Degree2Direction(degree);
            }

            _line.positionCount = maxPercent + 1;
            _line.SetPositions(_defaultPoints);
        }
        
    }
}
