# About

**FileConverter** is a .NET 7 ASP.NET REST API for converting files and stori.

Supported conversions:
 - XML to JSON files

Supported storages:
 - Physical Storage

## Usage

The API has Swagger document generator enabled on 'Development' environment.

In order to test the endpoints follow the steps below:
1. Open the `FileConverter` solution in Visual Studio preferably 2022
2. Build the solution /this will automatically restore used NuGet packages/
3. _Optional_: Run the unit tests placed inside `FileConverter.Api.UnitTests` project
4. Start `FileConverter.Api` project /this will open the Swagger page/

**Configuring**
1. _Optional_: If needed change the directory where the converted files are stored - 
adjust `ConvertedFileDirectory` placed inside `FileConverter.Api`, `appsettings.Development.json` configuration file

#### XmlConverter
1. ToJson
- Expand the **api/XmlConverter/ToJson** `POST` endpoint
- Choose file for the parameter `File`
- Click on `Execute` button
- See the result in `Server response` sectioin

## License

[MIT](https://choosealicense.com/licenses/mit/)