/*
 * 효과음 한번만 재생할때
 * AudioManager.Instance.PlaySFX(SFXType.PickupItem);
 * 예시
 * 아이템줍기:AudioManager.Instance.PlaySFX(SFXType.PickupItem);
 * 문열기:AudioManager.Instance.PlaySFX(SFXType.DoorOpen);
 * 숨기 들어갈때 : AudioManager.Instance.PlaySFX(SFXType.HideEnter);
 * 루프시킬때 켜기/끄기
 * 켜기:AudioManager.Instance.PlayLoop(SFXType.Heartbeat);
 * 끄기:AudioManager.Instance.StopLoop();
 * 몬스터가 가까워지면 켜고 안전해지면 끄는방식
 * 
 * using에
 * using _Kamikakushi.Audio;
 * using _Kamikakushi.Utills.Audio;
 * 추가하셔야 오디오매니저 SFXType 사용됨
 */