# Asset Management
This Asset Management system is a quick example solution built for clients in retail business. Their main product are clothes. To start, click on "Inventory", or "TRY NOW" on index.

## Data Storage
Using Json.Net, storying all data to a txt(could be csv) file under ~/Data folder.
This data storing method avoid any use of database. SQLite should be another choice to keep the web app "data-storage-agnostic". (I'm not sure if this means "database-agnostic" or "data-storage-localized")

## Asset Class
```cs
string Id // Base64 GUID Auto-generated
string Name // Not Null
int Count // [0,inf)
Group Group // Valid Enum
float UnitPrice // [0,inf)
float TotalValue // Read only
```

```cs
Group{
    NONE,
    TOP,
    BOTTOM,
    ACCESSORY,
    OTHER
}
```

Since it is not a database system, I generated a Base64 GUID each time when posting a new asset.

## API
Description| Type|URL|Body
--|--|--|--
Get all | GET | /api/asset
Get by ID | GET | /api/asset/{id}
Gey by group | GET | /api/asset/group/{group}
Add | POST | /api/asset | Asset
Update | PUT | /api/asset/{id} | Asset
Update count | PATCH | /api/asset | AssetPatchRequest
Delete | DELETE | /api/asset/{id}

## Tests

### Front-end -> Watir or similar test solutions

1. Go to home/index -> Click "Inventory"
2. Go to home/index -> Click "Try Now"
3. Go to asset/index -> Click "Name" -> Click "Name"
4. Same procedure with 3 applied to "Group", "Count", "Unit Price", "Total Value"
5. Go to asset/index -> Input existed string to "Search Input" -> Click "Search" -> Click "Clear All Filters/Sorts"
6. Go to asset/index -> Input non-existed string to "Search Input" -> Click "Search" -> Click "Clear All Filters/Sorts"
7. Go to asset/index -> Clear "Search Input" -> Click "Search" -> Click "Clear All Filters/Sorts"
8. Go to asset/index -> Click "Clear All Filters/Sorts"
9. Go to asset/index -> Click page selector

### Back-end -> API test
Send the following objects to API one by one, through POST and PUT.
```cs
var testAssets = new List<Asset>(){
    // Invalid
    new Asset { Name = "T-Shirt", Group = Group.TOP, UnitPrice = -1, Count = 10},
    new Asset { Name = "T-Shirt", Group = Group.TOP, UnitPrice = 7.99, Count = -1},
    new Asset { Name = "T-Shirt", Group = (Group)100, UnitPrice = 7.99, Count = 10},
    new Asset { Id = "something", Name = "T-Shirt", Group = Group.TOP, UnitPrice = 7.99, Count = 10},
    // Valid
    new Asset { Name = "T-Shirt", Group = Group.TOP, UnitPrice = 10.78555, Count = 10},
    new Asset { Name = "Hoody", Group = Group.TOP, UnitPrice = 0, Count = 1},
    new Asset { Name = "Sweat Pants", Group = Group.BOTTOM, UnitPrice = 30.99f, Count = 0},
    new Asset { Name = "Skirt", Group = (Group)100, UnitPrice = 28.00f, Count = 13}
}
```

Send the following JSON to API, through POST and PUT.
```json
[
    { //Success
        "Id": "",
        "Name": "Sweat Pants",
        "Count": 0,
        "Group": 2,
        "UnitPrice": 30.99,
        "TotalValue": 0.0
    },
    {
        "Id": "",
        "Name": "", //Null name
        "Count": 3,
        "Group": 2,
        "UnitPrice": 50.99,
        "TotalValue": 152.97
    },
    {
        "Id": "",
        "Name": "Sweater",
        "Count": "test", //Bad type
        "Group": 1,
        "UnitPrice": 30.0,
        "TotalValue": 180.0
    },
    {
        "Id": "",
        "Name": "Hoody",
        "Count": 1,
        "Group": "test", //Bad type
        "UnitPrice": 49.99,
        "TotalValue": 49.99
    },
    {
        "Id": "",
        "Name": "Sweat Pants",
        "Count": 0,
        "Group": 2,
        "UnitPrice": "test", //Bad Type
        "TotalValue": 0.0
    },
    { //Success
        "Id": "",
        "Name": "Skirt",
        "Count": 13,
        "Group": 2,
        "UnitPrice": 28.0,
        "TotalValue": 100000 //This value is not affected by input. It is a get only
    },
    { //Hack data should be ignored
        "Id": "",
        "Name": "Hack",
        "Count": 0,
        "Group": 2,
        "UnitPrice": 30.99,
        "TotalValue": 0.0,
        "Hack": "garbage data"
    }
]
```

Test Get by getting all data, selected ID, or selected group. Both existed and non-existed ID should be tested. Valid group, empty group, and invalid group should be tested.  
Test Delete by deleting selected ID. ID could be either existed or non-existed.
Send the following JSON to API through PATCH
```JSON
[
    { //Success
        "Id": "existedid",
        "Count": 100
    },
    {
        "Id": "non-existedid", //ID not exist
        "Count": 100
    },
    {
        "Id": "existedid",
        "Count": -100 //Invalid Value
    }
]
```
