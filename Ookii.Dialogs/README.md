# Ookii.Dialogs.dll

The only solution I have found to use a different filename, is to compile Ookii.Dialogs as a different assembly name. Renaming the file and referencing it allows the project to build, but on runtime FactorioInstanceManager still looks for `Ookii.Dialogs.dll`...

### Compiling `FactorioInstanceManager-Ookii.Dialogs.dll`

I have included this file in this repo for ease of use, but to get the exact same binary from source, follow these instructions from within the folder this README is in:

#### Linux/WSL:
- `rm FactorioInstanceManager-Ookii.Dialogs.dll`
- `git clone https://github.com/Walkman100/Ookii.Dialogs`
- `cd Ookii.Dialogs`
- `cp ../Ookii.Dialogs_Rename_Patch.patch .`
- `git apply Ookii.Dialogs_Rename_Patch.patch`
- Compile the solution:
  - Linux: `msbuild`
  - WSL: `/mnt/c/Windows/Microsoft.NET/Framework/v4.0.30319/MSBuild.exe`
- `cp bin/Debug/FactorioInstanceManager-Ookii.Dialogs.dll ..`
- `cd ..`
- Optionally `rm -rf Ookii.Dialogs/`

#### Windows CMD:
- `del FactorioInstanceManager-Ookii.Dialogs.dll`
- `git clone https://github.com/Walkman100/Ookii.Dialogs`
- `cd Ookii.Dialogs`
- `copy ..\Ookii.Dialogs_Rename_Patch.patch .`
- `git apply Ookii.Dialogs_Rename_Patch.patch`
- Compile the solution: `C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe`
- `copy bin\Debug\FactorioInstanceManager-Ookii.Dialogs.dll ..`
- `cd ..`
- Optionally `rmdir /S /Q Ookii.Dialogs`
