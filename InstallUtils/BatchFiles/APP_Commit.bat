

echo OFF
echo APP COMMIT

pushd "%~dp0"

echo ARG0=%~dp0
echo ARG1=%1
echo ARG2=%~2

IF "%~1" == "32BIT" (
	echo UNZIP 32BIT
	echo C:\Applics\7-zip\7z.exe x HAL_32.7z -oc:\Applics\Cockpit\ -aoa
) ELSE (
	echo UNZIP 64BIT
	echo C:\Applics\7-zip\7z.exe x HAL_64.7z -oc:\Applics\Cockpit\ -aoa
)

:endOk
EXIT /b 0

:endError
Exit /B %ERRORLEVEL%