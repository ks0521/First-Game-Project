using UnityEngine;

namespace Assets._Kamikakushi.Contents.Monster
{
    public class MentalMonster : Monster
    {
        public float moveSpeed = 5f;            // 몬스터 이동 속도
        public GameObject target;               // 추적할 대상(플레이어)

        public float chaseRange = 10f;          // 플레이어를 추적할 수 있는 최대 거리
        public float returnThreshold = 0.5f;    // 원래 위치로 거의 돌아왔다고 판단하는 거리

        private Vector3 originPos;              // 몬스터의 초기 위치(스폰 위치)
        private bool isReturning = false;       // 현재 복귀 중인지 여부

        private void Start()
        {
            // 시작 시 몬스터의 처음 위치를 저장
            originPos = transform.position;
        }

        // 특정 위치(targetPos)를 향해 몬스터를 이동시키는 함수
        public override void Move(Vector3 targetPos)
        {
            // 이동 방향 계산 (정규화된 벡터)
            Vector3 dir = (targetPos - transform.position).normalized;
            if (dir == Vector3.zero) return; // 동일 위치면 이동할 필요 없음

            // 이동
            transform.position += dir * moveSpeed * Time.deltaTime;

            // 이동 방향을 바라보도록 회전
            transform.rotation = Quaternion.LookRotation(dir);
        }

        private void Update()
        {
            // 현재 몬스터와 플레이어 사이 거리 계산
            float dist = Vector3.Distance(transform.position, target.transform.position);

            // 플레이어가 추적 범위를 벗어나면 복귀 모드 ON
            if (dist > chaseRange)
                isReturning = true;

            // 복귀 중이 아니라면 → 플레이어를 계속 추적
            if (!isReturning)
            {
                Move(target.transform.position);
                return; // 아래 복귀 로직 실행 방지
            }

            // 복귀 중이라면 → 원래 위치(originPos)로 이동
            Move(originPos);

            // 거의 원래 위치에 도달하면 복귀 종료
            if (Vector3.Distance(transform.position, originPos) <= returnThreshold)
                isReturning = false;
        }
    }
}
