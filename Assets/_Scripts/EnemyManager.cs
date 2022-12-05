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
    }
}
