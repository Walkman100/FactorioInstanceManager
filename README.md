# FactorioInstanceManager [![Build status](https://ci.appveyor.com/api/projects/status/414yhkkdvly6b0s5?svg=true)](https://ci.appveyor.com/project/Walkman100/FactorioInstanceManager)
Factorio Instance Manager - Instance refers to a set of mods, saves, settings e.t.c.

## Download
Get the latest version [here](https://github.com/Walkman100/FactorioInstanceManager/releases), and the latest build from commit
[here](https://ci.appveyor.com/project/Walkman100/FactorioInstanceManager/build/artifacts)
(note that these builds are built for the Debug config and so are not optimised)

## Compile requirements
See [CompileInstructions.md](https://github.com/Walkman100/gists/blob/master/CompileInstructions.md)

## Settings
Settings are stored in `FactorioInstanceManager.xml`. By default, this is stored in:
- Windows:
  - `%AppData%\WalkmanOSS\`
- Linux:
  - `$HOME/.config/WalkmanOSS/`

If you create a new file with the same name in the same directory
FactorioInstanceManager is stored in, it will use this file instead.

## Data Modification
Note that the only data modification (other than this app's settings and creating/deleting instances)
is done in [General.Installs.SetInstallCurrentInstance](General.vb#L116)