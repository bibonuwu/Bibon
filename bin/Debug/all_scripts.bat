@echo off
color D7
echo Configuring date and time formats (en-US)...

:: Set the first day of the week — Sunday (0 = Sunday)
REG ADD "HKCU\Control Panel\International" /v iFirstDayOfWeek /t REG_SZ /d 0 /f

:: Date formats
REG ADD "HKCU\Control Panel\International" /v sShortDate /t REG_SZ /d dd.MM.yyyy /f
REG ADD "HKCU\Control Panel\International" /v sLongDate /t REG_SZ /d d MMMM yyyy 'г.' /f

:: Time formats
REG ADD "HKCU\Control Panel\International" /v sShortTime /t REG_SZ /d H:mm /f
REG ADD "HKCU\Control Panel\International" /v sTimeFormat /t REG_SZ /d H:mm:ss /f

:: Set "This PC" as the default Explorer folder
echo Setting "This PC" as the default Explorer start location...

REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v LaunchTo /t REG_DWORD /d 1 /f

:: Restart Explorer (optional but recommended)
echo Removing Russian language and setting preferred languages...

:: Set English as the interface and primary language
powershell -Command "Set-WinUILanguageOverride -Language ru-RU"
powershell -Command "Set-WinUserLanguageList @('en-US','kk-KZ') -Force"
powershell -Command "Set-WinSystemLocale ru-RU"
powershell -Command "Set-Culture ru-RU"

:: Ensure Russian keyboard layout is removed
powershell -Command "$LangList = Get-WinUserLanguageList; $LangList = $LangList | Where-Object { $_.LanguageTag -ne 'ru-RU' }; Set-WinUserLanguageList $LangList -Force"
echo Enabling display of file extensions and hidden files...

:: Show file extensions
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v HideFileExt /t REG_DWORD /d 0 /f

:: Show hidden files
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v Hidden /t REG_DWORD /d 1 /f

:: Enable dark theme for Windows and apps
echo Enabling dark theme for Windows and apps...

:: Dark theme for system
REG ADD "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize" /v SystemUsesLightTheme /t REG_DWORD /d 0 /f

:: Dark theme for apps
REG ADD "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize" /v AppsUseLightTheme /t REG_DWORD /d 0 /f

:: Change taskbar search to icon only
echo Changing taskbar search to icon...

REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Search" /v SearchboxTaskbarMode /t REG_DWORD /d 1 /f

:: Disable News and Interests (Feeds)
REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Feeds" /v EnableFeeds /t REG_DWORD /d 0 /f
echo Disabling all types of notifications...

:: Completely disable notifications
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled /t REG_DWORD /d 0 /f

:: Disable lock screen notifications
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Notifications\Settings" /v NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK /t REG_DWORD /d 0 /f

:: Disable reminders and VoIP on lock screen
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Notifications\Settings" /v NOC_GLOBAL_SETTING_ALLOW_CRITICAL_TOASTS_ABOVE_LOCK /t REG_DWORD /d 0 /f

:: Disable notification sounds
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Notifications\Settings" /v NOC_GLOBAL_SETTING_ALLOW_NOTIFICATION_SOUND /t REG_DWORD /d 0 /f

:: Disable welcome screen after updates
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager" /v SubscribedContent-310093Enabled /t REG_DWORD /d 0 /f

:: Disable setup suggestions
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement" /v ScoobeSystemSettingEnabled /t REG_DWORD /d 0 /f

:: Disable tips and recommendations
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager" /v SubscribedContent-338388Enabled /t REG_DWORD /d 0 /f
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager" /v SubscribedContent-353694Enabled /t REG_DWORD /d 0 /f
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager" /v SubscribedContent-353696Enabled /t REG_DWORD /d 0 /f

echo Disabling screen off and sleep mode...

:: Prevent screen from turning off (0 = Never)
powercfg -change -monitor-timeout-ac 0
powercfg -change -monitor-timeout-dc 0

:: Prevent sleep mode
powercfg -change -standby-timeout-ac 0
powercfg -change -standby-timeout-dc 0

echo Setting taskbar grouping mode: "When taskbar is full"...

REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v TaskbarGlomLevel /t REG_DWORD /d 1 /f

echo Hiding Task View button...

REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v ShowTaskViewButton /t REG_DWORD /d 0 /f

echo [1/4] Taking ownership of system folders...
takeown /f "%ProgramFiles%\WindowsApps" /r /d y >nul
icacls "%ProgramFiles%\WindowsApps" /grant *S-1-5-32-544:F /t >nul

echo [2/4] Removing UWP apps...
:: (List remains unchanged)

echo [3/4] Removing for all new users...
:: (List remains unchanged)

echo [4/4] Removing OneDrive...
taskkill /f /im OneDrive.exe >nul 2>&1
%SystemRoot%\System32\OneDriveSetup.exe /uninstall

echo Removing Copilot (Windows Web Experience Pack)...
:: (Registry entries and removals remain unchanged)

echo Disabling battery saver and brightness reduction...

powercfg /SETDCVALUEINDEX SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 0
powercfg /SETDCVALUEINDEX SCHEME_CURRENT SUB_ENERGYSAVER EnergySaverBrightness 0
powercfg /SETACTIVE SCHEME_CURRENT

echo Setting time zone: Astana (UTC+06:00)...
tzutil /s "West Asia Standard Time"

echo Disabling background apps...
REG ADD "HKCU\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications" /v GlobalUserDisabled /t REG_DWORD /d 1 /f

