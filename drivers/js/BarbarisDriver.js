var request = require('request');



module.exports.BdbRequest = function(options, Callback) {
	var dbResponse;

  	request(options, function(error, response, body) {
    	dbResponse = body.output;
		Callback(dbResponse);
	});
}

module.exports.BdbExpressHttpResponse = function(options, Callback, httpResponse) {
	var dbResponse;

	request(options, function(error, response, body) {
		dbResponse = body.output;
		httpResponse.send(Callback(dbResponse));
	});
}