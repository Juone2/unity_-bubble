# Arena Setup Guide

## Arena Configuration

### GameObject Structure
```
Arena
├── Background
├── Boundaries
│   ├── TopWall
│   ├── BottomWall
│   ├── LeftWall
│   └── RightWall
├── ItemSpawnPoints
│   ├── SpawnPoint1
│   ├── SpawnPoint2
│   ├── SpawnPoint3
│   ├── SpawnPoint4
│   └── SpawnPoint5
└── PlayerSpawnPoints
    ├── Player1Spawn
    └── Player2Spawn
```

### Arena Dimensions
- Width: 20 units
- Height: 12 units
- Center: (0, 0)

### Boundary Walls
- **TopWall**: Position (0, 6), Scale (20, 1, 1)
- **BottomWall**: Position (0, -6), Scale (20, 1, 1)
- **LeftWall**: Position (-10, 0), Scale (1, 12, 1)
- **RightWall**: Position (10, 0), Scale (1, 12, 1)

Each wall needs:
- BoxCollider2D (Is Trigger: false)
- Layer: Boundaries
- Tag: Wall

### Item Spawn Points
Positions for item spawning:
1. **Center**: (0, 0)
2. **Top-Left**: (-5, 3)
3. **Top-Right**: (5, 3)
4. **Bottom-Left**: (-5, -3)
5. **Bottom-Right**: (5, -3)

Each spawn point:
- Empty GameObject
- Tag: ItemSpawnPoint
- Gizmo for visibility in editor

### Player Spawn Points
- **Player1Spawn**: (-8, 0)
- **Player2Spawn**: (8, 0)

### Background
- Large sprite or tiled background
- Sorting Layer: Background
- Order in Layer: -10

### Visual Elements
- Grid lines (optional)
- Center circle marker
- Corner decorations
- Lighting setup (2D or 3D)

### Camera Setup
- Orthographic camera
- Size: 7-8 units
- Position: (0, 0, -10)
- Clear Flags: Solid Color
- Background: Dark blue or black