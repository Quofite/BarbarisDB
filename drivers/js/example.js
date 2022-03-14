var bdb = require("./BarbarisDriver");
var express = require("express");
var app = express();

// !!! npm install request !!!

// ---- object with connection data

var options = {
    url: "http://localhost:5232/",  // url of database, path is '/'
    method: 'GET',                  // http method, GET is more prior, but POST should work too
    json: {                         // json with db request
      "method": "get",                    // method get/set (deferences are obvious, i guess)
      "file": "testfile",                 // db filename
      "data": "name=gleb and id=4"        // data - request string for get(for request string info see readme file) / data string for set
  }	
};

// ---- common getting data from db

function Callback(data) { // <-- function must request parameter of db data
	// your code
}

bdb.BdbRequest(options, Callback);  // and than you call for BdbRequest with previous params

// ---- getting data from db and response it using express

function CallbackExpress(data) {
  var yourResponseThatUsesData;
  return yourResponseThatUsesData;    // !!! IT MUST RETURN SOMETHING !!!
}

app.get("/path", (request, response) => {
  bdb.BdbExpressHttpResponse(options, CallbackExpress, response); // <--- pay attention on parameters, that you are to put http-response object
});

app.listen(3000);