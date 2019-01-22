# REST API Client Code Generator
A collection of Visual Studio custom tool code generators for Swagger / OpenAPI specification files

**Features**

- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the Swagger.json file
- Supports Visual Studio 2017 and 2019


**Custom Tools**

- AutoRestCodeGenerator - Generates a single file C# REST API Client using AutoRest. 
The resulting file is the equivalent of using the AutoRest CLI tool with 
`--csharp " +--input-file=[swaggerFile] --output-file={outputFile} --namespace="{defaultNamespace} --add-credentials`
