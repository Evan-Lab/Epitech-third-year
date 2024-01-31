/*const express = require('express');
const httpProxy = require('http-proxy');

const app = express();
const proxy = httpProxy.createProxyServer({});

app.all('/auth/*', (req, res) => {
  proxy.web(req, res, { target: 'https://oauth-server-url.com' });
});

app.listen(3000, () => {
  console.log('Proxy server is running on port 3000');
});*/
