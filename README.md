# SCPLockdown

<a href="https://github.com/Raul125/SCPLockdown/releases"><img src="https://img.shields.io/github/v/release/Raul125/SCPLockdown?include_prereleases&label=Release" alt="Releases"></a>
<a href="https://github.com/Raul125/SCPLockdown/releases"><img src="https://img.shields.io/github/downloads/Raul125/SCPLockdown/total?label=Downloads" alt="Downloads"></a>

Original Dev: https://github.com/AlmightyLks

Exiled Plugin to lockdown SCPs at the beginning of the round for a specified amount of time.<br>

---
### Configs

The plugin takes max. 6 different configurable SCPs and their desired duration in seconds.  
However, when setting both of the 939 SCPs, the latter one will be the one dominating.  

Example:  

```yaml
scp_lockdown:
  is_enabled: true
  # The affected SCPs and their duration of lockdown.
  affected_scps:
    Scp079: 60
    Scp173: 60
    Scp096: 60
    Scp106: 60
    Scp049: 60
    Scp93989: 60
    Scp93953: 60
  # Can the Scp-079 use the cameras while is in lockdown?
  scp079_camera: true
  # Time of Class-D locked in his cells, 0 is disabled
  class_d_lock: 0
  # Displayed to the scps when his lockdown is finished.
  c_b_hint: Containment Breach!
  # Custom Cassie for simulating containment breach announce.
  cassie_msg: containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols
  # Cassie Time to be played, 0 to disable it
  cassie_time: 60
```

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

#### SCP 939 89 & 53
The two stair-entry doors will be locked down for the specified duration.  
