# 🚀 빠른 Unity 설치 및 실행 가이드

## Step 1: Unity Hub 설치 (5분)

### 자동 설치 (Terminal):
```bash
# Unity Hub 다운로드 및 설치
curl -L -o ~/Downloads/UnityHubSetup.dmg "https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup-x64.dmg"

# DMG 마운트 및 설치
open ~/Downloads/UnityHubSetup.dmg
# Unity Hub를 Applications 폴더로 드래그하세요
```

### 수동 설치:
1. https://unity.com/download 방문
2. "Download Unity Hub" 클릭
3. DMG 파일 실행 → Applications으로 드래그

## Step 2: Unity 2022.3 LTS 설치 (10분)

```
Unity Hub 실행 → Installs → Install Editor → 
2022.3 LTS 선택 → Mac Build Support 체크 → Install
```

## Step 3: 프로젝트 열기 (1분)

```
Unity Hub → Projects → Open → 
이 폴더 선택: /Users/juwon/Desktop/Develop_Study/unity_game
```

## Step 4: 5분 만에 게임 실행하기

### 핵심 Scene 설정:
```csharp
// 1. GamePlay.unity Scene 생성
File → New Scene → 2D Template → Save As: Assets/Scenes/GamePlay.unity

// 2. GameManager 생성 및 설정
GameObject → Create Empty → "GameManager"
- GameManager.cs 추가
- UIManager.cs 추가  
- ItemManager.cs 추가

// 3. Player1 생성
GameObject → Create Empty → "Player1"
- Position: (-8, 0, 0)
- PlayerController.cs 추가
- Rigidbody2D 추가 (Gravity Scale: 0)
- CapsuleCollider2D 추가
- SpriteRenderer 추가 (Color: Blue)

// 4. Player2 생성  
Duplicate Player1 → "Player2"
- Position: (8, 0, 0)
- PlayerInput playerId: 2로 변경
- SpriteRenderer Color: Red

// 5. Arena 경계 생성
GameObject → Create Empty → "Boundaries"
하위에 4개 Wall 생성:
- TopWall: (0, 6, 0), Scale (20, 1, 1)
- BottomWall: (0, -6, 0), Scale (20, 1, 1)
- LeftWall: (-10, 0, 0), Scale (1, 12, 1)  
- RightWall: (10, 0, 0), Scale (1, 12, 1)
각각에 BoxCollider2D 추가

// 6. UI Canvas
GameObject → UI → Canvas
- GameHUD Panel 생성 (전체 화면 크기)
```

## Step 5: 즉시 플레이 테스트! 🎮

**Play 버튼 클릭** → 즉시 2P 게임 테스트 가능!

### 컨트롤:
- **Player 1 (Blue)**: WASD 이동, L.Shift/L.Ctrl 아이템
- **Player 2 (Red)**: 화살표 이동, R.Shift/R.Ctrl 아이템

## 🎯 30초 체크리스트:

플레이 모드에서 확인:
- [ ] 파란 네모(P1)와 빨간 네모(P2)가 보임
- [ ] WASD로 파란 네모 움직임
- [ ] 화살표로 빨간 네모 움직임  
- [ ] 경계벽에서 멈춤
- [ ] Console에 심각한 에러 없음

## 🚨 빠른 문제 해결:

### Input System 에러:
```
Window → Package Manager → Input System → Install
Project Settings → Player → Active Input Handling → Both
```

### 스크립트 컴파일 에러:
```
Window → General → Console에서 에러 확인
대부분 missing using 구문이나 오타
```

### 움직임 안됨:
```
Player GameObject에 PlayerInput.cs와 PlayerController.cs 확인
Rigidbody2D Gravity Scale: 0 설정
```

---

**🎉 성공시**: 5분 만에 2P 로컬 멀티플레이어 게임 프로토타입 완성!

**다음 단계**: 아이템 시스템, UI, 사운드 추가하며 완전한 게임으로 발전시키기