### Setup iis express to allow ssl for port 9000.

By default ssl on iis express is only setuped for port range from 44300 to 44399. To be able to run the app on port 9000 you need to run  some commands with elevated permissions.

The intruction is in https://learn.microsoft.com/en-us/iis/extensions/using-iis-express/handling-url-binding-failures-in-iis-express#using-a-custom-ssl-port.

In summary you need to run the following command on the console.

`netsh http add sslcert ipport=0.0.0.0:<portNumber> certhash=<thumbprint> appid=<appid>`

Example:

`netsh http add sslcert ipport=0.0.0.0:9000 certhash=86c43d3e28920e880e39d34df36ac4c3d942f46b appid={00112233-4455-6677-8899-AABBCCDDEEFF}`

### About the Project.
The project uses blazor wasm as the client and asp.net core api as the server.

### Running the Project.
1. Please set the startup project to "TEG.CodingChallenge.Server". This would run both the server and the client.
2. Please run using iis express.
3. If the above setup for ssl port 9000 was done properly. The project should run on port 9000 properly.


### API Documentation
- A the detailed documentation about the api can be found on `https://localhost:9000/swagger/index.html`

### I would like to add the following if I have more time.
 - Make a better UI.
 - Add searching.
 - Also filter event by date.




