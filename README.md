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
  is_enabled: true
  # The affected SCPs, the shown text when unlocked and the time of their lockdown. (RoleType, string, int => RoleType, text, time in seconds)
  affected_scps:
  - role_type: Scp079
    text: Containment Breach!
    time_to_unlock: 60
  - role_type: Scp173
    text: Containment Breach!
    time_to_unlock: 60
  - role_type: Scp096
    text: Containment Breach!
    time_to_unlock: 60
  - role_type: Scp106
    text: Containment Breach!
    time_to_unlock: 60
  - role_type: Scp049
    text: Containment Breach!
    time_to_unlock: 60
  - role_type: Scp939
    text: Containment Breach!
    time_to_unlock: 60
  # Doors that you want to open/unlock/destroy/unlock after x seconds, this doors are locked at round start. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)
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
    open: false
  # Use this if you want send cassies with a specified timing. (string, int => cassie text, delay in seconds)
  cassies:
  - content: containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols
    delay: 60
  - content: containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols
    delay: 120
  # Can the Scp-079 use/switch cameras while in lockdown?
  scp079_camera: true
  # the plugin should use hints or broadcasts?.
  use_hints: true
```

**DoorTypes for affected_doors:** https://github.com/Exiled-Team/EXILED/blob/master/Exiled.API/Enums/DoorType.cs

---
