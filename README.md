# SCPLockdown

<a href="https://github.com/Raul125/SCPLockdown/releases"><img src="https://img.shields.io/github/v/release/Raul125/SCPLockdown?include_prereleases&label=Release" alt="Releases"></a>
<a href="https://github.com/Raul125/SCPLockdown/releases"><img src="https://img.shields.io/github/downloads/Raul125/SCPLockdown/total?label=Downloads" alt="Downloads"></a>

Exiled Plugin to lockdown SCPs at the beginning of the round for a specified amount of time and more features, check the config below.<br>

Original Dev: https://github.com/AlmightyLks

---
### Configs

Default:  
```yaml
scp_lockdown:
  is_enabled: true
  # The affected SCPs, their shown text when unlocked and the time of their lockdown. (RoleType, string, int => RoleType, text, time in seconds)
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
  # Doors that you want to open/unlock/destroy/unlock after x seconds, this doors are locked at the round start. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)
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
  # Can the Scp-079 use/switch cameras while is in lockdown?
  scp079_camera: true
  # the plugin should use hints or broadcasts?.
  use_hints: true
```

**DoorTypes for affected_doors:** https://github.com/Exiled-Team/EXILED/blob/master/Exiled.API/Enums/DoorType.cs

---
### Lockdown

#### SCP 173
Peanut's connector door will be locked down for the specified duration because Heavy Gate is opened by game base.  

#### SCP 079
Computer cannot interact with doors, elevators and "cameras" such for specified duration.

#### SCP 096
Shy Guy's door towards the HCZ will be locked down for the specified duration.  

#### SCP 106
Larry will be put in lockdown inside of his own pocket dimension for the specified duration.  

#### SCP 049
The doctor's heavy gate will be locked down for the specified duration.  

#### SCP 939
The two room doors will be locked down for the specified duration.  
