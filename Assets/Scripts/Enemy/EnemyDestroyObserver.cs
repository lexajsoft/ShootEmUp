using Commands;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroyObserver : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;

        private void OnEnable()
        {
            _enemyManager.OnEnemyWasDestroyed += OnEnemyWasDestroyed;
        }
        
        private void OnDisable()
        {
            _enemyManager.OnEnemyWasDestroyed -= OnEnemyWasDestroyed;
        }

        private void OnEnemyWasDestroyed(GameObject obj)
        {
            // как вариант можно было бы создать еще ScoreComponent который потом просто закидывался бы противника
            // и когда он умирает мы могли бы запрашивать у него этот компонент и далее забирать значение и передовать в команду
            // на передачу очков
            new AddScoreCommand(25).Execute();
        }
    }
}