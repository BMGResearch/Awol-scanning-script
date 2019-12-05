
# Awol-scanning-script
> Direct resolution for https://trello.com/c/W7bh3Uq4

> Trello Epic: [See Trello Card](https://trello.com/c/kVV6rXQ2)


## Background

Program automatically check Interviewers who have not logged on for their shift.

For Interviewer not logged on for their shift program will change today schedule status from core to awol.

In CC_SchedulingSickLate Table program will create new awol entry for awol interviewer.

Program agency and internal stuff will get email notification (Resource manager, and interviewer team leader).

In email startTime/endTime is included and also total number of previous awol's.

## Issue / current system
Current problem is it sometimes doesn't run or doesn't action real awols for the script.


### Installation and set up
1. Git clone the repository.
2. Restore Nuget Packages.
4. Run app from vs.

## Schedule
List of schedule for this project.

| Frequency        | Location | Name |
| ------------- |:-------------:| :-------------:| 
| Daily 11am     |.10 server C:\Shares\Applications\Awol-Scanning-Script |Awol-scanning-script11Am|
| Daily 6pm     |.10 server C:\Shares\Applications\Awol-Scanning-Script |Awol-scanning-script6Pm|


## Developers 
List of people and their responsibilty regarding the source code.

| Name        | Responsibilty| Contact |
| ------------- |:-------------:| :-------------:| 
| Mateusz Stacel      | Lead developer  | mateusz.stacel@bmgresearch.co.uk|


## Product owners 
List of key contacts that are aware of the context of the project.

| Name        | Responsibilty | Contact |
| ------------- |:-------------:| :-------------:| 
| Mokbul Miah      | Overseer | mokbul.miah@bmgresearch.co.uk|
| James Skinner    | Previous script owner | James.Skinner@bmgresearch.co.uk|
| Ahmed Ali   | Resource Manager | Ahmed.Ali@bmgresearch.co.uk|
| Zishan Sajan   | Director | zishan.sajan@bmgresearch.co.uk|

## Related resource

| Name        | Description | Link
| ------------- |:-------------:| :-------------:| 
| WMS   | Please see [WMS App](https://github.com/BMGResearch/BMG_WMS) readme for more context. | https://github.com/BMGResearch/BMG_WMS


## High level change log 


| Date and time (dd/mm/yyyy hh:mm)      | Status/latest changes |
| ------------- |:-------------:|
| 15/11/2019      | Initial project started
| 21/11/2019      | project is running in test mode it mean that only internal stuff will get email notification (Resource manager, and interviewer team leader).

