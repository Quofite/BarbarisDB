# BARBARIS BD - database controlling system
## Containing:
[Main Info](#info)
[Node.js Driver](#node_driver)
[Python Driver](#python_driver)
[Request String](#request_string)


## Info<a name="info"></a>
Barbaris DB is an open-sourse database controlling system made with C# and ASP.NET 6.<br>

It uses documents system instead of tables. <br>
Data contains in a such way:<br>

id:1;name:Gleb;surname:Nikitin;<br>
name:Gleb;id:2<br>
surname:Nikitin;name:Gleb

It helps in memory managment because there are no unneedable fields:

id:1;name:Gleb;surname:Nikitin;<br>
name:Gleb;id:2;<b>surname:null</b>;<br>
surname:Nikitin;name:Gleb;<b>id:null</b>;

## Drivers
At the moment DB has drivers for Node.js and Python, sooner C# and Java drivers will be added.

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

## Request String <a name="request_string"></a>
To get or set data you are to use Request String, you could see it in drivers as special parameters in js object or python dictionary. Their spelings are depends on method:
#### Get method:
For get method you shuold write filters for data:
1) "*" - getting all
2) key=value - returns every line from db that contains such pair of key and value
3) key=value or key2=value2 - returns all lines that contains <b>ANY</b> of this 2 pairs
4) key=value and key2=value2 - returns all lines that contains <b>BOTH</b> pairs

You can use only 2 pairs, future updates will increase its ammout

#### Set method
For set method you write line to save in database in such way: <b>key:value;key2:value2;</b>