var http = require('http');

http.createServer(function (req, res) {
    res.writeHead(200, {'Content-Type': 'text/plain'});
    //res.write("test");

    //process.env.forEach(x => res.write(x.key));
    res.end(JSON.stringify(process.env));
}).listen(8080);
