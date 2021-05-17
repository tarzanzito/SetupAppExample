
echo OFF
echo APP INSTALL

pushd "%~dp0"

echo ARG0=%~dp0
echo ARG1=%1
echo ARG2=%~2

IF "%~1" == "32BIT" (
	echo USING 32BIT
) ELSE (
	echo UNSING 64BIT
)

rem C:\Applics\7-zip\7z.exe x HAL_64.7z -oc:\Applics\Cockpit\ -aoa
rem IF %ERRORLEVEL% NEQ 0 (GOTO endError)

:endOk
EXIT /b 0

:endError
Exit /B %ERRORLEVEL%%