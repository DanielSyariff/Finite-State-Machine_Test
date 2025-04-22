# 🥷 Ninja Platformer 2D

A simple 2D Ninja Platformer game built with Unity. The player controls a ninja character capable of moving, jumping, attacking, and surviving enemy hits.
The game features a state machine system, health mechanics with blinking invincibility, knockback, UI updates, and a respawn system.

---

## 🎮 Features

- ✅ **State Machine System** – Manage ninja behavior through states: `Idle`, `Run`, `Jump`, `Attack`, `Hurt`, and `Die`.
- ❤️ **Life System** – The ninja starts with 3 lives. Each enemy hit reduces 1 life. When lives reach 0, the ninja dies.
- ✨ **Blinking Invincibility** – After taking damage, the ninja blinks and becomes temporarily invincible.
- 💨 **Knockback Effect** – Knocked back upon enemy collision for a more dynamic feel.
- 🔁 **Respawn Functionality** – Respawns ninja at the starting point after death.
- 🖥️ **UI Manager** – Real-time display of current lives using Unity UI/TextMeshPro.

## 🎮 Controls

| Key         | Action             |
|-------------|--------------------|
| ← / →       | Move left/right     |
| Space       | Jump                |
| Z / X       | Attack (optional)   |

## 🧪 Testing the Game

- Collide with an enemy → Life decreases, blinking starts.
- During blinking → Ninja is invincible and cannot move/jump.
- After blinking → Control is restored.
- When lives reach 0 → Ninja dies and freezes.
- Respawn → Ninja returns to the original position with full lives.

Powered by Unity 6
By Daniel Akmal
