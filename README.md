# BaseApiService
simple service for call to api

## Api Usage

### Create instance
```c#
var apiService = new BaseApiService.BaseApiService();
```
### Create GET request to api for get HttpRespnseMesage
```c#
var response = await apiService.ExecuteGetAsync("path to your endpoint");
```
### Create GET request to api for get model by your type
```c#
TestClass response = await apiService.GetAsync<TestClass>("path to your endpoint");
```
### Create POST request to api for get HttpRespnseMesage
```c#
HttpResponseMessage response = await apiService.ExecutePostAsync("path to your endpoint");
```
### Create POST request to api for get model by your type
```c#
TestClass response = await apiService.PostAsync<TestClass>("path to your endpoint", requestModel);
```



### Additional abilities
```c#
//create instance by custom httpClient
var apiService = new BaseApiService.BaseApiService(HttpClient client);

//set baseUrl 
var apiService = new BaseApiService.BaseApiService(string basePartOfPath);
```
