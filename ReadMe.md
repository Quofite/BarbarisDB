# BARBARIS BD - database controlling system
## Containing:
[Main Info](#info)<br>
[Installing](#installing)<br>
[Node.js Driver](#node_driver)<br>
[Python Driver](#python_driver)<br>
[Java Driver](#java-driver)<br>
[Request String](#request_string)

## Info<a name="info"></a>
Barbaris DB is an open-sourse database controlling system made with C# and ASP.NET 6.<br>

It uses documents system instead of tables. <br>
Data contains in a such way:<br>

id:1, name:Gleb, surname:Nikitin;<br>
name:Gleb, id:2;<br>
surname:Nikitin, name:Gleb;

It helps in memory managment because there are no unneedable fields:

id:1, name:Gleb, surname:Nikitin;<br>
name:Gleb, id:2, <b>surname:null</b>;<br>
surname:Nikitin, name:Gleb, <b>id:null</b>;

## Installing<a name="installing"></a>
#### Windows:
1) Download <b>BarbarisDBWindows64.rar</b> from releases
2) Unpack it
3) Download ASP.NET Core Runtime from <a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">here</a>
4) Run BarbarisDB.exe on your server and check for host and port in starting message(it should be first pair)

#### Debian-like systems (Debian, Ubuntu, Mint and so on):
1) Download <b>BarbarisDBUbuntu64.tar.gz</b> from releases
2) Unpack it
3) Download ASP.NET Core runtime from <a href="https://docs.microsoft.com/ru-ru/dotnet/core/install/linux-debian">here for Debian</a> or <a href="https://docs.microsoft.com/ru-ru/dotnet/core/install/linux-ubuntu">here for Ubuntu</a>
4) Open terminal at folder with BarbarisDB files and type<br>
``` ./BarbarisDB ```
5) Check for host and port in starting message(it should be first pair)

## Drivers
At the moment DB has drivers for Node.js, Python and Java, sooner C#, PHP and Go drivers will be added.

### Node.js driver<a name="node_driver">:
1) <b>npm install request</b> in your terminal
2) Put BarbarisDriver.js file in your project
3) Add referense in your code by <b>var bdb = require("./BarbarisDriver")</b>
4) Make connecting object:
    ```
        var options = {
            url: "http://localhost:5232/",  // url of database, path is '/'
            method: 'GET',                  // http method, GET is more prior, but POST should work too
            json: {                         // json with db request
                "method": "get",                    // method get/set (deferences are obvious, i guess)
                "file": "testfile",                 // db filename
                "data": "name=gleb and id=4"        // data - request string for get(for request string info see special chapter) or data string for set
            }	
        };
    ```
    
5) Getting data from DB:
    #### Basic
    ```
        function Callback(data) { // <-- function must request parameter of db data
	        // your code
        }

        bdb.BdbRequest(options, Callback);  // and than you call for BdbRequest with previous params
    ```
    
    #### Outputing data with Express.js
    ```
        function CallbackExpress(data) {
            var yourResponseThatUsesData;
            return yourResponseThatUsesData;    // !!! IT MUST RETURN SOMETHING !!!
        }

        app.get("/path", (request, response) => {
            bdb.BdbExpressHttpResponse(options, CallbackExpress, response); // <--- pay attention on parameters, that you are to put http-response object
        });
    ```
    
    
### Python driver<a name="python_driver"></a>

1) <b>pip install -U requests</b> in your terminal
2) Put BarbarisDriver.py in yout project
3) ```import BarbarisDriver as bdb```
4) Make connection dictionary:
```
    data = {
        "method": "get",                # method get/set (diferences are obvious)
        "file": "testfile",             # name of database file
        "data": "name=gleb and id=4"    # request string / data string (depends on method, for more info see special chapter)
    }
```
5) Using data (print is for example, pay attention that function is returning data) 
```
print(bdb.bdb_request("http://localhost:5232/", data))  # calling for bdb_request function form BarbarisDriver and putting db's url and dict as params
```

### Java driver<a name="java_driver"></a>

1) Install <a href="https://github.com/google/gson#download">GSON</a> to your project.
2) Put 3 Java files somewhere into your project's package.
3) Call request by ``` BarbarisDriver.exec("host", "method", "file", "data"); ```<br>
   Make attention that function returns String object and if something wrong it will tell you.

## Request String <a name="request_string"></a>
To get or set data you are to use Request String, you could see it in drivers as special parameters in js object or python dictionary. Their spelings are depends on method:
#### Get method:
For get method you should write filters for data:
1) "*" - getting all
2) key=value - returns every line from db that contains such pair of key and value
3) key=value or key2=value2 - returns all lines that contains <b>ANY</b> of this 2 pairs
4) key=value and key2=value2 - returns all lines that contains <b>BOTH</b> pairs

You can use only 2 pairs, future updates will increase its ammout

#### Set method:
For set method you write line to save in database in such way: <b>key:value, key2:value2;</b>
