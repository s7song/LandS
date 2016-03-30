var express = require('express'),
    path = require('path'),
    port = process.env.PORT || 8080,
    app = express(),
    url = require("url");

if (process.env.NODE_ENV == "dev") {
    app.use(express.static(__dirname));    
    app.get('*', function (request, response) {
        response.sendfile('./index.html');
    });
}
else {
    app.use(express.static(path.join(__dirname, "public")));
    app.get('*', function (request, response) {
        response.sendfile('./public/index.html');
    });
}

app.listen(port, function () {
    console.log('Server listening on port ' + port);
});