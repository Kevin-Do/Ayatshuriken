defaultPort = 3000;
var io = require("socket.io")(process.env.port|| defaultPort);

console.log("Server started");

io.on("connection", function(socket){
    console.log("Client Connected!");

    //Broadcast to all connected clients:
    socket.broadcast.emit("spawn");

    socket.on("move", function(data){
        console.log("Client moved!");
    });
})