# ScpLockdown

<a href="https://github.com/Raul125/ScpLockdown/releases"><img src="https://img.shields.io/github/v/release/Raul125/ScpLockdown?include_prereleases&label=Release" alt="Releases"></a>
<a href="https://github.com/Raul125/ScpLockdown/releases"><img src="https://img.shields.io/github/downloads/Raul125/ScpLockdown/total?label=Downloads" alt="Downloads"></a>

Exiled Plugin to lockdown SCPs at the round beginning for a specified amount of time, and more features, check the
config below.<br>

---

### Configs

Default:

```yaml
scp_lockdown:
# Defines the affected SCPs, the text displayed upon unlocking, and the lockdown duration. (RoleTypeId, string, int => RoleType, display text, lockdown time in seconds)
  affected_scps:
  - role_type: Scp079
    text: 'Containment Breach!'
    time_to_unlock: 60
  - role_type: Scp173
    text: 'Containment Breach!'
    time_to_unlock: 60
  - role_type: Scp096
    text: 'Containment Breach!'
    time_to_unlock: 60
  - role_type: Scp106
    text: 'Containment Breach!'
    time_to_unlock: 60
  - role_type: Scp049
    text: 'Containment Breach!'
    time_to_unlock: 60
  - role_type: Scp939
    text: 'Containment Breach!'
    time_to_unlock: 60
  # Specifies doors to manage (e.g., lock, unlock, open, or destroy) after a delay. These doors are locked at the start of the round. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)
  affected_doors:
  - door_type: CheckpointLczA
    delay: 60
    unlock: true
    destroy: false
    open: false
  - door_type: CheckpointLczB
    delay: 60
    unlock: true
    destroy: false
    open: false
  - door_type: PrisonDoor
    delay: 60
    unlock: false
    destroy: false
    open: true
  # Defines CASSIE announcements with specified timing. (string, string, int => CASSIE message text, subtitle text [optional, leave it empty ''], delay in seconds)
  cassies:
  - content: 'containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols'
    subtitle: 'Containment breach detected!'
    delay: 60
  - content: 'containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols'
    subtitle: 'Containment breach detected!'
    delay: 120
  # Determines if SCP-079 can use or switch cameras while in lockdown.
  scp079_camera: true
  # Specifies whether the plugin should display hints or broadcasts.
  use_hints: true
  # Determines if the plugin should clear previous broadcasts before displaying new ones.
  clear_broadcasts: true
  # Enables or disables the plugin.
  is_enabled: true
  # Enables or disables debug mode for the plugin.
  debug: false
```

**DoorTypes for affected_doors:** https://github.com/ExMod-Team/EXILED/blob/master/EXILED/Exiled.API/Enums/DoorType.cs

---
