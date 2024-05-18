setlocal

set VERSION=%1

for /L %%Y in (2015,1,2024) do (
    call pack.bat %%Y %VERSION%
)

endlocal
