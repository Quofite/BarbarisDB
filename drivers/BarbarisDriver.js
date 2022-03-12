var request = require('request');

var options = {
  uri: 'localhost:5232/',
  method: 'GET',
  json: {
    "method": "get",
    "file": "testfile",
    "data": "name=gleb"
  }
};

request(options, function (error, response, body) {
    console.log(options);
    console.log(response);
    console.log(body); 
});
