var request = require('request');

module.exports.BdbRequest = function(options, Callback) {
  	request(options, function(error, response, body) {
		Callback(body.output);
	});
}

module.exports.BdbExpressHttpResponse = function(options, Callback, httpResponse) {
	request(options, function(error, response, body) {
		httpResponse.send(Callback(body.output));
	});
}
