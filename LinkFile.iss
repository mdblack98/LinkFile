; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "LinkFile"
#define MyAppVersion "20240531"
#define MyAppPublisher "Michael Black W9MDB"
#define MyAppURL "https://github.com/mdblack98/LinkFile"
#define MyAppExeName "LinkFile.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{42384B86-CCA1-453F-8591-2700664CE2CF}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=C:\Users\mdbla\Dropbox\Projects\LinkFile\LICENSE
; Remove the following line to run in administrative install mode (install for all users.)
PrivilegesRequired=lowest
OutputDir=C:\Users\mdbla\Dropbox\Projects\LinkFile\Output
OutputBaseFilename=LinkFile
SetupIconFile=C:\Users\mdbla\Dropbox\Projects\LinkFile\icons8-link-94.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\mdbla\Dropbox\Projects\LinkFile\bin\Release\net7.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mdbla\Dropbox\Projects\LinkFile\bin\Release\net7.0-windows\LinkFile.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mdbla\Dropbox\Projects\LinkFile\bin\Release\net7.0-windows\LinkFile.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mdbla\Dropbox\Projects\LinkFile\bin\Release\net7.0-windows\LinkFile.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mdbla\Dropbox\Projects\LinkFile\bin\Release\net7.0-windows\LinkFile.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
