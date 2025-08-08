# ItemPickup Prefab Setup Guide

## ItemPickup Prefab Configuration

### GameObject Structure
```
ItemPickup
├── Visual (Sprite Renderer)
├── Collider (Circle Collider 2D)
├── Effects (ParticleSystem)
└── Audio (AudioSource)
```

### Required Components
1. **ItemPickup** (Script)
   - Item: Assign ItemBase ScriptableObject
   - Rotation Speed: 90
   - Bob Speed: 2
   - Bob Height: 0.5

2. **CircleCollider2D**
   - Is Trigger: true
   - Radius: 0.5
   - Offset: (0, 0)

3. **SpriteRenderer**
   - Sprite: Item icon sprite
   - Color: White (1, 1, 1, 1)
   - Sorting Layer: Items
   - Order in Layer: 0

4. **ParticleSystem** (Optional)
   - Used for pickup effect
   - Duration: 0.5
   - Max Particles: 20
   - Start Color: Item rarity color
   - Shape: Circle
   - Emission: Burst on pickup

5. **AudioSource** (Optional)
   - Clip: Item pickup sound
   - Play On Awake: false
   - Volume: 0.5

### Animation Settings
- Continuous rotation around Y-axis
- Smooth bobbing motion up and down
- Pulsing glow effect (optional)

### Physics Settings
- No Rigidbody2D needed (kinematic movement)
- Trigger collider for pickup detection

### Layer Assignment
- Layer: Items
- Physics: Trigger only with Players

### Visual Feedback
- Rotation animation
- Bobbing motion
- Color based on item rarity:
  - Common: White
  - Rare: Blue
  - Epic: Purple
  - Legendary: Gold