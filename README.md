# Asset Management

### Data Storage
Using Json.Net, storying all data to a txt(could be csv) file under ~/Data folder.
This data storing method avoid any use of database. SQLite should be another choice to keep the web app "data-storage-agnostic". (I'm not sure if this means "database-agnostic" or "data-storage-localized")

### Asset Class:
```cs
string Id //Base64 GUID
string Name
int Count
int Group
float UnitPrice
float TotalValue
```
Since it is not a database system, I generated a Base64 GUID each time when posting a new asset.

### API
Type|URL|Body
--|--|--
GET | /api/asset
GET | /api/asset/{id}
POST | /api/asset | Asset
PUT | /api/asset/{id} | Asset
PATCH | /api/asset | AssetPatchRequest
DELETE | /api/asset/{id}