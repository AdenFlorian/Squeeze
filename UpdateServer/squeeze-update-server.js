var fs = require('fs')
var express = require('express')

var appName = 'Squeeze Update Server'

var app = express()

app.get('/', function (request, response) {
	response.send('Hello World!')
})

app.get('/version/newest', function (request, response) {
	var versions = JSON.parse(fs.readFileSync('versions.json', 'utf8'))
	var newestVersion = versions.newest
	response.send(newestVersion)
})

app.get('/package/:version', function (request, response) {
	var requestedVersion = request.params.version
	var file = 'update_packages/' + requestedVersion + '.zip'
	response.download(file)
})

var port = 8080

app.listen(port, function () {
	console.log(appName + ' running at http://localhost:' + port + '/')
})
