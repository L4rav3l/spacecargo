## About Project
In this game, you can deliver packages between space stations. You can unlock two endings: the first requires delivering five packages, and the second occurs when you run out of fuel.

## Stack
C# monogame

## Demo video
https://youtu.be/Ctwnhi3CGw8

## Build command

Linux:
```dotnet publish -c Release -r linux-x64 -p:PublishReadyToRun=false -p:TieredCompilation=false --self-contained```

MacOs:
```dotnet publish -c Release -r osx-x64 -p:PublishReadyToRun=false -p:TieredCompilation=false --self-contained```

Windows:
```dotnet publish -c Release -r win-x64 -p:PublishReadyToRun=false -p:TieredCompilation=false --self-contained```
