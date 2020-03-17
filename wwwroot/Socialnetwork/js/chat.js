"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connection2 = new XMLHttpRequest();
var connection3 = new XMLHttpRequest();
//Disable send button until connection is established
document.getElementsByClassName("send-message-input-1").disabled = true;

//connection.on("ReceiveMessage", function (user, message) {
//    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//    var encodedMsg = user + " says " + msg;
//    var li = document.createElement("li");
//    li.textContent = encodedMsg;
//    document.getElementById("messagesList").appendChild(li);
//});
connection.on("ReceiveMessage", function (user, message, idRoom) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
  //  var encodedMsg = user + " says " + msg;
  //  var li = document.createElement("li");
  //  li.textContent = encodedMsg;

    var text = document.createTextNode(msg);
    var divText = document.createElement("div");
    divText.className = "text";
    divText.appendChild(text);

    var messageBody = document.createElement("div");
    messageBody.className = "message__body";
    messageBody.appendChild(divText);
  //  document.getElementsByClassName("send-message-input-1").reset();

    connection3.onreadystatechange = function (data) {
        console.log(data);
        //Kiem tra neu nhu da gui request thanh cong
        if (this.readyState == 4 && this.status == 200) {
            //In ra data nhan duoc
            if (data.currentTarget.response === "sender") {
                var sentMessage = document.createElement("div");
                sentMessage.className = "sent-message mb-4";
                sentMessage.appendChild(messageBody);
                sentMessage.innerHtml = connection3.responseText;
                document.getElementById(idRoom).appendChild(sentMessage);
            }
            if (data.currentTarget.response === "receiveder") {
                var received = document.createElement("div");
                received.className = "received-message mb-4";
                received.appendChild(messageBody);
                received.innerHtml = connection3.responseText;

                document.getElementById(idRoom).appendChild(received);
            }
        }
    };

    //cau hinh request
    connection3.open("POST", '/Chat/GetUser', true);
    connection3.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    connection3.send("userName=" + user );
});

connection.start().then(function () {
  //  document.getElementsByClassName("send-message-input-1").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementsByClassName("send-message-input-1")
//    .addEventListener("keyup", function (event) {
//        event.preventDefault();
//        if (event.keyCode === 13) {
//            var userName1 = document.getElementById("userInput1").value;
//            var message1 = document.getElementsByClassName("send-message-input-1").value;
//            var idRoom = document.getElementById("RoomId").value;
//            console.log("hello");

//            //ajax
//            connection2.onreadystatechange = function () {
//                //Kiem tra neu nhu da gui request thanh cong
//                if (this.readyState == 4 && this.status == 200) {
//                    //In ra data nhan duoc

//                }
//            };
//            //cau hinh request
//            connection2.open("POST", '/Chat/LoadMessage', true);
//            connection2.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//            //gui request

//            connection2.send("userName=" + userName1 + "&message=" + message1 + "&idRoom" + idRoom);
//            event.preventDefault();
//        }
//    });
//document.getElementById("send-message-input-1").addEventListener("click", function (event) {
//    var userName = document.getElementById("userInput1").value;
//    var message = document.getElementById("send-message-input-1").value;
//    connection.invoke("SendMessage", userName, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
