# Player Prefab Setup Guide

## Player Prefab Configuration

### GameObject Structure
```
Player
├── Visual (Sprite Renderer)
├── Collider (Capsule Collider 2D)
└── Effects (ParticleSystem - optional)
```

### Required Components
1. **PlayerController** (Script)
   - Player Data configuration
   - Move Speed: 5.0
   - Controls Reversed: false

2. **PlayerInput** (Script)
   - Player ID: 1 or 2
   - Use New Input System: true

3. **PlayerStats** (Script)
   - Health: 100
   - Score: 0

4. **Rigidbody2D**
   - Body Type: Dynamic
   - Material: Default
   - Simulated: true
   - Use Full Kinematic Contacts: false

5. **CapsuleCollider2D**
   - Is Trigger: false
   - Size: (1, 2)
   - Offset: (0, 0)

6. **SpriteRenderer**
   - Color: Player 1 = Blue, Player 2 = Red
   - Sorting Layer: Player
   - Order in Layer: 0

### Player Data Configuration
```
Player 1:
- Player ID: 1
- Player Name: "Player 1"
- Spawn Position: (-8, 0)
- Player Color: Blue (0, 0.5, 1, 1)

Player 2:
- Player ID: 2
- Player Name: "Player 2"
- Spawn Position: (8, 0)
- Player Color: Red (1, 0.2, 0.2, 1)
```

### Physics Settings
- Gravity Scale: 0 (top-down movement)
- Linear Drag: 8 (smooth stopping)
- Angular Drag: 5
- Freeze Rotation Z: true

### Layer Assignment
- Layer: Player
- Physics: Collides with Items, Boundaries
- Ignores: Other Players (optional)