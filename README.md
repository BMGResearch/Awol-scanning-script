# Awol-scanning-script

Program is scheduled to run at 11am and 6pm.

Program automatically check Interviewers who have not logged on for their shift.

For Interviewer not logged on for their shift program will change today schedule status from core to awol.

In CC_SchedulingSickLate Table program will create new awol entry for awol interviewer.

Program is running in test mode, it mean that only internal stuff will get email notification (Resource manager, and interviewer team leader).

In email startTime/endTime is included and also total number of previous awol's.

Script name in task scheduller: Awol-scanning-script11Am, Awol-scanning-script6Pm

Script Location: C:\Shares\Applications\Awol-Scanning-Script
