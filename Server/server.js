defaultPort = 3000;
var io = require("socket.io")(process.env.port|| defaultPort);
var playerCount = 0;
console.log("Server started");

io.on("connection", function(socket){
    console.log("Client Connected!");

    //Broadcast to all connected clients:
    socket.broadcast.emit("spawn");
    playerCount++;
    for (i = 0; i < playerCount; i++){
        //Only sent to newly connected clients
        socket.emit("spawn");
        console.log("Sending spawn action to new client.");
    }
    socket.on("move", function(data){
        console.log("Client moved!");
    });

    socket.on("disconnect", function(){
        console.log("Client disconnected.");
        playerCount--;
    });
})