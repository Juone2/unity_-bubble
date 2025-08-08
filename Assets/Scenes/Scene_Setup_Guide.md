# Scene Setup Guide

## Scene Structure Overview

### Main Scenes Required
1. **MainMenu.unity** - Main menu and settings
2. **GamePlay.unity** - Core gameplay scene  
3. **TestScene.unity** - Development testing

## GamePlay Scene Setup

### Scene Hierarchy
```
GamePlay Scene
├── Main Camera
├── GameManager
├── Canvas (UI)
│   ├── GameHUD
│   ├── MenuPanels
│   └── EventSystem
├── Arena
│   ├── Background
│   ├── Boundaries
│   ├── ItemSpawnPoints
│   └── PlayerSpawnPoints
├── Player1
├── Player2
└── Lighting (2D)
```

### Camera Configuration
- **Main Camera**
  - Projection: Orthographic
  - Size: 7-8 units
  - Position: (0, 0, -10)
  - Clear Flags: Solid Color
  - Background: #1a1a1a (dark gray)
  - Culling Mask: Everything
  - Allow HDR: false

### GameManager Setup
- Create empty GameObject named "GameManager"
- Add GameManager script
- Add UIManager script  
- Add ItemManager script
- Set as DontDestroyOnLoad
- Reference GameSettings ScriptableObject

### Canvas (UI) Setup
- **Canvas**
  - Render Mode: Screen Space - Overlay
  - Canvas Scaler: Scale With Screen Size
  - Reference Resolution: 1920x1080
  - Screen Match Mode: Match Width Or Height
  - Match: 0.5

- **GameHUD Panel**
  - Anchored to fill screen
  - Contains timer, player stats, item slots

- **Menu Panels**
  - MainMenu Panel (initially active)
  - PauseMenu Panel (initially inactive)
  - GameOver Panel (initially inactive)

### Arena Setup
Follow Arena_Setup_Guide.md for complete arena configuration:
- Background sprite
- Boundary walls with colliders
- 5 item spawn points
- 2 player spawn points

### Player Setup
- **Player1**
  - Use Player Prefab
  - Position: (-8, 0, 0)
  - Set PlayerID: 1
  - Configure for WASD controls

- **Player2**
  - Use Player Prefab
  - Position: (8, 0, 0)
  - Set PlayerID: 2
  - Configure for Arrow key controls

### Lighting Setup (Optional)
- Add 2D Light (Global)
- Intensity: 1
- Color: White
- Light Type: Global

## MainMenu Scene Setup

### Scene Hierarchy
```
MainMenu Scene
├── Main Camera
├── Canvas
│   ├── MainMenuPanel
│   │   ├── Title
│   │   ├── StartButton
│   │   ├── SettingsButton
│   │   └── QuitButton
│   └── EventSystem
└── Background
```

### Menu Configuration
- Background image or simple color
- Title text: "Bubble Battle"
- Start Game button → Load GamePlay scene
- Settings button → Open settings panel
- Quit button → Application.Quit()

## Layer Setup

### Required Layers
1. **Default** (0) - General objects
2. **Player** (8) - Player objects
3. **Items** (9) - Item pickups
4. **Boundaries** (10) - Arena walls
5. **UI** (5) - UI elements

### Physics Collision Matrix
- Players ↔ Boundaries: Collide
- Players ↔ Items: Trigger only
- Players ↔ Players: Ignore (or collide)
- Items ↔ Everything else: Trigger only

## Tags Setup

### Required Tags
- **Player** - Player GameObjects
- **Item** - Item pickup objects
- **Wall** - Boundary walls
- **ItemSpawnPoint** - Item spawn locations
- **PlayerSpawnPoint** - Player spawn locations

## Scene Build Settings

### Build Index Order
0. MainMenu
1. GamePlay
2. TestScene (development only)

### Scene Loading
- Use SceneManager.LoadScene() for transitions
- Implement loading screen for larger scenes
- Ensure proper cleanup between scenes

## Testing Checklist

### GamePlay Scene
- [ ] Both players can move with their respective controls
- [ ] Items spawn at designated points
- [ ] Items can be picked up and used
- [ ] UI updates correctly (timer, scores, health)
- [ ] Game ends after timer expires
- [ ] Pause/resume functionality works

### MainMenu Scene  
- [ ] Start button loads GamePlay scene
- [ ] All buttons respond to input
- [ ] Scene transition is smooth

### General
- [ ] No console errors on scene load
- [ ] All prefab references are assigned
- [ ] Audio plays correctly (if implemented)
- [ ] Performance is stable (60 FPS target)