Write-Host "`r`nInstall .NET Core Tool`r`n"
dotnet tool install --global rapicgen

Write-Host "`r`nDownload Swagger Petstore spec`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
rapicgen autorest Swagger.json GeneratedCode ./AutoRestOutput.cs

Write-Host "`r`nTesting NSwag Code Generation`r`n"
rapicgen nswag Swagger.json GeneratedCode ./NSwagOutput.cs

Write-Host "`r`nTesting Swagger Code Generation`r`n"
rapicgen swagger Swagger.json GeneratedCode ./SwaggerOutput.cs

Write-Host "`r`nTesting Open API Code Generation`r`n"
rapicgen openapi Swagger.json GeneratedCode ./OpenApiOutput.cs

Write-Host "`r`n"