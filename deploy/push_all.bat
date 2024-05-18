setlocal

set VERSION=%1
set API_KEY=%2

for /L %%Y in (2015,1,2024) do (
    call push.bat %%Y %VERSION% %API_KEY%
)

endlocal
