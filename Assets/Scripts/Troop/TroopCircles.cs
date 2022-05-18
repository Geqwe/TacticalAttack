using UnityEngine;

namespace Troop {

    public class TroopCircles : MonoBehaviour
    {
        private TroopStats _troopStats;
        [SerializeField] private LineRenderer[] _lineRenderers;
        [SerializeField] private Color _movementCircleColor, _attackCircleColor;
        [SerializeField] private float _circleWidth = 0.1f;
        private int _segments = 200;

        void Start()
        {
            _troopStats = GetComponent<TroopStats>();
            CreateCircle(_lineRenderers[0], _troopStats.MovementRadius, _movementCircleColor);
            CreateCircle(_lineRenderers[1], _troopStats.AttackRadius, _attackCircleColor);
        }

        public void CreateMovementCircleWithNewRadius(float radius) {
            CreateCircle(_lineRenderers[0], radius, _movementCircleColor);
        }

        private void CreateCircle(LineRenderer lineRenderer, float radius, Color color) {
            float x;
            float y;

            lineRenderer.startColor = color; 
            lineRenderer.endColor = color;
            lineRenderer.startWidth = _circleWidth;
            lineRenderer.endWidth = _circleWidth;
            lineRenderer.positionCount = _segments+1;
            lineRenderer.useWorldSpace = false;

            float angle = 0f;

            for (int i = 0; i < (_segments + 1); i++)
            {
                x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
                y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition (i,new Vector3(x,0,y));

                angle += (360f / _segments);
            }
        }
    }

}
