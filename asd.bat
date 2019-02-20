echo OFF
for /F "tokens=1" %%i in (cmd.txt) do git.exe %%i
pause