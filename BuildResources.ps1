Copy-Item -Path "src/WebApp/Views" -Destination "Temp/Views" -Recurse
Copy-Item -Path "src/WebApp/wwwroot/js/" -Destination "Temp/wwwroot/js/" -Recurse

Compress-Archive -Path 'Temp/*' -DestinationPath "Resources.zip" -Force
Remove-Item -LiteralPath "Temp" -Force -Recurse
Move-Item -Path "Resources.zip" -Destination "Resources.zip"
