# Saverr

Saverr is a binary used as a cronjob on Windows server.
Saverr aims to automate frequent saves from a specific filesystem path into
another.

## Install

Simply run the following command:

```powershell
Invoke-WebRequest -Uri
"https://github.com/LilianSchall/saverr/releases/download/v0.0.4/Saverr.exe" -OutFile Saverr.exe
```

## Usage

The following example makes a backup of `D:\` drive in `X:\` drive with a
maximum number of saves set to `5`.

```powershell
.\Saverr.exe "D:\" "X:\" 5
```

As this binary is built to be used as a cronjob, you can use this utility in a
powershell command to set a frequency of launch:

```powershell
$Action = New-ScheduledTaskAction -Execute "C:\Program Files (x86)\Saverr\Saverr.exe" -Argument '"D:\" "X:\" "5"' 
$Trigger = New-ScheduledTaskTrigger -Daily -At 1am
Register-ScheduledTask -Action $Action -Trigger $Trigger -TaskName "MyDailyJob" -Description "Runs backup service every day at 1AM"
```
