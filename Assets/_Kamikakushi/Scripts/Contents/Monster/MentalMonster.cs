using UnityEngine;

namespace Assets._Kamikakushi.Contents.Monster
{
    public class MentalMonster : Monster
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }

        public virtual bool Hit(Vector3 targetPos)
        {
            // ¸àÅ» Å¸ÀÔ °ø°Ý Ã³¸®
            Debug.Log($"{name} Mental Attack!");
            return true;
        }
    }
}
