# FanSet

Someone could say I'm obsessed with controlling fans.

## Usage

```
C:\Users\Michele\Documents\FanSet\FanSet\bin\Release>fanset.exe

Fan Control #1 == 80%
Fan Control #2 == 40%
Fan Control #3 == 65%

C:\Users\Michele\Documents\FanSet\FanSet\bin\Release>fanset.exe "Fan Control #1" 85

Fan Control #1 = 85%

```

## Why
I couldn't see an example on how to do this online so here we go.

## The catch
This program will not re-enable automatic fan control, so use it only if you want to manage your fans manually until next reboot.

Also, OpenHardwareMonitor is pretty outdated at this point and this program may not be able to find suitable fan controls for your motherboard.

## Credits
This project uses [OpenHardwareMonitorLib](https://github.com/openhardwaremonitor/openhardwaremonitor). Huge thanks to them.
