# Scenes Directory

This directory contains Unity scene files for the Bubble Battle game.

## Scene Files

### MainMenu.unity
The main menu scene where players start the game, access settings, and quit.

**Key Components:**
- Main menu UI
- Game title display
- Start/Settings/Quit buttons
- Background visuals

### GamePlay.unity  
The core gameplay scene where the 2-player battle takes place.

**Key Components:**
- Game arena with boundaries
- Player spawn points
- Item spawn points
- Game UI (HUD, timer, scores)
- GameManager and core systems

### TestScene.unity
Development scene for testing individual components and features.

**Key Components:**
- Isolated testing environment
- Debug tools and utilities
- Component testing setups

## Setup Instructions

1. **For Unity Developers:**
   - Open Unity 2022.3 LTS
   - Load the project
   - Open Scene_Setup_Guide.md for detailed setup instructions
   - Build scenes in the order: MainMenu → GamePlay → TestScene

2. **Scene Dependencies:**
   - All scenes require the GameManager prefab
   - GamePlay scene requires Player prefabs
   - UI Canvas prefabs needed for all interactive scenes

3. **Build Settings:**
   - Add scenes to Build Settings in the correct order
   - MainMenu should be scene index 0
   - GamePlay should be scene index 1

## Notes

- Scene files (.unity) are binary and cannot be viewed in text editors
- Use Unity Editor to modify scene contents
- Follow the setup guides in the Prefabs directory for proper configuration
- Test each scene individually before building the complete game