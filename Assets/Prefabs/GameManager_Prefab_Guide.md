# GameManager Prefab Setup Guide

## GameManager Prefab Configuration

### GameObject Structure
```
GameManager
├── UIManager
├── ItemManager
└── AudioManager (optional)
```

### Required Components
1. **GameManager** (Script)
   - Game Settings: Reference to GameSettings ScriptableObject
   - Game Duration: 300 seconds (5 minutes)

2. **UIManager** (Script)
   - Main Menu Panel: Reference
   - Game HUD Panel: Reference
   - Pause Panel: Reference
   - Game Over Panel: Reference

3. **ItemManager** (Script)
   - Available Items: Array of ItemBase ScriptableObjects
   - Item Spawn Points: Array of Transform references
   - Spawn Interval: 10 seconds
   - Max Items On Field: 5
   - Item Pickup Prefab: Reference to ItemPickup prefab

### Settings
- DontDestroyOnLoad: true
- Singleton pattern implementation
- Event-driven communication with other systems

### Dependencies
- GameSettings ScriptableObject
- ItemBase ScriptableObjects array
- UI Canvas references
- ItemPickup prefab reference

### Initialization Order
1. GameManager (first)
2. UIManager
3. ItemManager
4. Player spawning
5. Game start

### Event System
- OnGameStateChanged
- OnGameTimerUpdated
- OnGameEnded
- Item spawn/pickup events