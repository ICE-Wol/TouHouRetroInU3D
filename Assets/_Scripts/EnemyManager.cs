using System.Collections.Generic;
using UnityEngine;

namespace _Scripts {
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyController> enemyList;
        public static EnemyManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
