using UnityEngine;

namespace _Scripts {
    public class CircularHealthBar : MonoBehaviour {
        [SerializeField] private float innerRadius;
        [SerializeField] private float outerRadius;
        [SerializeField] private int curPercent;
        [SerializeField] private int maxPercent;

        private MeshFilter _filter;
        private Mesh _mesh;
        private Vector3[] _defaultVertices;
        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;
        private int _timer;

        private void Start() {
            transform.position = Vector3.zero;
            _filter = GetComponent<MeshFilter>();
            _mesh = new Mesh();
            GenerateMesh();
        }

        private void GenerateMesh() {
            _defaultVertices = new Vector3[(maxPercent + 1) * 2];
            _vertices = new Vector3[(maxPercent + 1) * 2];
            _uv = new Vector2[(maxPercent + 1) * 2];
            _triangles = new int[maxPercent * 2 * 3];
            
            for (int i = 0; i <= maxPercent; i++) {
                var degree = 360f / maxPercent * i;
                _defaultVertices[i * 2] = innerRadius * Calc.Degree2Direction(degree);
                _defaultVertices[i * 2 + 1] = outerRadius * Calc.Degree2Direction(degree);
                
                _vertices[i * 2] = _defaultVertices[i * 2] + transform.position;
                _vertices[i * 2 + 1] = _defaultVertices[i * 2 + 1] + transform.position;
                
                _uv[i * 2] = (float)i / maxPercent * Vector2.up;
                _uv[i * 2 + 1] = (float)i / maxPercent * Vector2.one;
                
                if(i == maxPercent) continue;
                // the last two points dont have triangle sets.
                
                _triangles[i * 6] = i * 2;
                _triangles[i * 6 + 1] = (i + 1) * 2 + 1;
                _triangles[i * 6 + 2] = i * 2 + 1;

                _triangles[i * 6 + 3] = i * 2;
                _triangles[i * 6 + 4] = (i + 1) * 2;
                _triangles[i * 6 + 5] = (i + 1) * 2 + 1;
            }
            
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _mesh.triangles = _triangles;
            _filter.mesh = _mesh;
        }

        private void RefreshMesh() {
            for (int i = 0; i < maxPercent - curPercent; i++) {
                var tmp = maxPercent - curPercent;
                _vertices[i * 2] = _defaultVertices[tmp * 2] + transform.position;
                _vertices[i * 2 + 1] = _defaultVertices[tmp * 2 + 1]  + transform.position;
            }
            
            for (int i = maxPercent - curPercent; i <= maxPercent; i++) {
                _vertices[i * 2] = _defaultVertices[i * 2] + transform.position;
                _vertices[i * 2 + 1] = _defaultVertices[i * 2 + 1] + transform.position;
            }
            
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _filter.mesh = _mesh;
        }
        
        // Update is called once per frame
        void Update()
        {
            RefreshMesh();
            _timer++;
            if (_timer % 10 == 0) curPercent -= 1;
        }
    }
}
