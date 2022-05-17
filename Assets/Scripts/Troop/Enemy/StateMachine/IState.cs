
namespace Troop.Enemy.StateMachine {
    public interface IState
    {
        IState DoState(EnemyDecisions enemyAI);
    }
}
