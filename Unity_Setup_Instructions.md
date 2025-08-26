# Unity 설치 및 프로젝트 실행 가이드

## 1단계: Unity Hub 설치

### macOS 설치 과정:
1. **Unity Hub 다운로드**
   - https://unity.com/download 방문
   - "Download the Unity Hub" 클릭
   - macOS용 DMG 파일 다운로드

2. **Unity Hub 설치**
   ```bash
   # 다운로드된 DMG 파일을 더블클릭하여 마운트
   # Unity Hub를 Applications 폴더로 드래그
   ```

3. **Unity Hub 실행**
   - Applications 폴더에서 Unity Hub 실행
   - 첫 실행 시 보안 경고가 나올 수 있음 (시스템 환경설정 → 보안 및 개인 정보 보호에서 허용)

## 2단계: Unity Editor 설치

### Unity 2022.3 LTS 설치:
1. **Unity Hub에서 설치**
   - Unity Hub 실행
   - "Installs" 탭 클릭
   - "Install Editor" 버튼 클릭
   - "2022.3 LTS" 선택 (권장 버전)
   - 필요한 모듈 선택:
     - ✅ Mac Build Support (Intel 64-bit)
     - ✅ Mac Build Support (Apple Silicon)
     - ✅ Visual Studio for Mac (선택사항)

2. **설치 완료 대기**
   - 다운로드 및 설치 시간: 약 5-15분
   - 인터넷 연결 필요

## 3단계: 프로젝트 열기

### Bubble Battle 프로젝트 열기:
1. **Unity Hub에서 프로젝트 추가**
   ```
   Unity Hub → Projects → Open → 
   /Users/juwon/Desktop/Develop_Study/unity_game 폴더 선택
   ```

2. **프로젝트 열기**
   - 프로젝트 목록에서 "unity_game" 클릭
   - Unity Editor 실행 대기 (첫 실행 시 컴파일 시간 소요)

## 4단계: Scene 및 Prefab 설정

### 필수 설정 단계:
1. **GamePlay Scene 생성**
   ```
   File → New Scene → 2D Template → Save As → Assets/Scenes/GamePlay.unity
   ```

2. **GameManager 설정**
   - 빈 GameObject 생성 → 이름: "GameManager"
   - GameManager.cs 스크립트 추가
   - UIManager.cs 스크립트 추가
   - ItemManager.cs 스크립트 추가

3. **Player Prefab 생성**
   - GameObject → Create Empty → 이름: "Player1"
   - PlayerController.cs 스크립트 추가
   - PlayerInput.cs, PlayerStats.cs 추가
   - Rigidbody2D, CapsuleCollider2D 컴포넌트 추가
   - SpriteRenderer 추가 (임시 색상: 파란색)

4. **UI Canvas 설정**
   - GameObject → UI → Canvas
   - Canvas 하위에 GameHUD Panel 생성
   - EventSystem 자동 생성 확인

5. **Arena 설정**
   - 경계벽 생성 (BoxCollider2D 사용)
   - 아이템 스폰 포인트 배치
   - 플레이어 스폰 포인트 설정

## 5단계: 테스트 실행

### 게임 테스트:
1. **Play 버튼 클릭**
2. **컨트롤 테스트**
   - Player 1: WASD 이동, Left Shift (아이템 사용), Left Ctrl (아이템 교체)
   - Player 2: 화살표 키 이동, Right Shift (아이템 사용), Right Ctrl (아이템 교체)

3. **확인사항**
   - [ ] 플레이어 이동 작동
   - [ ] UI 표시 정상
   - [ ] 타이머 카운트다운
   - [ ] 아이템 스폰 및 획득
   - [ ] 조작 반전 아이템 효과

## 문제 해결

### 일반적인 문제들:
1. **스크립트 컴파일 에러**
   - Unity Input System 패키지 확인: Window → Package Manager → Input System 설치
   - 에러 콘솔 확인: Window → General → Console

2. **Input System 관련 오류**
   - Project Settings → Input System Package → Both (Old/New) 선택
   - PlayerInputActions.inputactions 파일 존재 확인

3. **ScriptableObject 참조 오류**
   - GameSettings 에셋을 GameManager에서 참조
   - 아이템 에셋들을 ItemManager에서 참조

### 설정 완료 확인 체크리스트:
- [ ] Unity 2022.3 LTS 설치 완료
- [ ] 프로젝트가 에러 없이 열림
- [ ] GamePlay Scene 생성됨
- [ ] Player Prefab 설정 완료
- [ ] UI Canvas 작동
- [ ] 게임이 Play 모드에서 실행됨

---

**설정 완료 후**: 
- 게임 밸런스 조정
- 비주얼 에셋 추가 (스프라이트, 파티클 효과)
- 사운드 이펙트 추가
- 빌드 및 배포

모든 코드와 구조는 이미 준비되어 있습니다! 🚀